using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class SubCategoriesVM
    {
        public int SubCategoryID { get; set; }

        public string SubCategoryName { get; set; }

        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string LanguageID { get; set; }
    }
}