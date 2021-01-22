using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers_1_0
{
    class Numeric_Helpers
    {
        public static int[] get_Int(String s)
        {
            int[] num = new int[s.Length];

            int i = 0;
            while(i < s.Length)
            {
                num[i] = s[i];
                i++;
            }

            return num;
        }
    }
}
