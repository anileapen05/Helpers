using System;
using System.Collections.Generic;
using System.IO;

namespace Helpers_1_0
{
    class Dir_Info_Helpers
    {
        Dir_Info_Helpers()
        { 
        
        }

        static private string current_dir = "";

        static public string get_Current_Directory()
        {
            return current_dir = Directory.GetCurrentDirectory();
        }

        static public List<string> listing = new List<string>();

        static public List<string> dirs = new List<string>();
        static public List<string> files = new List<string>();

        static public void clear_Listing()
        {
            try
            {
                listing.Clear();
            }
            catch(Exception e)
            {
                //Console.WriteLine("clear_Listing failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public void clear_Dirs()
        {
            try
            {
                dirs.Clear();
            }
            catch (Exception e)
            {
                //Console.WriteLine("clear_Dirs failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public void clear_Files()
        {
            try
            {
                files.Clear();
            }
            catch (Exception e)
            {
                //Console.WriteLine("clear_Files failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public void list_Dir(string abs_dir)
        {
            try
            {

                foreach (string sub_dir in Directory.GetDirectories(abs_dir))
                {
                    //list this dir
                    listing.Add(sub_dir);

                    //list the sub dir and files of dir 
                    list_Dir(sub_dir);
                }

                //list files for this dir
                foreach (string file in Directory.GetFiles(abs_dir))
                {
                    listing.Add(file);
                }

            }
            catch (System.Exception e)
            {
                //Console.WriteLine("list_Dir failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public void list_Dir(string abs_dir,ref List<string> ext_dirs,ref List<string> ext_files)
        {
            try
            {

                foreach (string sub_dir in Directory.GetDirectories(abs_dir))
                {
                    //list this dir
                    ext_dirs.Add(sub_dir);

                    //list the sub dir and files of dir 
                    list_Dir(sub_dir,ref ext_dirs,ref ext_files);
                }

                //list files for this dir
                foreach (string file in Directory.GetFiles(abs_dir))
                {
                    ext_files.Add(file);
                }

            }
            catch (System.Exception e)
            {
                //Console.WriteLine("list_Dir failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public void list_Dirs_Files(string abs_dir)
        {
            try
            {

                foreach (string sub_dir in Directory.GetDirectories(abs_dir))
                {
                    //list this dir
                    dirs.Add(sub_dir);

                    //list the sub dir and files of dir 
                    list_Dir(sub_dir, ref dirs, ref files);
                }

                //list files for this dir
                foreach (string file in Directory.GetFiles(abs_dir))
                {
                    files.Add(file);
                }

            }
            catch (System.Exception e)
            {
                //Console.WriteLine("list_Dir failed..");
                //Console.WriteLine(e.Message);
            }
        }

        static public List<string> list_Files(string abs_dir)
        {
            List<string> files = new List<string>();

            //list files for this dir
            foreach (string file in Directory.GetFiles(abs_dir))
            {
                files.Add(file);
            }

            return files;
        }

        static public Int64 get_File_Size(string abs_file)
        {
            File_Info_Helpers.access_File(abs_file);

            return File_Info_Helpers.get_Size();
        }
    }
}
