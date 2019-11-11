using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PermissionID { get; set; }
        //[ForeignKey("Role")]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PermissionDetail> PermissionDetails { get; set; }

    }
}