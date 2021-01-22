using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Helpers_1_0
{

    class String_Helpers
    {
        static Dictionary<string, string> language_encodings = new Dictionary<string, string>() {
                                                                                            {"spanish","ISO-8859-1"},
                                                                                            {"windows","Windows-1252"},
                                                                                            
                                                                                         };
        public static string replace_in_String(string ext_string,
                                        string[] ext_target_strings,
                                      string[] ext_replace_strings)
        {
            try
            {
                foreach (string target in ext_target_strings)
                {
                    int target_index = ext_target_strings.ToList().IndexOf(target);

                    string replace = ext_replace_strings[target_index];

                    ext_string = ext_string.Replace(target, replace);
                }

                return ext_string;
            }
            catch (Exception e)
            {
                Console.WriteLine("replace_in_String failed.." + e.Message);
                return null;
            }
        }
        public static void display_With_NewLine_To_Console(string[] ext_string_array)
        {
            try
            {
                foreach (string s in ext_string_array)
                {
                    Console.WriteLine(s);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("display_With_NewLine_To_Console failed.."+e.Message);
            }
        }

        public static string unescape(string ext_string)
        {
            try
            {
                return Regex.Unescape(ext_string);
            }
            catch(Exception e)
            {
                Console.WriteLine("unescape failed.."+e.Message);
                return null;
            }
        }

        public static string utf8_Decode(string ext_string,string language)
        {
            try
            {
                byte[] tempBytes = System.Text.Encoding.GetEncoding(language_encodings[language]).GetBytes(ext_string);
                string temp = System.Text.Encoding.UTF8.GetString(tempBytes);

                if (temp == null)
                {
                    Console.WriteLine("utf8_Decode: null string!");
                }
                
                return temp;
            }
            catch(Exception e)
            {
                Console.WriteLine("utf8_Decode failed.." + e.Message);
                return null;
            }
        }

        public static string utf7_Decode(string ext_string, string language)
        {
            try
            {
                byte[] tempBytes = System.Text.Encoding.GetEncoding(language_encodings[language]).GetBytes(ext_string);
                string temp = System.Text.Encoding.UTF7.GetString(tempBytes);

                if (temp == null)
                {
                    Console.WriteLine("utf7_Decode: null string!");
                }

                return temp;
            }
            catch (Exception e)
            {
                Console.WriteLine("utf7_Decode failed.." + e.Message);
                return null;
            }
        }

        public static string extract_Null_Terminated_UTF8_String(byte[] data)
        {
            string s = Encoding.UTF8.GetString(data);

            int pos = s.IndexOf('\0');

            return  s.Substring(0, pos);
        }

        public static string extract_Marker_Terminated_UTF8_String(byte[] data,char marker)
        {
            string s = Encoding.UTF8.GetString(data);

            int pos = s.IndexOf(marker);

            return s.Substring(0, pos);
        }

        public static byte[] get_Bytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string get_String(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

                
    }

    
}
