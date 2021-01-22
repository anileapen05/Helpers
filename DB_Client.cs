using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database_1_0
{
    public class Db_Client
    {
        //public string database_path;
        //public string database_file_name;

        public string server_name;
        public string user_name;
        public string database_name;
        public string port;
        public string password;
        public string connect_timeout;

        static List<string> sub_query_list = new List<string> { 
                                                                "where"
                                                              };

        static List<string> function_list = new List<string> { 
                                                                "count(",
                                                                "database(",
                                                                "found_rows(",
                                                                "last_insert_id(",
                                                                "row_count(",
                                                                "schema(",
                                                                "analyse(",
                                                                "benchmark(",
                                                                "database("
                                                             };

        static List<string> query_list = new List<string>  {
                                                                "select",
                                                                "show",
                                                                "describe",
                                                                "explain",
                                                                "help"
                                                           };

        static List<string> non_query_list = new List<string> { 
                                                                "alter",                                            
                                                                "create" ,
                                                                "drop",
                                                                "use", 
                                                                "rename",
                                                                "start transaction",
                                                                "commit",
                                                                "do",
                                                                "insert",
                                                                "delete",
                                                                "update",
                                                                "handler",
                                                                "load data infile",
                                                                "release savepoint",
                                                                "replace",
                                                                "rollback",
                                                                "rollback to savepoint",
                                                                "savepoint",
                                                                "set",
                                                                "truncate",
                                                                "xa",
                                                              };

        public Db_Client()
        {
            //database_path = "";
            //database_file_name = "";
            server_name = "";
            user_name = "";
            database_name = "";
            port = "";
            password = "";
            connect_timeout = ""; //seconds
        }

        public void display_Table_To_Console(DataTable ext_dt)
        {
            try
            {
                Console.WriteLine("table has " + ext_dt.Columns.Count + " columns and " + ext_dt.Rows.Count + " rows");

                Console.WriteLine();

                foreach (DataColumn col in ext_dt.Columns)
                {
                    Console.Write(col + " | ");
                }

                foreach (DataRow row in ext_dt.Rows)
                {
                    Console.WriteLine();
                    int col_count = 0;
                    while (col_count < ext_dt.Columns.Count)
                    {
                        Console.Write(row.ItemArray[col_count] + " | ");
                        col_count++;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Table display failed!");
                Console.WriteLine(e.Message);
                return;
            }

        }

        protected string contains_Query(string lower_sql_statement)
        {

            foreach (string query in query_list)
            {
                if (lower_sql_statement.ToLower().Contains(query))
                {
                    return query;
                }
            }

            return null;
        }

        protected string contains_Non_Query(string lower_sql_statement)
        {


            foreach (string non_query in non_query_list)
            {
                if (lower_sql_statement.ToLower().Contains(non_query))
                {
                    return non_query;
                }
            }

            return null;
        }

        protected bool is_Scalar_Query(string lower_sql_statement)
        {
            string function = contains_Function(lower_sql_statement);

            if (function != null)
            {
                string sub_query = contains_SubQuery(lower_sql_statement);
                if (sub_query != null)
                {
                    if (function_Before_SubQuery(lower_sql_statement, function, sub_query))
                    {
                        return true;//function before subquery
                    }
                    else
                    {
                        return false;//function in subquery
                    }
                }
                else
                    return true;//function only no subquery
            }
            else
                return false;//no function no subquery
        }

        private string contains_SubQuery(string lower_sql_statement)
        {


            foreach (string sub_query in sub_query_list)
            {
                if (lower_sql_statement.ToLower().Contains(sub_query)) //||
                {
                    return sub_query;
                }
            }

            return null;
        }

        private string contains_Function(string lower_sql_statement)
        {
            foreach (string function in function_list)
            {
                if (lower_sql_statement.ToLower().Contains(function)) //||
                {
                    return function;
                }
            }

            return null;
        }

        private bool function_Before_SubQuery(string lower_sql_statement, string function, string sub_query)
        {
            if (lower_sql_statement.ToLower().IndexOf(function) < lower_sql_statement.ToLower().IndexOf(sub_query))
            {
                return true;
            }
            return false;
        }

    }

}