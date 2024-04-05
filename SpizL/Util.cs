using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spizL
{
    class Util
    {
        public static bool IsDouble(char str)
        {
            return IsDouble(str.ToString());
        }

        public static bool IsDouble(string str)
        {
            return double.TryParse(str.ToString(), out double spiz);
        }

        public static bool IsNumber(string s)
        {
            bool value = true;
            foreach (Char c in s.ToCharArray())
                value = value && Char.IsDigit(c);
            return value;
        }

    }
}

