using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Helpers_1_0
{
    class Directory_Helpers
    {
        static public bool create_Path(string abs_path)
        {
            if (abs_path.Length > 260)
            {
                Console.WriteLine("The destination path is too long");
                return false;
            }

            //if (!Directory.Exists(Path.GetDirectoryName(abs_path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(abs_path));
            }

            return true;
        }
    }
}
