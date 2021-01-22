using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace Helpers_1_0
{

    class Networking_Helpers
	{
        static List<string> page_extensions = new List<string>{
                                           "htm",
                                           "html",
                                           "asp",
                                           "aspx",
                                           "php"
                                       };
        
        public static void set_Default_Connection_Limit(int ext_limit)
		{
			try
			{
				ServicePointManager.DefaultConnectionLimit = ext_limit;
			}
			catch (Exception e)
			{
                Console.WriteLine("set_Default_Connection_Limit failed.." + e.Message);
			}
		}

		public static bool check_Valid_URI(string ext_address)
		{
				try
				{
                    //HttpUtility.UrlDecode to handle 
                    //If characters such as blanks and punctuation are passed in an HTTP stream, 
                    //they might be misinterpreted at the receiving end
                    string url_dec = decode_URL(ext_address);//WebUtility.UrlDecode(ext_address);
                    new Uri(url_dec);

                    return true;
				}
				catch (Exception e)
				{
                    Console.WriteLine("check_Valid_URI failed.." + e.Message);
					return false;
				}
		}

        public static string decode_URL(string ext_address)
        {
            try
            {
                //return HttpUtility.UrlDecode(ext_address);
                return WebUtility.UrlDecode(ext_address);
            }
            catch(Exception e)
            {
                Console.WriteLine("decode_URI failed.."+e.Message);
                return null;
            }
        }

        public static string decode_HTML(string ext_html)
        {
            try
            {
                //return HttpUtility.HtmlDecode(ext_html);
                return WebUtility.HtmlDecode(ext_html);
            }
            catch (Exception e)
            {
                Console.WriteLine("decode_HTML failed.." + e.Message);
                return null;
            }
        }

		public static Uri create_URI(string ext_address)
		{
			 try
			 {
                 if (check_Valid_URI(ext_address))
                 {
                     return new Uri(ext_address);
                 }
                 else
                     return null;

			 }
			 catch (Exception e)
			 {
                 Console.WriteLine("create_URI failed.." + e.Message);
                 return null;
			 }

		}

        public static bool is_URI_File(string ext_address)
        {
            string[] segments;

            if (check_Valid_URI(ext_address))
            {
                segments = ext_address.Split('/');

                if (segments[segments.Length - 1].Contains("."))
                {
                    return true;
                }
            }

            return false;
        }

        public static string get_File_Path(Uri ext_address,string abs_dest_dir)
        { 
            
            string full_path = null;

            if (check_Valid_URI(ext_address.ToString()))
            {
                //html file
                if (ext_address.IsFile)
                {
                    full_path = abs_dest_dir
                            + string.Join("", ext_address.Segments).Replace("/", "\\");

                    return full_path;
                }
                else//html string
                {

                    if (ext_address.Segments.Count() > 1)
                    {
                        //check if last segment is file_name
                        if (Networking_Helpers.is_URI_File(ext_address.ToString()))
                        {
                            full_path = abs_dest_dir
                            + string.Join("", ext_address.Segments).Replace("/", "\\");
                        }
                        else//create a file_name
                        {
                            full_path = abs_dest_dir + "\\"
                            + ext_address.Host + "\\"
                            + string.Join("", ext_address.Segments).Replace("/", "_") + ".html";
                        }

                    }
                    else
                    {
                        full_path = abs_dest_dir
                                + "\\" + ext_address.Host + "\\index.html";
                    }

                }
            }
              
            return full_path;
            
        }

        public static bool is_Uri_Page(string ext_address)
        {
            try
            {
                //
                if(check_Valid_URI(ext_address))
                {
                    string file_name = get_File_Name(ext_address);
                    if (file_name!=null)
                    {
                        string[] file_name_segments = file_name.Split('.');
                        if (file_name_segments!=null && file_name_segments.Length > 0)
                        {
                            string extension = file_name_segments[file_name_segments.Length - 1];

                            if (page_extensions.Contains(extension))
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine("is_Uri_Page failed.."+e.Message);
                return false;
            }
        }

        public static bool is_Uri_Link(string ext_address)
        {
            try
            {
                if(check_Valid_URI(ext_address))
                {
                     if(get_File_Name(ext_address)==null)
                     {
                         return true;
                     }
                }
                return false;
            }
            catch(Exception e)
            {
                Console.WriteLine("is_Uri_Link failed.."+e.Message);
                return false;
            }
        }

        public static string get_File_Name(string ext_address)
        {
            try
            {
                if (check_Valid_URI(ext_address))
                {
                    string[] segments;

                    if ((segments = ext_address.Split('/')) != null && (ext_address.Split('/').Length - 1 >= 3) )
                    {
                        if (segments[segments.Length - 1].Split('.').Length >= 2)//filename "navbarCSS-global-min-1710585973._V1_.css"
                        {
                            return segments[segments.Length - 1];
                        }
                    }
                }

                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_File_Name failed.."+e.Message);
                return null;
            }
        }

        public static string get_File_Name_Extension(string ext_address)
        {
            try
            {
                if (check_Valid_URI(ext_address))
                {
                    string file_name = get_File_Name(ext_address);
                    if(file_name!=null)
                    {
                        string[] file_name_segments;
                        if( (file_name_segments = file_name.Split('.'))!=null )
                        {
                            if (file_name_segments.Length>0)
                            {
                                return file_name_segments[file_name_segments.Length - 1];
                            }
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_File_Name_Extension failed.." + e.Message);
                return null;
            }
        }

        
	}

}

