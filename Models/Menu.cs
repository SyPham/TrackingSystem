using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Menu
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string FontawareSome { get; set; }
        public int RoleID { get; set; }
        public int Index { get; set; }
        public virtual Role Role { get; set; }
    }
}