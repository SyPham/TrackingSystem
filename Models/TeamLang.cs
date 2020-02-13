using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class TeamLang : GeneralLang
    {
        public int TeamID { get; set; }
        public virtual Team Team { get; set; }

    }
}