using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using NetWorking_1_0;

namespace Helpers_1_0
{
    class Web_Client_Helpers
    {
        public static long get_Content_Length(Web_Client client)
        {
            switch (client.get_Uri_Scheme())
            {
                case "http":
                    return (new HTTP_Client
                        (client.get_Base_Address(),
                        client.get_Uri().ToString(),
                        client.get_Credentials())
                        ).get_Content_Length();
                case "ftp":
                    return (new FTP_Client
                        (client.get_Base_Address(),
                        client.get_Uri().ToString(),
                        client.get_Credentials())
                        ).get_Content_Length();
                default:
                    return -1;
            }
        }
    }
}
