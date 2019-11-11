using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    [Serializable()]
    public class UserVM
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public int RoleID { get; set; }
        public int DepartmentID { get; set; }
        public int TeamID { get; set; }

        public int LocationID { get; set; }
        public static string CurrentCulture { get; set; }
        public string LanguageID { get; set; }
    }
}