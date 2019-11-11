using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.ViewModel
{
    public class LoadDataRecordVM
    {
        public int RecordID { get; set; }
      
        public DateTime CreatedDate { get; set; }

        public int CategoryID { get; set; }

        public int SubCategoryID { get; set; }

        public int UserID { get; set; }

        public string Description { get; set; }

        public string DocumentCategory { get; set; }

        public string Location { get; set; }

        public int LocationID { get; set; }

        public string Title { get; set; }

        public int StatusID { get; set; }

        public string StatusName { get; set; }

        public string TeamName { get; set; }

        public int TeamID { get; set; }

        public int DepartmentID { get; set; }

        public string Department { get; set; }

        public string ApprovalSheetCategory { get; set; }

        public string Username { get; set; }

        public DateTime Modifieddate { get; set; }

        public string TrackingID { get; set; }

        public string LanguageID { get; set; }
        public string ColorCode { get; set; }
        public int ColorCodeID { get; set; }
    }
}