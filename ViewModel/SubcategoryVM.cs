using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class SubcategoryVM
    {
        public int SubCategoryID { get; set; }

        [Display(Name = "SubCategory_Name", ResourceType = typeof(StaticResource.Resource))]
        public string SubCategoryName { get; set; }

        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public string Language { get; set; }
        public string LanguageID { get; set; }
        public DateTime CreateTime { get; set; }

    }
}