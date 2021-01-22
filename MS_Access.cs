using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Data.OleDb;

namespace Database_1_0
{
    public class MS_Access_Db_Client : Db_Client
    {
        private OleDbConnection db_con;

        public MS_Access_Db_Client()
        {
            db_con = null;
        }

        public bool connect_To_Server()
        {

            try
            {
                string con_str = 
                //"server=" + server_name + ";user=" + user_name + ";database=" + database_name + ";port=" + port + ";password=" + password + ";logging=true;connect timeout=" + connect_timeout + ";";
                "Provider=Microsoft.ACE.OLEDB.12.0;"+
                "Data Source="+database_name;
                
                Console.WriteLine("closing previous connection ?");

                close_Connection();

                Console.WriteLine("connecting to server..");

                db_con = new OleDbConnection(con_str);

                db_con.Open();

                Console.WriteLine(user_name + " " + "successfully connected to " + " @" + server_name + ":" + port);
                if (database_name.Length > 0)
                {
                    Console.WriteLine("current database: " + database_name);
                }
                //else
                //    if (database_file_name.Length > 0)
                //    {
                //        Console.WriteLine("current database file: " + database_file_name);
                //    }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("connection to server failed!");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool connect_To_Excel_File(string sheet_name, string ext_excel_file_path)
        {

            try
            {
                
                string con_str = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ext_excel_file_path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";

                Console.WriteLine("closing previous connection ?");

                close_Connection();

                Console.WriteLine("connecting to server..");

                db_con = new OleDbConnection(con_str);

                db_con.Open();

                Console.WriteLine(user_name + " " + "successfully connected to " + " @" + sheet_name + ":" + ext_excel_file_path);
                if (sheet_name.Length > 0)
                {
                    Console.WriteLine("current database: " + sheet_name);
                }
                //else
                //    if (database_file_name.Length > 0)
                //    {
                //        Console.WriteLine("current database file: " + database_file_name);
                //    }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("connection to server failed!");
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void close_Connection()
        {
            try
            {
                Console.WriteLine("disconnecting database connection..");

                if (db_con != null)
                {
                    db_con.Close();
                    Console.WriteLine("disconnection successful!");
                    return;
                }

                Console.WriteLine("no connection to close!");
            }
            catch (Exception e)
            {
                Console.WriteLine("closing the Database connection failed!");
                Console.WriteLine(e.Message);
                return;
            }
        }

        public void delete_Table(string table_name)
        {
            if (db_con != null)
            {
                try
                {
                    Console.WriteLine("deleting all rows from " + table_name + "..");

                    OleDbCommand  command = db_con.CreateCommand();


                    command.CommandText = "DELETE FROM " + table_name + ";";

                    command.ExecuteNonQuery();

                    Console.WriteLine(table_name + " table rows deleted successful.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(table_name + " table deletion failed!");
                    Console.WriteLine(e.Message);
                    return;
                }


            }
        }

        

        DataTable static_Query(string sql_statement)
        {
            try
            {
                OleDbCommand  cmd = new OleDbCommand (sql_statement, db_con);

                //OleDbDataReader rdr = cmd.ExecuteReader();
                OleDbDataAdapter adp = new System.Data.OleDb.OleDbDataAdapter(sql_statement, db_con);
                DataTable dt = new DataTable();

                adp.Fill(dt);


                //int total_columns = dt.Columns.Count;//rdr.FieldCount;

                

                //int col_count = 0;
                //while (col_count < total_columns)
                //{
                //    DataColumn dc = new DataColumn();
                //    dc.ColumnName = dt.Columns[col_count].ColumnName;//rdr.GetName(col_count);

                //    dt.Columns.Add(dc);

                //    col_count++;
                //}



                //while (rdr.Read())
                //{
                //    DataRow row = dt.NewRow();

                //    int col = 0;
                //    while (col < total_columns)
                //    {
                //        row[col] = rdr[col];

                //        col++;
                //    }

                //    dt.Rows.Add(row);
                //}

                if (dt.Rows.Count > 0)
                {
                    Console.WriteLine("\n" + dt.Rows.Count + " row(s) affected");
                }

                Console.WriteLine(dt.Rows.Count + " row(s) returned");

                //always close after using datareader
                //adp.Close();

                //display_Table_To_Console(dt);

                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("static_Query failed..");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        object static_Scalar(string sql_statement)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(sql_statement, db_con);

                object result = null;
                result = cmd.ExecuteScalar();

                if (result != null)
                {
                    Console.WriteLine("1 row returned");
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("static_Scalar failed..");
                Console.WriteLine(e.Message);

                //failed operation
                return null;
            }
        }

        int static_Non_Query(string sql_statement)
        {
            try
            {
                
                OleDbCommand  cmd = new OleDbCommand (sql_statement, db_con);

                int result = cmd.ExecuteNonQuery();

                Console.WriteLine("\n" + result + " row(s) affected");

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("static_Non_Query failed..");
                Console.WriteLine(e.Message);

                //failed operation
                return -1;
            }
        }

        public Query_Result static_Process_Query(string sql_statement)
        {
            try
            {
                //string lower_sql_statement = sql_statement.ToLower();
                string lower_sql_statement = sql_statement;

                if (contains_Query(lower_sql_statement) != null)
                {
                    if (is_Scalar_Query(lower_sql_statement))
                    {
                        //need way to set query result type
                        return new Query_Result(static_Scalar(lower_sql_statement));
                    }
                    //else
                    {
                        return new Query_Result(static_Query(lower_sql_statement));
                    }
                }
                else
                    if (contains_Non_Query(lower_sql_statement) != null)
                    {
                        return new Query_Result(static_Non_Query(lower_sql_statement));
                    }

                Console.WriteLine("failed to process statement:\t" + '"' + sql_statement + '"');
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("static_Process_Query failed..");
                Console.WriteLine(e.Message);

                return null;
            }
        }



    }

}