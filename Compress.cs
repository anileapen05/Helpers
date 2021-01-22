using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO_1_0;
using System.IO.Compression;

namespace IO_1_0
{
    enum Compress_Type
    {
        Deflate,
        GZip
    }

    class Compress
    {
        static public void CompressFile(string src_file_name,Compress_Type ct)
        {
            File_Access src_file = new File_Access();
            src_file.Open_File(src_file_name);

            File_Access compressedFileStream = new File_Access();
            compressedFileStream.Create_File(src_file.finfo.Name + ".cmp");

            if(ct == Compress_Type.Deflate)
            {
                using (DeflateStream compressionStream = new DeflateStream(compressedFileStream.fs,
                                                            CompressionLevel.Optimal))
                {
                    src_file.fs.CopyTo(compressionStream);
                }
            }
            else
            if (ct == Compress_Type.GZip)
            {
                using (GZipStream compressionStream = new GZipStream(compressedFileStream.fs,
                                                           CompressionLevel.Optimal))
                {
                    src_file.fs.CopyTo(compressionStream);
                }
            }

            compressedFileStream.Close_File();
            src_file.Close_File();
        }

        static public void DeCompressFile(string dest_file_name, Compress_Type ct)
        {
            File_Access dest_file = new File_Access();
            dest_file.Create_File(dest_file_name.Replace(".cmp", "")+".ucmp");

            File_Access uncompressedFileStream = new File_Access();
            uncompressedFileStream.Open_File(dest_file_name);

            if (ct == Compress_Type.Deflate)
            {
                using (DeflateStream compressionStream = new DeflateStream(uncompressedFileStream.fs,
                                                            CompressionMode.Decompress))
                                                            
                {
                    compressionStream.CopyTo(dest_file.fs);
                }
            }
            else
            if (ct == Compress_Type.GZip)
            {
                using (GZipStream compressionStream = new GZipStream(uncompressedFileStream.fs,
                                                            CompressionMode.Decompress))
                {
                    compressionStream.CopyTo(dest_file.fs);
                }
            }

            uncompressedFileStream.Close_File();
            dest_file.Close_File();
        }

        public const char EOF = '\u007F';
        public const char ESCAPE = '\\';

        static public string RunLengthEncode(string s)
        {
            try
            {
                string srle = string.Empty;
                int ccnt = 1; //char counter
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] != s[i + 1] || i == s.Length - 2) //..a break in character repetition or the end of the string
                    {
                        if (s[i] == s[i + 1] && i == s.Length - 2) //end of string condition
                            ccnt++;
                        srle += ccnt + ("1234567890".Contains(s[i]) ? "" + ESCAPE : "") + s[i]; //escape digits
                        if (s[i] != s[i + 1] && i == s.Length - 2) //end of string condition
                            srle += ("1234567890".Contains(s[i + 1]) ? "1" + ESCAPE : "") + s[i + 1];
                        ccnt = 1; //reset char repetition counter
                    }
                    else
                    {
                        ccnt++;
                    }

                }
                return srle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in RLE:" + e.Message);
                return null;
            }
        }

        static public string RunLengthDecode(string s)
        {
            try
            {
                string dsrle = string.Empty
                        , ccnt = string.Empty; //char counter
                for (int i = 0; i < s.Length; i++)
                {
                    if ("1234567890".Contains(s[i])) //extract repetition counter
                    {
                        ccnt += s[i];
                    }
                    else
                    {
                        if (s[i] == ESCAPE)
                        {
                            i++;
                        }
                        dsrle += new String(s[i], int.Parse(ccnt));
                        ccnt = "";
                    }

                }
                return dsrle;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in RLD:" + e.Message);
                return null;
            }
        }
    }
}
