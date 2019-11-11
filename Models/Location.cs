using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("LocationID")]
        public int Number { get; set; }
        [Column("Name")]
        [Display(Name = "Location_Content", ResourceType = typeof(StaticResource.Resource))]
        public string Content { get; set; }

        [Display(Name = "Location_Remark", ResourceType = typeof(StaticResource.Resource))]
        public string Remark { get; set; }

        [Display(Name = "Location_DepartmentID", ResourceType = typeof(StaticResource.Resource))]
        public int DepartmentID { get; set; }

        [Display(Name = "Location_TeamID", ResourceType = typeof(StaticResource.Resource))]
        public int TeamID { get; set; }

        [Display(Name = "Location_LanguageID", ResourceType = typeof(StaticResource.Resource))]
        public string LanguageID { get; set; }
    }
}