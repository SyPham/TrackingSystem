using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class NotificationDetail
    {
        public NotificationDetail()
        {
            this.CreatedDate = DateTime.Now;
        }
        [Key]
        public int NotificationDetailID { get; set; }

        public int NotificationID { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserID { get; set; }

        public string LanguageID { get; set; }
    }
}