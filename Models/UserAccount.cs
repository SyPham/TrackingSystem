using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class UserAccount
    {
        public UserAccount()
        {
            this.CreateTime = DateTime.Now;
        }

        ///---

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Required]
        [Display(Name = "UserAccounts_Username",ResourceType = typeof(StaticResource.Resource))]
        public string Username { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "UserAccounts_Password", ResourceType = typeof(StaticResource.Resource))]
        //[Required(ErrorMessage = "Please enter your Password.")]
        public string Password { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Address { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Email { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar")]
        public string Mobi { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string Avatar { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "UserAccounts_Description", ResourceType = typeof(StaticResource.Resource))]
        public string Description { get; set; }

        public DateTime CreateTime { get; set; }

        [StringLength(255)]
        [Column(TypeName = "nvarchar")]
        public string CreateBy { get; set; }

        public int Position { get; set; }

        [Display(Name = "UserAccounts_Status", ResourceType = typeof(StaticResource.Resource))]
        public bool Status { get; set; }

        [Display(Name = "UserAccounts_IDcardNumber", ResourceType = typeof(StaticResource.Resource))]
        public string IDcardNumber { get; set; }

        [Display(Name = "UserAccounts_RoleID", ResourceType = typeof(StaticResource.Resource))]
        public int RoleID { get; set; }

        [Display(Name = "UserAccounts_DepartmentID", ResourceType = typeof(StaticResource.Resource))]
        public int DepartmentID { get; set; }

        [Display(Name = "UserAccounts_TeamID", ResourceType = typeof(StaticResource.Resource))]
        public int TeamID { get; set; }

        public int DepartmentHeadID { get; set; }

        public int LocationID { get; set; }

        public string LanguageID { get; set; }
        public int PemisionID { get; set; }

    }
}