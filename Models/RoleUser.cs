using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class RoleUser
    {
        public RoleUser()
        {
            UpdateTime = DateTime.Today;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int RoleID { get; set; }
        public int UserID { get; set; }

        public DateTime UpdateTime { get; set; }
    }
    
}