using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class DeleteVM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        //public bool Status { get; set; }

        public string Description { get; set; }

        public string LocationName { get; set; }

        public string StatusName { get; set; }

        public string TeamName { get; set; }

        public int TeamID { get; set; }

        public string DepartmentName { get; set; }

        public string SubCategories { get; set; }

        public string Username { get; set; }

        public DateTime Modifieddate { get; set; }

        public string TrackingID { get; set; }
    }
}