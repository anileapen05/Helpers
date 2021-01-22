using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Data;

namespace NetWorking_1_0
{
    class IP
    {
        IPHostEntry entry;
        
        public void get_IPHostEntry(string address)
        {
            try
            {
                entry = Dns.GetHostEntry(address);
            }
            catch(Exception e)
            {
                Console.WriteLine("get_IPHostEntry failed.." + e.Message);
            }
        }

        public DataTable get_Addresses_As_DataTable()
        {
            try
            {
                DataTable table = new DataTable();

                // Declare DataColumn and DataRow variables.
                DataColumn column;
                DataRow row;

                //IPAddress
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "IPAddress";
                table.Columns.Add(column);

                //IPAddress family
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Family";
                table.Columns.Add(column);

                //IsIPv6LinkLocal
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "IsIPv6LinkLocal";
                table.Columns.Add(column);

                //IPv6Multicast
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "IsIPv6Multicast";
                table.Columns.Add(column);

                //IsIPv6SiteLocal
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "IsIPv6SiteLocal";
                table.Columns.Add(column);

                //IsIPv6Teredo
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Boolean");
                column.ColumnName = "IsIPv6Teredo";
                table.Columns.Add(column);

                //ScopeId
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int64");
                column.ColumnName = "ScopeId";
                table.Columns.Add(column);

                // Create new DataRow objects and add to DataTable.     
                foreach(IPAddress address in entry.AddressList)
                {
                    row = table.NewRow();
                    row["IPAddress"]        = address.ToString();
                    row["Family"]           = address.AddressFamily.ToString();
                    row["IsIPv6LinkLocal"]  = address.IsIPv6LinkLocal;
                    row["IsIPv6Multicast"]  = address.IsIPv6Multicast;
                    row["IsIPv6SiteLocal"]  = address.IsIPv6SiteLocal;
                    row["IsIPv6Teredo"]     = address.IsIPv6Teredo;
                    if (address.AddressFamily.ToString() == ProtocolFamily.InterNetworkV6.ToString())
                    {
                        row["ScopeId"] = address.ScopeId;
                    }

                    table.Rows.Add(row);
                }

                return table;

            }
            catch(Exception e)
            {
                Console.WriteLine("get_IP_Addresses failed.." + e.Message);
                return null;
            }
        }

        public IPAddress[] get_List_Addresses()
        {
            return entry.AddressList;        
        }

        public string[] get_Aliases()
        {
            return entry.Aliases;
        }

        public DataTable get_Aliases_As_DataTable()
        {
            DataTable table = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column;
            
            //Alias
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Alias";
            table.Columns.Add(column);

            table.Rows.Add(entry.Aliases);

            return table;
        }

        //untested
        public string get_Host_Name()
        {
            return entry.HostName;
        }
    }
}
