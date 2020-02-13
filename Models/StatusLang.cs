using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class StatusLang : GeneralLang
    {
        public int StatusID { get; set; }
        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; }

    }
}