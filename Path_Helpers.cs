using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Helpers_1_0
{
    class Path_Helpers
    {
        static public string get_Parent_Dir(string abs_path,string[] seperator)//,int append_repeat_count,int append_start_pos=0)
        {
            try
            {
                string[] split_string = abs_path.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                if (split_string.Length > 0)
                {
                    StringBuilder parent_dir = new StringBuilder();

                    int count = 0;
                    while (count < split_string.Length - 1)
                    {
                        parent_dir.Append(split_string[count]);
                        parent_dir.Append(seperator[0]);//, append_start_pos, append_repeat_count);
                        count++;
                    }

                    return parent_dir.ToString();
                }

                return null;
            }
            catch(Exception e)
            {
                Console.WriteLine("get_Parent_Dir failed!");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        
        static public string get_Child_Dir(string abs_path, string[] seperator)
        {
            try
            {
                string[] split_string = abs_path.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                if(split_string.Length>1)
                {
                    if(split_string[split_string.Length - 1].Contains("."))
                    {
                        if(split_string.Length>2)
                            return split_string[split_string.Length - 2];
                    }
                    
                    return split_string[split_string.Length - 1];
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("get_Child_Dir failed!");
                Console.WriteLine(e.Message);

                return null;
            }
        }
        
    }
}
