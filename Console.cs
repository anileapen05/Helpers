using System;
using System.IO;
using System.Text;

using System.Windows.Forms;

namespace System_1_0
{
    public static class Global_Console
    {
        private static StringBuilder output_sb;

        private static StringWriter output_sw;

        public static void redirect_Output_To_StringWriter()
        {
            try
            {
                output_sb = new StringBuilder();

                output_sw = new StringWriter(output_sb);

                Console.SetOut(output_sw);
            }
            catch(Exception e)
            {
                Console.WriteLine("redirect_Output_To_StringWriter failed");
                Console.WriteLine(e.Message);
            }

        }

        public static void redirect_To_Standard_Output_Stream()
        {
            try
            {
                Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            }
            catch(Exception e)
            {
                Console.WriteLine("redirect_To_Standard_Output_Stream failed");
                Console.WriteLine(e.Message);
            }
        }

        public static string get_Output()
        {
            try
            {
                //Clears all buffers for the current writer and 
                //causes any buffered data to be written to the underlying device.
                output_sw.Flush();

                string output = output_sb.ToString();

                output_sb.Clear();

                return output;

            }
            catch (Exception e)
            {
                Console.WriteLine("get_Output failed");
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void get_Output(ref TextBox ex_textbox)
        {
            try
            {
                //Clears all buffers for the current writer and 
                //causes any buffered data to be written to the underlying device.
                output_sw.Flush();

                ex_textbox.Text = output_sb.ToString();

                output_sb.Clear();

            }
            catch (Exception e)
            {
                Console.WriteLine("get_Output to textbox failed");
                Console.WriteLine(e.Message);
            }
        }

        public static void clear_Current_Line()
        {
            try
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            catch(Exception e)
            {
                Console.WriteLine("clear_Current_Line failed");
                Console.WriteLine(e.Message);
            }
        }

        public static void clear()
        {
            try
            {
                Console.Clear();
            }
            catch(Exception e)
            {
                Console.WriteLine("clear failed");
                Console.WriteLine(e.Message);
            }
        }

        
    }
}
