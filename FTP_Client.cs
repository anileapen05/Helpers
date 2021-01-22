using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;

namespace NetWorking_1_0
{
    public class FTP_Client: Web_Client
    {
        FtpWebRequest req;

        FtpWebResponse resp;

        public FTP_Client()
        { 
        
        }

        public FTP_Client(string ext_base_address,
                            string ext_address,
                            ICredentials ext_credentials): base()
        {
            this.set_Base_Address(ext_base_address);
            this.set_Address(ext_address);
            this.set_Credentials( ext_credentials);
        }

        void prepare_Request()
        {
            req = (FtpWebRequest)WebRequest.Create(this.get_Uri());

            req.Credentials = this.get_Credentials();
        }

        public long get_Content_Length()
        {
            try
            {
                prepare_Request();   

                //Return the size of a file.
                req.Method = WebRequestMethods.Ftp.GetFileSize;

                resp = (FtpWebResponse)req.GetResponse();

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
