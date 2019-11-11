using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        
        public int RecordID { get; set; }

        //public DateTime CreatedDate { get; set; }

        public DateTime CreatedDate
        {
            get;
            set;
        }


        //[DisplayName("Categories")]
        public int CategoryID { get; set; }



        public int SubCategoryID { get; set; }


        public string Description { get; set; }



        public int UserID { get; set; }


        //[DisplayName("Locations")]
        public int LocationID { get; set; }


        public string Title { get; set; }


        //[DisplayName("Status")]
        public int StatusID { get; set; }


        //[DisplayName("Teams")]
        public int TeamID { get; set; }


        public int DepartmentID { get; set; }


        //[DisplayName("Modified Date")]
        public DateTime Modifieddate { get; set; }

        public string TrackingID { get; set; }

        public int DepartmentHeadID { get; set; }

        public string LanguageID { get; set; }

        public int ColorCodeID { get; set; }
    }
}