using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading;

using Threading_1_0;
using Helpers_1_0;

namespace NetWorking_1_0
{
    // The Request_State class passes data across async calls.
    public class Request_State
    {
        public int buffer_size = 1024;


        //for complete response
        public string response_string;
        public Byte[] response_bytes;

        //for reading response
        public StringBuilder request_data;
        public Byte[] buffer_read;

        public WebRequest request;
        public Stream response_stream;
        public bool b_complete;

        public Request_State()
        {
            try
            {
                buffer_read = new Byte[buffer_size];
                request_data = new StringBuilder(string.Empty);
                request = null;
                response_stream = null;
                b_complete = true;
            }
            catch (Exception e)
            {
                
            }

        }
    }

	public class Http_Client
	{

		private HttpWebRequest http_web_req;

		private HttpWebResponse http_web_resp;

		private Stream resp_stream;

		private StreamReader stream_reader;

		private BinaryReader binary_reader;

		public string response_string;

		public Byte[] response_bytes;

		public int bytes_read;

		public int timeout;

		public Request_State req_state;

		/*need to use app threading*/
	//public:
		//static ManualResetEvent^ man_res_evnt = gcnew ManualResetEvent(false);



		public Http_Client()
		{
			timeout = 10; //10,000 msecs default
			response_string = "";
			bytes_read = 0;
		}

		public System.Boolean create_Request_For_Valid_Http_Address(string ext_address)
		{
				Uri temp_uri = null;

				try
				{
					temp_uri = Networking_Helpers.create_URI(ext_address);

					http_web_req = (HttpWebRequest) WebRequest.Create(temp_uri);
				}
				catch (InvalidCastException e)
				{
					//MessageBox::Show("Invalid http address\n\n"+e->Message);

					return false;
				}

				return true;
		}

		public void get_Response()
		{
			try
			{
				http_web_resp = (HttpWebResponse)http_web_req.GetResponse();
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.Timeout)
				{
					//MessageBox::Show("Response TimeOut\n\n"+e->Message);
				}
			}
		}

		public void async_Get_Response()
		{

			  try
			  {
				 // Create the state object.
				 req_state = new Request_State();

				 // Put the request into the state object so it can be passed around.
				 req_state.request = http_web_req;

				 //signal start of async request
				 req_state.b_complete = false;

				 // Issue the async request.
				 IAsyncResult r = (IAsyncResult) http_web_req.BeginGetResponse(new AsyncCallback(response_Callback), req_state);

				 // Wait until the ManualResetEvent is set so that the application 
				 // does not exit until after the callback is called.
				 Threading_Globals.man_res_evnt.WaitOne();

			  }
			  catch (Exception e)
			  {
				 return;
			  }
		}

		private static void response_Callback(IAsyncResult async_result)
		{
		  try
		  {
			  // Get the Request_State object from the async result.
			  Request_State req_state = (Request_State)async_result.AsyncState;

			  // Get the WebRequest from Request_State.
			  HttpWebRequest http_req = (HttpWebRequest)req_state.request;

			  // Call EndGetResponse, which produces the WebResponse object
			  //  that came from the request issued above.
			  HttpWebResponse http_resp = (HttpWebResponse)http_req.EndGetResponse(async_result);

			  //  Start reading data from the response stream.
			  Stream response_stream = http_resp.GetResponseStream();


			  // Store the response stream in Request_State to bytes_read 
			  // the stream asynchronously.
			  req_state.response_stream = response_stream;


			  //  Pass req_state.buffer_read to BeginRead. Read data into req_state.buffer_read
			  response_stream.BeginRead(req_state.buffer_read, 0, req_state.buffer_size, new AsyncCallback(read_CallBack), req_state);
		  }
		  catch (Exception e)
		  {
			return;
		  }
		}

