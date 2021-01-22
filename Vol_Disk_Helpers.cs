using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IO_1_0
{
    public class Disk_Helpers
    {
        static public long get_Free_Space(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }

            return -1;
        }
    }
    
    public class Vol_Helpers
    { 
    
    }
}
