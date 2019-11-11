using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class TeamVM
    {
        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int DepartmentID { get; set; }

        public string Department { get; set; }

        public string LanguageID { get; set; }

    }
}