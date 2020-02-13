namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version114 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryLangs", "Category_CategoryID", c => c.Int());
            AddColumn("dbo.DepartmentLangs", "Department_DepartmentID", c => c.Int());
            AddColumn("dbo.LocationLangs", "Location_Number", c => c.Int());
            AddColumn("dbo.StatusLangs", "Status_StatusID", c => c.Int());
            AddColumn("dbo.SubCategoryLangs", "SubCategory_SubCategoryID", c => c.Int());
            AddColumn("dbo.TeamLangs", "Team_TeamID", c => c.Int());
            CreateIndex("dbo.CategoryLangs", "Category_CategoryID");
            CreateIndex("dbo.DepartmentLangs", "Department_DepartmentID");
            CreateIndex("dbo.LocationLangs", "Location_Number");
            CreateIndex("dbo.StatusLangs", "Status_StatusID");
            CreateIndex("dbo.SubCategoryLangs", "SubCategory_SubCategoryID");
            CreateIndex("dbo.TeamLangs", "Team_TeamID");
            AddForeignKey("dbo.CategoryLangs", "Category_CategoryID", "dbo.Categories", "CategoryID");
            AddForeignKey("dbo.DepartmentLangs", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.LocationLangs", "Location_Number", "dbo.Locations", "LocationID");
            AddForeignKey("dbo.StatusLangs", "Status_StatusID", "dbo.Status", "StatusID");
            AddForeignKey("dbo.SubCategoryLangs", "SubCategory_SubCategoryID", "dbo.SubCategories", "SubCategoryID");
            AddForeignKey("dbo.TeamLangs", "Team_TeamID", "dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamLangs", "Team_TeamID", "dbo.Teams");
            DropForeignKey("dbo.SubCategoryLangs", "SubCategory_SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.StatusLangs", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.LocationLangs", "Location_Number", "dbo.Locations");
            DropForeignKey("dbo.DepartmentLangs", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.CategoryLangs", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.TeamLangs", new[] { "Team_TeamID" });
            DropIndex("dbo.SubCategoryLangs", new[] { "SubCategory_SubCategoryID" });
            DropIndex("dbo.StatusLangs", new[] { "Status_StatusID" });
            DropIndex("dbo.LocationLangs", new[] { "Location_Number" });
            DropIndex("dbo.DepartmentLangs", new[] { "Department_DepartmentID" });
            DropIndex("dbo.CategoryLangs", new[] { "Category_CategoryID" });
            DropColumn("dbo.TeamLangs", "Team_TeamID");
            DropColumn("dbo.SubCategoryLangs", "SubCategory_SubCategoryID");
            DropColumn("dbo.StatusLangs", "Status_StatusID");
            DropColumn("dbo.LocationLangs", "Location_Number");
            DropColumn("dbo.DepartmentLangs", "Department_DepartmentID");
            DropColumn("dbo.CategoryLangs", "Category_CategoryID");
        }
    }
}
