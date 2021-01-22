using System;
using System.Collections.Generic;
using System.Data;

using IO_1_0;

namespace Helpers_1_0
{
    public static class CSV_Helpers
    {
        public static DataTable csv_To_DataTable(string ext_abs_filename, string seperator)
        {
            try
            {
                File_Access file_access = new File_Access();

                file_access.Open_File(ext_abs_filename);

                file_access.Attach_StreamReader();

                DataTable dt = new DataTable();

                string line;
                while ((line = file_access.Read_Line_To_String()) != null)
                {
                    string[] row = line.Split(new string[] { seperator }, StringSplitOptions.None);

                    if (dt.Columns.Count == 0)
                    {
                        foreach (string col in row)
                        {
                            dt.Columns.Add(col);
                        }
                    }
                    else
                    {
                        dt.Rows.Add(row);
                    }
                }

                file_access.Close_File();

                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("csv_To_DataTable failed");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public static bool datatable_To_CSV_File(DataTable ext_dt, string ext_abs_filename, string seperator, string font = "windows")
        {
            try
            {
                File_Access file_access = new File_Access();

                file_access.Create_File(ext_abs_filename);

                file_access.Attach_StreamWriter();

                foreach (DataColumn col in ext_dt.Columns)
                {
                    if (col.Ordinal < ext_dt.Columns.Count - 1)
                    {
                        file_access.Write_String(String_Helpers.utf7_Decode(col.ColumnName, font)
                            + seperator);
                    }
                    else
                    {
                        file_access.Write_String(String_Helpers.utf7_Decode(col.ColumnName, font)
                            + "\n");
                    }
                }

                



                foreach (DataRow row in ext_dt.Rows)
                {
                    int cell_count = 0;
                    foreach (object cell in row.ItemArray)
                    {
                        if (cell_count < ext_dt.Columns.Count - 1)
                        {
                            file_access.Write_String(String_Helpers.utf7_Decode(cell.ToString(), font)
                                + seperator);
                        }
                        else
                        {
                            file_access.Write_String(String_Helpers.utf7_Decode(cell.ToString(), font)
                                + "\n");
                        }

                        cell_count++;
                    }

                }

                file_access.Close_File();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("datatable_To_CSV_File failed");
                Console.WriteLine(e.Message);

                return false;
            }
        }
    }
}
