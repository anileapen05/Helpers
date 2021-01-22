using System;
using System.IO;
using System.Text;

namespace IO_1_0
{
    enum File_Access_Attached_Status
    { 
        FAAS_None,
        FAAS_StreamReader,
        FAAS_StreamWriter,
    }

	public class File_Access
	{
		public FileStream fs;

		public FileInfo finfo;

        private string file_name;

		private string file_path;

		public StreamWriter sw;

        public StreamReader sr;

        File_Access_Attached_Status attached_status = File_Access_Attached_Status.FAAS_None;


        public File_Access()
        {
            fs      = null;
            finfo   = null;
            sw      = null;
            sr      = null;
        }

        public long Get_Length()
        {
            if (finfo != null)
            {
                finfo.Refresh();
                return finfo.Length;
            }

            return 0;
        }

        public void Flush()
        {
            try
            {
                if (sw != null && attached_status == File_Access_Attached_Status.FAAS_StreamWriter)
                {
                    sw.Flush();
                }
                else
                {
                    fs.Flush();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("flush failed" + e.Message);
            }
        }

        public bool File_Exists(string ext_abs_file_path)
        {
            return File.Exists(ext_abs_file_path);
        }

		public void Attach_StreamWriter()
		{
			try
			{
                if (attached_status == File_Access_Attached_Status.FAAS_None)
                {
                    sw = new StreamWriter(fs);

                    attached_status = File_Access_Attached_Status.FAAS_StreamWriter;
                }
			}
			catch (Exception e)
			{
                //Console.WriteLine("attach_StreamWriter failed..");
                //Console.WriteLine(e.Message);
			}
		}

        public void Attach_StreamReader()
        {
            try
            {
                if (attached_status == File_Access_Attached_Status.FAAS_None)
                {
                    sr = new StreamReader(fs);

                    attached_status = File_Access_Attached_Status.FAAS_StreamReader;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("attach_StreamReader failed..");
                //Console.WriteLine(e.Message);
            }
        }

        /** file FileMode and FileAccess*/
		public void Create_File(string ext_abs_file_path)
		{
			try
			{
                fs = new FileStream(ext_abs_file_path,FileMode.Create,FileAccess.Write,FileShare.ReadWrite);

                if (fs != null)
                {
                    finfo = new FileInfo(ext_abs_file_path);

                    file_name = finfo.Name;

                    file_path = finfo.Directory.ToString();
                }
			}
			catch (Exception e)
			{
                //Console.WriteLine("create_File failed..");
                //Console.WriteLine(e.Message);
			}
		}

        //untested
        public void Append_To_File(string ext_abs_file_path)
        {
            try
            {
                finfo = new FileInfo(ext_abs_file_path);

                file_name = finfo.Name;

                file_path = finfo.Directory.ToString();

                fs = new FileStream(file_path + "\\" + file_name, FileMode.Append,FileAccess.Write,FileShare.ReadWrite);
                
            }
            catch (Exception e)
            {
                //Console.WriteLine("append_to_File failed..");
                //Console.WriteLine(e.Message);
            }
        }

        public void Update_File(string ext_abs_file_path)
        {
            try
            {
                finfo = new FileInfo(ext_abs_file_path);

                file_name = finfo.Name;

                file_path = finfo.Directory.ToString();

                fs = new FileStream(file_path + "\\" + file_name, FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);

            }
            catch (Exception e)
            {
                //Console.WriteLine("update_File failed..");
                //Console.WriteLine(e.Message);
            }
        }

		public void Open_File(string ext_abs_file_path)
		{
			try
			{
				finfo = new FileInfo(ext_abs_file_path);

				file_name = finfo.Name;

				file_path = finfo.Directory.ToString();

				fs = new FileStream(file_path + "\\" + file_name,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
			}
			catch (Exception e)
			{
                //Console.WriteLine("open_File failed..");
                //Console.WriteLine(e.Message);
			}
		}

        public bool Is_Valid()
        {
            if (fs != null)
                return true;
            else
                return false;
        }

        public void Close_File()
        {
            try
            {
                if (sr != null && attached_status == File_Access_Attached_Status.FAAS_StreamReader)
                {
                    sr.Close();
                    sr = null;
                    attached_status = File_Access_Attached_Status.FAAS_None;
                }
                else
                    if (sw != null && attached_status == File_Access_Attached_Status.FAAS_StreamWriter)
                    {
                        sw.Flush();
                        sw.Close();
                        
                        sw = null;
                        attached_status = File_Access_Attached_Status.FAAS_None;
                    }
                    else
                    {
                        fs.Flush();
                        fs.Close();
                        
                        fs = null;
                    }

                file_name = "";
                file_path = "";
                finfo = null;
            }
            catch (Exception e)
            {
                //Console.WriteLine("close_File failed..");
                //Console.WriteLine(e.Message);
            }
        }


        /*******************************************************************/

		public void Write_Line_String(string ext_message)
		{
			try
			{
				sw.WriteLine(ext_message);

				
			}
			catch (Exception e)
			{
                //Console.WriteLine("write_Line_String failed..");
                //Console.WriteLine(e.Message);
			}
		}

        public void Write_String(string ext_message)
        {
            try
            {
                sw.Write(ext_message);

                
            }
            catch (Exception e)
            {
                //Console.WriteLine("write_String failed..");
                //Console.WriteLine(e.Message);
            }
        }

        public string Read_Line_To_String()
        {
            try
            {
                return sr.ReadLine();
            }
            catch (Exception e)
            {
                //Console.WriteLine("read_Line_To_String failed..");
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        /*untested*/
        public string Read_To_String(long limit)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                int next;
                long count = 0;

                while ( (next = sr.Read()) != -1 && count<limit)
                {
                    sb.Append( ((char)next).ToString() );

                    count++;
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                //Console.WriteLine("read_To_String failed..");
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        public string Read_To_End()
        {
            try
            {
                return sr.ReadToEnd();
            }
            catch(Exception e)
            {
                //Console.WriteLine("read_To_End failed..");
                //Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Write_Byte(Byte ext_byte)
        {
            try
            {
                fs.WriteByte(ext_byte);
            }
            catch (Exception e)
            {
                //Console.WriteLine("write_Byte failed..");
                //Console.WriteLine(e.Message);
            }
        }

		public void Write_Bytes(Byte[] ext_byte_array, int ext_offset, int ext_count)
        {
			try
			{
                fs.Write(ext_byte_array, ext_offset, ext_count);
			}
			catch (Exception e)
			{
                //Console.WriteLine("write_Bytes failed..");
                //Console.WriteLine(e.Message);
			}
		}

        public int Read_File_To_Byte_Array(Byte[] ext_byte_array, int ext_offset, int ext_count)
        {
            try
            {
                return fs.Read(ext_byte_array, ext_offset, ext_count);
            }
            catch (Exception e)
            {
                //Console.WriteLine("read_File_To_Byte_Array failed..");
                //Console.WriteLine(e.Message);
                return -1;
            }
        }

        public int Read_File_To_Byte()
        {
            try
            {
                return fs.ReadByte();
            }
            catch (Exception e)
            {
                //Console.WriteLine("read_File_To_Byte failed..");
                //Console.WriteLine(e.Message);
                return -1;
            }
        }

		public long Seek(long ext_offset,SeekOrigin status)
		{
            try
            {
                return fs.Seek(ext_offset, status);
            }
            catch(Exception e)
            {
                //Console.WriteLine("seek failed..");
                //Console.WriteLine(e.Message);
                return -1;
            }
		}

        public long Get_Position()
        {
            try
            {
                return fs.Position;
            }
            catch (Exception e)
            {
                //Console.WriteLine("get_Position failed..");
                //Console.WriteLine(e.Message);
                return -1;
            }
        }

        public bool EOF()
        {
            if(Get_Position() == Get_Length())
            {
                return true;
            }

            return false;
        }
	}
}