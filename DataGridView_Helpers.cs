using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;


namespace Helpers_1_0
{
    class DataGridView_Helpers
    {
        public static DataTable datagridview_To_DataTable(DataGridView dgv)
        {
            try
            {
                DataTable dt = new DataTable();

                foreach(DataGridViewColumn dgv_col in dgv.Columns)
                {
                    dt.Columns.Add(dgv_col.HeaderText);
                }

                int row_count = 0;
                while( row_count < dgv.Rows.Count-1)
                {
                    DataGridViewRow dgv_row = dgv.Rows[row_count];

                    List<string> cells = new List<string>();

                    foreach(DataGridViewCell cell in dgv_row.Cells)
                    {
                        cells.Add(cell.Value.ToString());
                    }

                    dt.Rows.Add(cells.ToArray());

                    row_count++;
                }

                return dt;
            }
            catch(Exception e)
            {
                Console.WriteLine("datagridview_To_DataTable failed.." + e.Message);
                return null;
            }
        }
    }
}
