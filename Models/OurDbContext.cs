using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoDoan.Models
{
    public class OurDbContext : DbContext
    {
        public OurDbContext() : base("name=OurDBContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
            modelBuilder.Entity<Team>()
             .Property(c => c.TeamID)
             .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
        public DbSet<UserAccount> userAccount { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationDetail> NotificationDetails { get; set; }
        public DbSet<Language> Language { get; set; }

        public DbSet<UserLanguages> UserLanguages { get; set; }
        public DbSet<ColorCode> ColorCodes { get; set; }
        public DbSet<DepartmentManger> DepartmentMangers { get; set; }
        public DbSet<PermissionDetail> PermissionDetails { get; set; }

    }
}