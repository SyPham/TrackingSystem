using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public abstract class GeneralLang
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LanguageID { get; set; }
    }
}