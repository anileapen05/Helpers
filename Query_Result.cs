using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Database_1_0
{
    public enum Query_Result_Type
    {
        Not_Set,
        Table,
        Integer,
        Scalar_Object
    }



    public class Query_Result
    {
        Query_Result_Type result_type;

        object static_query_scalar_result;

        DataTable static_query_table_result;

        int static_non_query_result;

        public Query_Result()
        {
            result_type = Query_Result_Type.Not_Set;
        }

        public Query_Result(DataTable ext_static_query_table_result)
        {
            static_query_table_result = ext_static_query_table_result;

            result_type = Query_Result_Type.Table;
        }

        public Query_Result(object ext_static_query_scalar_result)
        {
            static_query_scalar_result = ext_static_query_scalar_result;

            result_type = Query_Result_Type.Scalar_Object;
        }

        public Query_Result(int ext_static_non_query_result)
        {
            static_non_query_result = ext_static_non_query_result;

            result_type = Query_Result_Type.Integer;
        }

        public DataTable get_Static_Query_Result()
        {
            return static_query_table_result;
        }

        public object get_Scalar_Result()
        {
            return static_query_scalar_result;
        }

        public int get_Static_Non_Query_Result()
        {
            return static_non_query_result;
        }

        public Query_Result_Type get_Result_Type()
        {
            return result_type;
        }
    }
}
