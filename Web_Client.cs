using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.ComponentModel;

using Helpers_1_0;


namespace NetWorking_1_0
{
    public class Web_Client
    {
        WebClient client = new WebClient();

        Uri uri;
        
        public Web_Client()
        {
            //async string download
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCallback);

            //async data download
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);

            //async file download
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback);

            //async string download
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(UploadStringCallback);

            //async data download
            client.UploadDataCompleted += new UploadDataCompletedEventHandler(UploadDataCallback);

            //async file download
            client.UploadFileCompleted += new UploadFileCompletedEventHandler(UploadFileCallback);

        }

        ~Web_Client()
        {
            close();
        }

        public WebClient get_Client()
        {
            try
            {
                return client;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Client failed.." + e.Message);
                return null;
            }
        }

        public Uri get_Uri()
        {
            try
            {
                if (client.BaseAddress == "")
                {
                    return uri;
                }
                else
                {
                    return new Uri(client.BaseAddress + "//" + uri.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Uri failed.." + e.Message);
                return null;
            }
        }

        public void set_Base_Address(string base_address)
        {
            try
            {
                if (base_address.Length > 0)
                {
                    client.BaseAddress = Networking_Helpers.create_URI(base_address).ToString();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("set_Base_Address failed.." + e.Message);
            }
        }

        public String get_Base_Address()
        {
            try
            { 
                return client.BaseAddress;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Base_Address failed.." + e.Message);
                return null;
            }
        }

        public Uri get_Address()
        {
            try
            {
                return new Uri(uri.ToString(),UriKind.Relative);
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Address failed.." + e.Message);
                return null;
            }
        }

        public void set_Address(string address)
        {
            try
            {
                if (client.BaseAddress == "")
                {
                    uri = Networking_Helpers.create_URI(address);
                }
                else
                    if (Networking_Helpers.create_URI(client.BaseAddress + "\\" + address) != null)
                    {
                        uri = new Uri(address, UriKind.Relative);
                    }

            }
            catch (Exception e)
            {
                Console.WriteLine("set_Address failed.." + e.Message);
            }
        }

        public void set_Cache_Policy(System.Net.Cache.RequestCacheLevel cache_level)
        {
            client.CachePolicy = new System.Net.Cache.RequestCachePolicy(cache_level);
        }

        public void set_Credentials(string username, string password, string domain)
        {
            try
            {
                client.Credentials = new NetworkCredential(username, password, domain);
            }
            catch (Exception e)
            {
                Console.WriteLine("set_Credentials failed.." + e.Message);
            }
        }

        public void set_Credentials(ICredentials ext_credentials)
        {
            try
            {
                client.Credentials = ext_credentials;
            }
            catch (Exception e)
            {
                Console.WriteLine("set_Credentials failed.." + e.Message);
            }
        }

        public ICredentials get_Credentials()
        {
            try
            {
                return client.Credentials;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Credentials failed.." + e.Message);
                return null;
            }
        }

        public void set_Connection(string address,
                                    string base_address,
                                   string username, 
                                    string password, 
                                    string domain)
        {
            try
            {
                set_Base_Address(base_address);
                set_Address(address);
                set_Credentials(username, password, domain);
            }
            catch (Exception e)
            {
                Console.WriteLine("set_Connection failed.." + e.Message);
            }
        }

        public string get_Uri_Scheme()
        {
            try
            {
                return this.get_Uri().Scheme;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Uri_Scheme failed.." + e.Message);
                return null;
            }
        }

        //untested
        public void set_Request_Header(string header, string value)
        {
            try
            {
                client.Headers.Set(header, value);
            }
            catch (Exception e)
            {
                Console.WriteLine("set_Header failed..", e.Message);
            }
        }

        public NameValueCollection get_Queries()
        {
            try
            {
                return client.QueryString;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Queries failed.." + e.Message);
                return null;
            }
        }

        public void add_Query(string name,string value)
        {
            try
            {
                client.QueryString.Add(name, value);
            }
            catch (Exception e)
            {
                Console.WriteLine("add_Query failed..", e.Message);
            }
        }

        public void cancel_Async_Request()
        {
            try
            {
                client.CancelAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("cancel_Async_Request failed.." + e.Message);
            }
        }

        //async download string
        public delegate void get_Async_Download_String(string text);

        get_Async_Download_String ext_download_string_func;

        public void async_Download_String(get_Async_Download_String ext_func)
        {
            try
            {
                this.ext_download_string_func = ext_func;
                client.DownloadStringAsync(get_Uri());
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Download_String failed.." + e.Message);
            }
        }

        public void DownloadStringCallback(Object sender,
                                            DownloadStringCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    this.ext_download_string_func((string)e.Result);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("DownloadStringCallback failed.." + exc.Message);
            }

        }

        //async download data
        public delegate void get_Async_Download_Data(byte[] data);

        get_Async_Download_Data ext_download_data_func;

        public void async_Download_Data(get_Async_Download_Data ext_func)
        {
            try
            {
                this.ext_download_data_func = ext_func;
                client.DownloadDataAsync(get_Uri());
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Download_Data failed.." + e.Message);
            }
        }

        public void DownloadDataCallback(Object sender,
                                            DownloadDataCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    this.ext_download_data_func(e.Result);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("DownloadDataCallback failed.." + exc.Message);
            }

        }

        //async download file
        //public delegate void get_Async_Download_File(Guid task_id);
        public delegate void get_Async_Download_File(string local_abs_file_name);

        get_Async_Download_File ext_file_func;
        string local_abs_file_name ="";

        public void async_Download_File(string abs_file_name,get_Async_Download_File ext_func)
        {
            try
            {
                this.ext_file_func = ext_func;
                this.local_abs_file_name = abs_file_name;

                client.DownloadFileAsync(get_Uri(),abs_file_name);
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Download_File failed.." + e.Message);
            }
        }

        public void DownloadFileCallback(Object sender,
                                            AsyncCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    //this.ext_file_func((Guid)e.UserState);
                    this.ext_file_func(this.local_abs_file_name);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("DownloadFileCallback failed.." + exc.Message);
            }

        }

        //async upload string
        //public delegate void get_Async_Upload_String(string text);
        public delegate void get_Async_Upload_String();

        get_Async_Upload_String ext_upload_string_func;

        public void async_Upload_String(string message,get_Async_Upload_String ext_func)
        {
            try
            {
                this.ext_upload_string_func = ext_func;
                client.UploadStringAsync(get_Uri(), message);
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Upload_String failed.." + e.Message);
            }
        }

        public void UploadStringCallback(Object sender,
                                            UploadStringCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    //this.ext_upload_string_func((string)e.Result);
                    this.ext_upload_string_func();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("UploadStringCallback failed.." + exc.Message);
            }

        }

        //async upload data
        public delegate void get_Async_Upload_Data();

        get_Async_Upload_Data ext_upload_data_func;

        public void async_Upload_Data(byte[] data, get_Async_Upload_Data ext_func)
        {
            try
            {
                this.ext_upload_data_func = ext_func;
                client.UploadDataAsync(get_Uri(), data);
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Upload_Data failed.." + e.Message);
            }
        }

        public void UploadDataCallback(Object sender,
                                            UploadDataCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    //this.ext_upload_data_func(e.Result);
                    this.ext_upload_data_func();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("UploadDataCallback failed.." + exc.Message);
            }

        }

        //async upload file
        //public delegate void get_Async_Upload_File(Guid task_id);
        public delegate void get_Async_Upload_File();

        get_Async_Upload_File ext_upload_file_func;

        public void async_Upload_File(string abs_file_name, get_Async_Upload_File ext_func)
        {
            try
            {
                this.ext_upload_file_func = ext_func;
                client.UploadFileAsync(get_Uri(), abs_file_name);
            }
            catch (Exception e)
            {
                Console.WriteLine("async_Upload_File failed.." + e.Message);
            }
        }

        public void UploadFileCallback(Object sender,
                                            AsyncCompletedEventArgs e
                                            )
        {
            try
            {
                if (!e.Cancelled && e.Error == null)
                {
                    //this.ext_file_func((Guid)e.UserState);
                    this.ext_upload_file_func();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("UploadFileCallback failed.." + exc.Message);
            }

        }
        
        public string download_String()
        {
            try
            {
                return client.DownloadString(get_Uri());
                
            }
            catch (Exception e)
            {
                Console.WriteLine("download_String failed.." + e.Message);

                return null;
            }
        }

        public byte[] download_Data()
        {
            try
            {
                return client.DownloadData(get_Uri());
            }
            catch(Exception e)
            {
                Console.WriteLine("download_Data failed.."+e.Message);
                return null;
            }
        }

        public void download_File(string abs_file_name)
        {
            try
            {
                client.DownloadFile(get_Uri(), abs_file_name);
            }
            catch(Exception e)
            {
                Console.WriteLine("download_File failed.." + e.Message);
            }
        }

        public byte[] upload_Data(byte[] message)
        {
            try
            {
                return client.UploadData(get_Uri(), message);
            }
            catch(Exception e)
            {
                Console.WriteLine("upload_Data failed.." + e.Message);
                return null;
            }
        }

        public string  upload_String(string message)
        {
            try
            {
                return client.UploadString(get_Uri(), message);
            }
            catch (Exception e)
            {
                Console.WriteLine("upload_String failed.." + e.Message);
                return null;
            }
        }

        public byte[] upload_File(string abs_file_name)
        {
            try
            {
                return client.UploadFile(get_Uri(), abs_file_name);
            }
            catch(Exception e)
            {
                Console.WriteLine("upload_File failed.." + e.Message);
                return null;
            }
        }

        public void close()
        {
            try
            {
                cancel_Async_Request();
            }
            catch (Exception e)
            {
                Console.WriteLine("close failed.." + e.Message);
            }
        }

        public string get_Page(string abs_dest_dir)
        {
            Uri address = get_Uri();

            string full_path = Networking_Helpers.get_File_Path(address, abs_dest_dir);

            if (full_path != null)
            {
                //download html file
                if (address.IsFile)
                {
                    if (Directory_Helpers.create_Path(full_path))
                    {
                        download_File(full_path);

                        return full_path;
                    }
                }
                else//dowload html page as string and write to file
                {
                    if (Directory_Helpers.create_Path(full_path))
                    {
                        File.WriteAllText(full_path, download_String());

                        return full_path;
                    }
                }
            }

            return null;

        }



    }
}
