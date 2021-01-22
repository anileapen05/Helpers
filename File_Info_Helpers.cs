using System;
using System.Collections.Generic;
using System.IO;

using IO_1_0;

namespace Helpers_1_0
{
    class File_Info_Helpers
    {
        private static FileInfo file_info;

        public static void access_File(string ext_abs_path)
        {
            file_info = new FileInfo(ext_abs_path);
        }

        public static long get_Size()
        {
             return file_info.Length;
        }

        public static string get_Directory()
        {
            return file_info.Directory.ToString();
        }

        public static string get_Name()
        {
            return file_info.Name;
        }

        public static string get_File_Name(string ext_file_name)
        {
            try
            {
                if (ext_file_name.Length>0)
                {
                    string[] segments;

                    if ((segments = ext_file_name.Split('/')) != null && (ext_file_name.Split('/').Length - 1 >= 3))
                    {
                        if (segments[segments.Length - 1].Split('.').Length >= 2)//filename "navbarCSS-global-min-1710585973._V1_.css"
                        {
                            return segments[segments.Length - 1];
                        }
                    }

                    if ((segments = ext_file_name.Split('\\')) != null && (ext_file_name.Split('\\').Length - 1 >= 3))
                    {
                        if (segments[segments.Length - 1].Split('.').Length >= 2)//filename "navbarCSS-global-min-1710585973._V1_.css"
                        {
                            return segments[segments.Length - 1];
                        }
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                //Console.WriteLine("get_File_Name failed.." + e.Message);
                return null;
            }
        }

        public static string get_File_Name_Extension(string ext_file_name)
        {
            try
            {
                if (ext_file_name.Length>0)
                {
                    string file_name = get_File_Name(ext_file_name);
                    if (file_name != null)
                    {
                        string[] file_name_segments;
                        if ((file_name_segments = file_name.Split('.')) != null)
                        {
                            if (file_name_segments.Length > 0)
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
                //Console.WriteLine("get_File_Name_Extension failed.." + e.Message);
                return null;
            }
        }

        public static long get_Size_Diff(string file1,string file2)
        {
            long diff = 0;

            File_Info_Helpers.access_File(file1);
            long s1 = File_Info_Helpers.get_Size();

            File_Info_Helpers.access_File(file2);
            long s2 = File_Info_Helpers.get_Size();

            diff = s1 - s2;
            
            return diff;
        }

        public static bool is_Diff(string file1, string file2)
        {
            File_Access f1 = new File_Access();
            f1.open_File(file1);

            File_Access f2 = new File_Access();
            f2.open_File(file2);

            if (get_Size_Diff(file1, file2) == 0)//file length diff 0
            {
                do
                {
                    int bf1 = f1.read_File_To_Byte();
                    int bf2 = f2.read_File_To_Byte();
                    if (bf1 != bf2)
                    {
                        f1.close_File();
                        f2.close_File();
                        return true;//both diff
                    }
                }
                while (f1.get_Position() < f1.finfo.Length);

                f1.close_File();
                f2.close_File();
                return false;//both same
            }

            f1.close_File();
            f2.close_File();
            return true;//both diff
        }
    }
}
