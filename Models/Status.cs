using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusID { get; set; }

        [Display(Name = "Status_Name", ResourceType = typeof(StaticResource.Resource))]
        public string Name { get; set; }

        [Display(Name = "Status_LanguageID", ResourceType = typeof(StaticResource.Resource))]
        public string LanguageID { get; set; }
    }
}