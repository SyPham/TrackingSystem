using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Category
    {
        public Category()
        {
            this.CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        public int CategoryID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        //[Required]
        [Display(Name = "Category_Name", ResourceType = typeof(StaticResource.Resource))]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        [Display(Name = "Category_LanguageID", ResourceType = typeof(StaticResource.Resource))]
        public string LanguageID { get; set; }
    }
}