		private static void read_CallBack(IAsyncResult ext_async_result)
		{
	   try
	   {
		  // Get the Request_State object from AsyncResult.
           Request_State req_state = (Request_State)ext_async_result.AsyncState;

		  // Retrieve the response_stream that was set in response_Callback. 
		  Stream response_stream = req_state.response_stream;

		  // Read req_state.buffer_read to verify that it contains data. 
          int bytes_read = response_stream.EndRead(ext_async_result);
		  if (bytes_read > 0)
		  {

				 // Append the recently bytes_read data to the request_data stringbuilder
				 // object contained in Request_State.
				 req_state.request_data.Append(Encoding.ASCII.GetString(req_state.buffer_read, 0, bytes_read));

				 // Continue reading data until 
				 // response_stream.EndRead returns -1.
				 IAsyncResult async_result = response_stream.BeginRead(req_state.buffer_read, 0, req_state.buffer_size, new AsyncCallback(read_CallBack), req_state);

		  }
		  else
		  {
			 //reading of response completed
			 if (req_state.request_data.Length > 0)
			 {
				//set response string
				 req_state.response_string = req_state.request_data.ToString();

				 //set response bytes
				 req_state.response_bytes = Encoding.ASCII.GetBytes(req_state.response_string);
			 }

				 // Close down the response stream.
				 response_stream.Close();

				 req_state.b_complete = true;

				 // Set the ManualResetEvent so the main thread can exit.
				 Threading_Globals.man_res_evnt.Set();

		  }
	   }
	   catch (Exception e)
	   {
			return;
	   }

		}

		public void get_Response_Stream()
		{
			try
			{
				resp_stream = http_web_resp.GetResponseStream();
			}
			catch (Exception e)
			{

			}
		}

		public void open_Stream_Reader()
		{
			try
			{
				stream_reader = new StreamReader(resp_stream);
			}
			catch (Exception e)
			{

			}
		}

		public void open_Binary_Reader()
		{
			try
			{
				binary_reader = new BinaryReader(resp_stream);
			}
			catch (Exception e)
			{

			}
		}

		public int read_String_Response_From_Stream()
		{
			//sync
			response_string = "";

			try
			{
				response_string = stream_reader.ReadToEnd();

				stream_reader.Close();


			}
			catch (Exception e)
			{

			}

			return response_string.Length;

		}

		public void close_Stream_Reader()
		{
			try
			{
				stream_reader.Close();
			}
			catch (Exception e)
			{

			}
		}

		public int read_Bytes_Response_From_Stream(int start_pos,int bytes)
		{
			//sync

			bytes_read = 0;

			try
			{
				response_bytes = new Byte[bytes];

				bytes_read = binary_reader.Read(response_bytes,start_pos, bytes);


			}
			catch (Exception e)
			{

			}

			return bytes_read;


		}

		public void close_Binary_Reader()
		{
			try
			{
				binary_reader.Close();
			}
			catch (Exception e)
			{

			}
		}

		public void set_Request_TimeOut()
		{
			try
			{
				http_web_req.Timeout = timeout * 1000;
			}
			catch (Exception e)
			{

			}
		}

		public void set_TimeOut(int ext_time_out)
		{
			timeout = ext_time_out;
		}

		public void close_Response()
		{
			try
			{
				http_web_resp.Close();
			}
			catch (Exception e)
			{

			}
		}


		public void get_Resource(string ext_address)
		{
				try
				{
					if (!create_Request_For_Valid_Http_Address(ext_address))
					{
						return;
					}

					set_Request_TimeOut();

					get_Response();

					get_Response_Stream();

					open_Stream_Reader();

					open_Binary_Reader();
				}
				catch (Exception e)
				{

				}


		} //get_Resource

		public void async_Get_Resource(string ext_address)
		{
				try
				{
					if (!create_Request_For_Valid_Http_Address(ext_address))
					{
						return;
					}

					set_Request_TimeOut();

					async_Get_Response();
				}
				catch (Exception e)
				{

				}

		} //get_Resource


	}
}