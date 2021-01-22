using System;
using System.Collections.Generic;
using System.Linq;

using NetWorking_1_0;

namespace Web_Crawler_1_0
{
    class Web_Crawler
    {
        public Dictionary<string,HTML_Analyser> pages = new Dictionary<string,HTML_Analyser>();

        public List<Web_Client> clients = new List<Web_Client>(1);

        public long depth = 0;

        public void reset()
        {
            try
            {
                pages.Clear();
                clients.Clear();

                depth = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine("reset failed.." + e.Message);
            }
        }

        public void setup(long num_clients)
        {
            try
            {
                long count = 0;
                while(count<num_clients)
                {
                    clients.Add(new Web_Client());
                    count++;
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("setup failed.." + e.Message);
            }
        }

        

        public string get_Site()
        {
            {
                //loop to get all pages
                //based on download options
                return null;
            }
        }

        //untested
        public Web_Client get_Free_Client()
        {
            try
            {
                foreach (Web_Client client in clients)
                {
                    if (client.get_Client().IsBusy)
                    {
                        return client;
                    }
                }

                Console.WriteLine("All clients are busy!");
                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Free_Client failed.."+e.Message);
                return null;
            }
        }
    }
}
