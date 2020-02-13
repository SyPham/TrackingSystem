using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class SubCategoryLang : GeneralLang
    {
        public int SubCategoryID { get; set; }
        [ForeignKey("SubCategoryID")]
        public virtual SubCategory SubCategory { get; set; }

    }
}