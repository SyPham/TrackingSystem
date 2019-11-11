using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class UserAccountVM
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public string IDcardNumber { get; set; }

        public string RoleID { get; set; }

        public int DepartmentID { get; set; }

        public int TeamID { get; set; }

        public string Department { get; set; }

        public string Team { get; set; }

        public string LanguageID { get; set; }
    }
}