using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        [Display(Name = "Team_Name", ResourceType = typeof(StaticResource.Resource))]
        public string Name { get; set; }

        [Display(Name = "Team_DepartmentID", ResourceType = typeof(StaticResource.Resource))]
        public int DepartmentID { get; set; }

        [Display(Name = "Team_LanguageID", ResourceType = typeof(StaticResource.Resource))]
        public string LanguageID { get; set; }
        public virtual List<TeamLang> TeamLangs { get; set; }

    }
}