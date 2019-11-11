using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class Notifications
    {
        public Notifications()
        {
            this.CreatedDate = DateTime.Now;
        }
        [Key]
        public int NotificationID { get; set; }

        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public int RecordID { get; set; }

        public int UserID { get; set; }

        public string Action { get; set; }

        public string LanguageID { get; set; }
    }
}