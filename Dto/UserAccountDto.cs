using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Dto
{
    public class UserAccountDto
    {
        public int UserID { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public string IDcardNumber { get; set; }

        public int RoleID { get; set; }

        public int DepartmentID { get; set; }

        public int TeamID { get; set; }

    }
}