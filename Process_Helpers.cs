using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.IO;

namespace System_1_0
{
    class Process_Helpers
    {
        public delegate void ext_On_Exit();
        public ext_On_Exit eoe;

        //ProcessStartInfo si;
        Process p;
	    string module_path;
	    //int exit_code;

	    public Process_Helpers()
        {
            p = null;
            eoe = null;
        }
    
	    public bool start_Process_From_Batch_File(string ext_module_name)
	    {
            try
            {
                if (File.Exists(ext_module_name) && ext_module_name.Contains(".bat"))
                {
                    p = new Process();
                    // Start the child process. 
                    p.StartInfo.FileName = ext_module_name;

                    p.EnableRaisingEvents = true;
                    p.Exited += new EventHandler(onExit);

                    if (p.Start())
                    {
                        module_path = ext_module_name;    
                    }

                }

                return true;
            }
            catch(Exception e)
            {
                Console.Write("\nCreateProcess failed for " + ext_module_name+" "+e.Message);
                //printError(L"start_Process failed");
                return false;
            }
	    }

        public bool start_Process_From_Command(string ext_module_name, string arg)
        {
            try
            {
                if (ext_module_name.Length > 0)
                {
                    p = new Process();
                    // Start the child process. 
                    p.StartInfo.FileName = ext_module_name;
                    p.StartInfo.Arguments = arg;
                    p.EnableRaisingEvents = true;
                    p.Exited += new EventHandler(onExit);

                    if (p.Start())
                    {
                        module_path = ext_module_name;
                    }

                }

                return true;
            }
            catch (Exception e)
            {
                Console.Write("\nCreateProcess failed for " + ext_module_name + "with arg: " + arg + " " + e.Message);
                //printError(L"start_Process failed");
                return false;
            }
        }

	    //untested
        public bool kill_Process()
	    {
            try
            {
                 p.Kill();
                 return true;
            }
            catch(Exception e)
            {
                Console.Write("kill_Process failed! "+e.Message);
		        return false;
            }
	    }

        private void onExit(object sender, System.EventArgs e)
        {
            Console.WriteLine("Process has exited.");
            if (eoe != null)
            {
                eoe();
            }
        }

        public bool debug_Start_Process_From_Batch_File(string ext_module_name)
        {
            try
            {
                if (File.Exists(ext_module_name) && ext_module_name.Contains(".bat"))
                {
                    p = new Process();
                    // Start the child process. 
                    p.StartInfo.FileName = ext_module_name;

                    p.EnableRaisingEvents = true;
                    p.Exited += new EventHandler(onExit);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;

                    if (p.Start())
                    {
                        using (StreamReader reader = p.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            Console.Write(result);
                        }
                        module_path = ext_module_name;
                    }

                }

                return true;
            }
            catch (Exception e)
            {
                Console.Write("\nCreateProcess failed for " + ext_module_name + " " + e.Message);
                //printError(L"start_Process failed");
                return false;
            }
        }

        public bool debug_Start_Process_From_Command(string ext_module_name, string arg)
        {
            try
            {
                if (ext_module_name.Length > 0)
                {
                    p = new Process();
                    // Start the child process. 
                    p.StartInfo.FileName = ext_module_name;
                    p.StartInfo.Arguments = arg;
                    p.EnableRaisingEvents = true;
                    p.Exited += new EventHandler(onExit);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;

                    if (p.Start())
                    {
                        using (StreamReader reader = p.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            Console.Write(result);
                        }
                        module_path = ext_module_name;
                    }

                }

                return true;
            }
            catch (Exception e)
            {
                Console.Write("\nCreateProcess failed for " + ext_module_name + "with arg: " + arg + " " + e.Message);
                //printError(L"start_Process failed");
                return false;
            }
        }
	
    };
}
