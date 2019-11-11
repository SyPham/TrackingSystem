using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Language
    {
        [Key]
        public string LanguageID { get; set; }

        [Display(Name = "Language_Name", ResourceType = typeof(StaticResource.Resource))]
        public string Name { get; set; }

        [Display(Name = "Language_Status", ResourceType = typeof(StaticResource.Resource))]
        public bool Status { get; set; }
    }
}