using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Department
    {
        public Department()
        {
            this.CreateTime = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }

        public string LanguageID { get; set; }
        public virtual List<DepartmentLang> DepartmentLangs { get; set; }

    }
}