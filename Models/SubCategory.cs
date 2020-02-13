using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class SubCategory
    {
        public SubCategory()
        {
            this.CreateTime = DateTime.Today;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        [Display(Name = "SubCategory_Name", ResourceType = typeof(StaticResource.Resource))]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        [Display(Name = "SubCategory_CategoryID", ResourceType = typeof(StaticResource.Resource))]
        public int CategoryID { get; set; }

        [Display(Name = "SubCategory_LanguageID", ResourceType = typeof(StaticResource.Resource))]
        public string LanguageID { get; set; }
        public virtual List<SubCategoryLang> SubCategoryLangs { get; set; }

    }
}