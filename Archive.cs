using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

using Helpers_1_0;

namespace IO_1_0
{
    class Archive_Entry_Info
    {
        public string name;
        public string dir_name;
        public string path;
        public long compressed_length;
        public long uncompressed_length;
        public bool is_directory = false;
        public DateTime last_modified;
        public int level = 0;
    }

    class Archiver
    {
        ~Archiver()
        {
            close_Archive();
        }
        private ZipArchive archive;

        public List<Archive_Entry_Info> result_aei_list;

        private File_Access file = new File_Access();

        public void create(string abs_file_path)
        {
            try
            {
                file.create_File(abs_file_path);

                archive = new ZipArchive(file.fs, ZipArchiveMode.Create);

            }
            catch (Exception e)
            {
                Console.WriteLine("create failed!.." + e.Message);
            }
        }

        public void read(string abs_file_path)
        {
            try
            {
                file.open_File(abs_file_path);

                archive = new ZipArchive(file.fs, ZipArchiveMode.Read);
            }
            catch (Exception e)
            {
                Console.WriteLine("read failed!.." + e.Message);
            }
        }

        public void update(string abs_file_path)
        {
            try
            {
                file.update_File(abs_file_path);

                archive = new ZipArchive(file.fs, ZipArchiveMode.Update);

            }
            catch (Exception e)
            {
                Console.WriteLine("update failed!.." + e.Message);
            }
        }

        public void close_Archive()
        {
            try
            {
                archive.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine("close_Archive failed!");
                Console.WriteLine(e.Message);
            }
            
        }

        public void get_Entries(string ext_dir_name,int ext_dir_level)
        {
            try
            {
                List<Archive_Entry_Info> aei_list = new List<Archive_Entry_Info>();

                foreach(ZipArchiveEntry entry in archive.Entries)
                {
                    Archive_Entry_Info aei = new Archive_Entry_Info();

                    //dir
                    //if (entry.FullName.StartsWith(ext_dir_name) && !Path.HasExtension(entry.FullName) && entry.FullName.Split(new char[] { '/' }).Length - 2 == ext_dir_level)
                    if (entry.FullName.StartsWith(ext_dir_name) && entry.Name.Length==0 && entry.FullName.Split(new char[] { '/' }).Length - 2 == ext_dir_level)
                    {
                        aei.is_directory = true;
                        aei.name = Path_Helpers.get_Child_Dir(entry.FullName, new string[] { "/" });
                        aei.dir_name = ext_dir_name;
                        aei.path = entry.FullName;
                        aei.last_modified = entry.LastWriteTime.DateTime;
                        aei.level = ext_dir_level;


                        aei_list.Add(aei);
                    }
                    else//file
                        //if (entry.FullName.StartsWith(ext_dir_name) && Path.HasExtension(entry.FullName) && entry.FullName.Split(new char[] { '/' }).Length - 1 == ext_dir_level)
                        if (entry.FullName.StartsWith(ext_dir_name) && entry.Name.Length>0 && entry.FullName.Split(new char[] { '/' }).Length - 1 == ext_dir_level)
                    {
                        aei.is_directory = false;
                        aei.name = entry.Name;
                        aei.dir_name = ext_dir_name;
                        aei.path = entry.FullName;
                        aei.compressed_length = entry.CompressedLength;
                        aei.uncompressed_length = entry.Length;
                        aei.last_modified = entry.LastWriteTime.DateTime;
                        aei.level = ext_dir_level;


                        aei_list.Add(aei);
                    }

                    
                }//foreach archive_entry

                if (aei_list.Count > 0)
                {
                    result_aei_list = new List<Archive_Entry_Info>(aei_list);
                }
                else
                {
                    result_aei_list = new List<Archive_Entry_Info>();
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Entries failed!.." + e.Message);

                
            }
        }

        //untested
        public void get_Entries()
        {
            try
            {
                List<Archive_Entry_Info> aei_list = new List<Archive_Entry_Info>();

                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Archive_Entry_Info aei = new Archive_Entry_Info();

                    //dir
                    if (entry.Name.Length == 0)
                    {
                        aei.is_directory = true;
                        aei.name = Path_Helpers.get_Child_Dir(entry.FullName, new string[] { "/" });
                        aei.dir_name = entry.Name;
                        aei.path = entry.FullName;
                        aei.last_modified = entry.LastWriteTime.DateTime;
                        aei.level = entry.FullName.Split(new char[] { '/' }).Length - 2;


                        aei_list.Add(aei);
                    }
                    else//file
                        if (entry.Name.Length > 0)
                        {
                            aei.is_directory = false;
                            aei.name = entry.Name;
                            aei.dir_name = Path_Helpers.get_Parent_Dir(entry.FullName, new string[] { "/" }); ;
                            aei.path = entry.FullName;
                            aei.compressed_length = entry.CompressedLength;
                            aei.uncompressed_length = entry.Length;
                            aei.last_modified = entry.LastWriteTime.DateTime;
                            aei.level = entry.FullName.Split(new char[] { '/' }).Length - 1;


                            aei_list.Add(aei);
                        }


                }//foreach archive_entry

                if (aei_list.Count > 0)
                {
                    result_aei_list = new List<Archive_Entry_Info>(aei_list);
                }
                else
                {
                    result_aei_list = new List<Archive_Entry_Info>();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("get_Entries failed!.." + e.Message);
            }
        }

        public void display_All_Entries(string abs_file_path)
        {
            try
            {
                this.read(abs_file_path);
                
                int count = 0;
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                
                    Console.WriteLine(entry.FullName);
                    count++;
                }//foreach archive_entry

                this.close_Archive();

                Console.WriteLine("Total entries: "+count.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("display_All_Entries failed!.." + e.Message);
            }
        }

        public void delete_File(string archive_entry_full_name)
        {
            try
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName == archive_entry_full_name)
                    {
                        entry.Delete();
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("delete_File failed.."+e.Message);
            }
            
        }

