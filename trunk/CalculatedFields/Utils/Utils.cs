using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatedFields.Utils
{
    public static class Utils
    {
        public static string Escape(this string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "").Replace("\r", "").Replace("\'", "\\\'");
        }
    }
}
