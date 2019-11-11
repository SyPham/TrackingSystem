using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class PermissionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string NameAction { get; set; }
        public string CodeAction { get; set; }
        public int PermisionID { get; set; }
        [ForeignKey("PermisionID")]
        public virtual Permission Permission { get; set; }
    }
}