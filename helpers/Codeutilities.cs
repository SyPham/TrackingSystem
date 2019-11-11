using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.helpers
{
    public static class Codeutilities
    {
       
        public static bool IsNullOrEmpty(this string value)
        {
            return (value == null || value.Length == 0);
        }
    }
}