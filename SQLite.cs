using System;
using System.Data;
using System.Collections.Generic;
using System.IO;

using System.Data.SQLite;



namespace Database_1_0
{
    public class SQLite_Db_Client
    {
        private SQLiteConnection db_con; 

        public SQLite_Db_Client()
        {
            db_con = null;
        }

        public bool connect_To_Database(string database_path,string database_file_name,string user_name,string server_name)
        {
            try
            {
                if(!Directory.Exists(database_path))
                {
                    Console.WriteLine("Invalid database path!");
                    return false;
                }

                if(!File.Exists(
                    database_path+"\\"+
                    database_file_name))
                {
                    Console.WriteLine("Invalid database file!");
                    return false;
                }

                string con_str = "data source= " + database_path + "\\" + database_file_name;

                Console.WriteLine("closing previous connection ?");

                close_Connection();

                Console.WriteLine("connecting to server..");

                db_con = new SQLiteConnection(con_str);

                db_con.Open();

                Console.WriteLine(user_name + " " + "successfully connected to " + " @" + server_name);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("connect_To_Database failed!");
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

                    SQLiteCommand command = db_con.CreateCommand();


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

        public DataTable static_Query(string sql_statement)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql_statement, db_con);

                SQLiteDataReader rdr = cmd.ExecuteReader();

                int total_columns = rdr.FieldCount;

                DataTable dt = new DataTable();

                int col_count = 0;
                while (col_count < total_columns)
                {
                    DataColumn dc = new DataColumn();
                    dc.ColumnName = rdr.GetName(col_count);

                    dt.Columns.Add(dc);

                    col_count++;
                }



                while (rdr.Read())
                {
                    DataRow row = dt.NewRow();

                    int col = 0;
                    while (col < total_columns)
                    {
                        row[col] = rdr[col];

                        col++;
                    }

                    dt.Rows.Add(row);
                }

                if (rdr.RecordsAffected > 0)
                {
                    Console.WriteLine("\n" + rdr.RecordsAffected + " row(s) affected");
                }

                Console.WriteLine(dt.Rows.Count + " row(s) returned");

                //always close after using datareader
                rdr.Close();

                display_Table_To_Console(dt);

                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine("static_Query failed..");
                Console.WriteLine(e.Message);

                return null;
            }
        }

        public int static_Non_Query(string sql_statement)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql_statement, db_con);

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

        public object static_Scalar(string sql_statement)
        {
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql_statement, db_con);

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