        public void delete_Directory(string abs_dir_full_name)
        {
            try
            {
                ZipArchiveEntry match = null;
                bool not_found = false;

                do
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.FullName.StartsWith(abs_dir_full_name))
                        {
                            match = entry;
                            break;
                        }

                    }

                    if (match == null)
                    {
                        not_found = true;//no matches end search  and exit
                    }
                    else
                    {
                        not_found = false;//match found, continue with search for others, after deleting this one
                        match.Delete();
                        match = null;
                    }

                }
                while (not_found == false);//not_found is false..there is a match in the collection
            }
            catch(Exception e)
            {
                Console.WriteLine("delete_Directory failed.."+e.Message);
            }

            
        }

        public void add_Directory(string abs_dir_path, System.IO.Compression.CompressionLevel comp_level)
        {
            try
            {

                //add dir
                archive.CreateEntry(abs_dir_path,comp_level);

            }
            catch (Exception e)
            {
                Console.WriteLine("add_Directory failed!.." + e.Message);
            }
        }

        public void add_File(string abs_file_path, string entry_full_name, System.IO.Compression.CompressionLevel comp_level)
        {
            try
            {
                //add file
                archive.CreateEntryFromFile(abs_file_path, entry_full_name, comp_level);
            }
            catch (Exception e)
            {
                Console.WriteLine("add_File failed!.." + e.Message);
            }
        }

        public void extract_File(string archive_entry_full_name,string destination_abs_file_name,bool overwrite)
        {
            try
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName == archive_entry_full_name)
                    {
                        Directory.CreateDirectory(destination_abs_file_name.Replace( entry.Name,""));
                        entry.ExtractToFile(destination_abs_file_name, overwrite);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("extract_File failed.." + e.Message);
            }
        }

        public void extract_Directory(string abs_dir_full_name, string destination_abs_folder, bool overwrite)
        {
            try
            {
                
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.StartsWith(abs_dir_full_name))
                    {
                        //if file
                        if (Path.HasExtension(entry.FullName))
                        {
                            Directory.CreateDirectory(destination_abs_folder+"\\"+abs_dir_full_name.Replace("/","\\"));
                            //extract file
                            extract_File(entry.FullName,
                                destination_abs_folder + "\\" + entry.FullName.Replace("/","\\"),
                                overwrite);
                        }
                        else//if folder
                        {
                            //extract folder
                            Directory.CreateDirectory(destination_abs_folder + "\\" + entry.FullName.Replace("/", "\\"));
                        }

                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine("extract_Directory failed.." + e.Message);
            }
        }

        public bool extract_Archive(string destination_abs_folder)
        {
            try
            {
                archive.ExtractToDirectory(destination_abs_folder);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("extract_To_Folder failed.."+e.Message);
                return false;
            }
        }

        public ZipArchiveEntry get_Entry(string entry_name)
        {
            ZipArchiveEntry entry = null;
            
            entry = archive.GetEntry(entry_name);
            
            return entry;
            
        }

        public long get_Uncompressed_Length()
        { 
            long size = 0;

            foreach(ZipArchiveEntry entry in archive.Entries)
            {
                size+=entry.Length;
            }

            return size;
        }


        
    }
}
