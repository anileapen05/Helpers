using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

using IO_1_0;

namespace Feedback_1_0
{

	public enum Object_Result : uint
	{
		OK = 0,
		ARGUMENT_INVALID,
		MEMBER_INVALID,
		METHOD_MEMBER_INVALID,
		METHOD_MEMBER_FAILED
	}



	public class Logger
	{



		//result code
		private Object_Result result;

		//result details string
		private string result_details;

		//log file stream
		private File_Access log_file;

		//log file name
		private string file_name;

		//log file path
		private string file_path;
		public bool b_enabled;

		public Logger()
		{
			b_enabled = false;
			result = Object_Result.OK;
			result_details = "";
			file_name = "";
			file_path = "";
		}

		//log methods
		public void setup_Log_File(string ext_abs_file_name)
		{
			if (b_enabled)
			{
				try
				{
					log_file = new File_Access();

					log_file.create_File(ext_abs_file_name);

					log_file.attach_StreamWriter();

					log_Result(Object_Result.OK, "setup_Log_File: success");
				}
				catch (Exception e)
				{
					return;
				}
			}


		}

		public void close_Log_File()
		{
			if (b_enabled)
			{
				try
				{
					log_Result(Object_Result.OK, "close_Log_File: closing..");

					log_file.close_File();


				}
				catch (Exception e)
				{
					if (log_file != null)
					{
						log_Result(Object_Result.METHOD_MEMBER_FAILED, "close_Log_File: log_file");
					}

					return;
				}
			}
		}

		//log result of method
			public void log_Result(Object_Result ext_result,string ext_result_details)
			{


			if (Enum.IsDefined(typeof(Object_Result),ext_result))

			{
				result = ext_result;
			}
			else
				return;

			if (ext_result_details == null)
				return;

			result_details = ext_result_details;

			if (b_enabled)
			{
				try
				{
					log_file.write_Line_String(ext_result.ToString() + "\t\t\t" + ext_result_details + "\n\n");
				}
				catch (Exception e)
				{
					return;
				}
			}




			}
	}
}