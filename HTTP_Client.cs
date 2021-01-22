using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace NetWorking_1_0
{
    class HTTP_Client: Web_Client
    {
        HttpWebRequest req;

        HttpWebResponse resp;

        public HTTP_Client()
        { 
        
        }

        public HTTP_Client(string ext_base_address,
                            string ext_address,
                            ICredentials ext_credentials): base()
        {
            this.set_Base_Address(ext_base_address);
            this.set_Address(ext_address);
            this.set_Credentials( ext_credentials);
        }

        void prepare_Request()
        {
            req = (HttpWebRequest)WebRequest.Create(this.get_Uri());

            req.Credentials = this.get_Credentials();
        }

        public long get_Content_Length()
        {
            try
            {
                prepare_Request();

                //for retrieving meta-information written in response headers, 
                //without having to transport the entire content.
                req.Method = WebRequestMethods.Http.Head;

                resp = (HttpWebResponse)req.GetResponse();

                long result = resp.ContentLength;

                resp.Close();

                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine("get_Content_Length failed.." + e.Message);
                return -1;
            }
        }


    }
}
