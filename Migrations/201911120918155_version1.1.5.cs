namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version115 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryLangs", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.DepartmentLangs", "Department_DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.LocationLangs", "Location_Number", "dbo.Locations");
            DropForeignKey("dbo.StatusLangs", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.SubCategoryLangs", "SubCategory_SubCategoryID", "dbo.SubCategories");
            DropIndex("dbo.CategoryLangs", new[] { "Category_CategoryID" });
            DropIndex("dbo.DepartmentLangs", new[] { "Department_DepartmentID" });
            DropIndex("dbo.LocationLangs", new[] { "Location_Number" });
            DropIndex("dbo.StatusLangs", new[] { "Status_StatusID" });
            DropIndex("dbo.SubCategoryLangs", new[] { "SubCategory_SubCategoryID" });
            RenameColumn(table: "dbo.CategoryLangs", name: "Category_CategoryID", newName: "CategoryID");
            RenameColumn(table: "dbo.DepartmentLangs", name: "Department_DepartmentID", newName: "DepartmentID");
            RenameColumn(table: "dbo.LocationLangs", name: "Location_Number", newName: "LocationID");
            RenameColumn(table: "dbo.StatusLangs", name: "Status_StatusID", newName: "StatusID");
            RenameColumn(table: "dbo.SubCategoryLangs", name: "SubCategory_SubCategoryID", newName: "SubCategoryID");
            AlterColumn("dbo.CategoryLangs", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.DepartmentLangs", "DepartmentID", c => c.Int(nullable: false));
            AlterColumn("dbo.LocationLangs", "LocationID", c => c.Int(nullable: false));
            AlterColumn("dbo.StatusLangs", "StatusID", c => c.Int(nullable: false));
            AlterColumn("dbo.SubCategoryLangs", "SubCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.CategoryLangs", "CategoryID");
            CreateIndex("dbo.DepartmentLangs", "DepartmentID");
            CreateIndex("dbo.LocationLangs", "LocationID");
            CreateIndex("dbo.StatusLangs", "StatusID");
            CreateIndex("dbo.SubCategoryLangs", "SubCategoryID");
            AddForeignKey("dbo.CategoryLangs", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            AddForeignKey("dbo.DepartmentLangs", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
            AddForeignKey("dbo.LocationLangs", "LocationID", "dbo.Locations", "LocationID", cascadeDelete: true);
            AddForeignKey("dbo.StatusLangs", "StatusID", "dbo.Status", "StatusID", cascadeDelete: true);
            AddForeignKey("dbo.SubCategoryLangs", "SubCategoryID", "dbo.SubCategories", "SubCategoryID", cascadeDelete: true);
            DropColumn("dbo.CategoryLangs", "LangId");
            DropColumn("dbo.DepartmentLangs", "LangId");
            DropColumn("dbo.LocationLangs", "LangId");
            DropColumn("dbo.StatusLangs", "LangId");
            DropColumn("dbo.SubCategoryLangs", "LangId");
            DropColumn("dbo.TeamLangs", "LangId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamLangs", "LangId", c => c.String());
            AddColumn("dbo.SubCategoryLangs", "LangId", c => c.String());
            AddColumn("dbo.StatusLangs", "LangId", c => c.String());
            AddColumn("dbo.LocationLangs", "LangId", c => c.String());
            AddColumn("dbo.DepartmentLangs", "LangId", c => c.String());
            AddColumn("dbo.CategoryLangs", "LangId", c => c.String());
            DropForeignKey("dbo.SubCategoryLangs", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.StatusLangs", "StatusID", "dbo.Status");
            DropForeignKey("dbo.LocationLangs", "LocationID", "dbo.Locations");
            DropForeignKey("dbo.DepartmentLangs", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.CategoryLangs", "CategoryID", "dbo.Categories");
            DropIndex("dbo.SubCategoryLangs", new[] { "SubCategoryID" });
            DropIndex("dbo.StatusLangs", new[] { "StatusID" });
            DropIndex("dbo.LocationLangs", new[] { "LocationID" });
            DropIndex("dbo.DepartmentLangs", new[] { "DepartmentID" });
            DropIndex("dbo.CategoryLangs", new[] { "CategoryID" });
            AlterColumn("dbo.SubCategoryLangs", "SubCategoryID", c => c.Int());
            AlterColumn("dbo.StatusLangs", "StatusID", c => c.Int());
            AlterColumn("dbo.LocationLangs", "LocationID", c => c.Int());
            AlterColumn("dbo.DepartmentLangs", "DepartmentID", c => c.Int());
            AlterColumn("dbo.CategoryLangs", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.SubCategoryLangs", name: "SubCategoryID", newName: "SubCategory_SubCategoryID");
            RenameColumn(table: "dbo.StatusLangs", name: "StatusID", newName: "Status_StatusID");
            RenameColumn(table: "dbo.LocationLangs", name: "LocationID", newName: "Location_Number");
            RenameColumn(table: "dbo.DepartmentLangs", name: "DepartmentID", newName: "Department_DepartmentID");
            RenameColumn(table: "dbo.CategoryLangs", name: "CategoryID", newName: "Category_CategoryID");
            CreateIndex("dbo.SubCategoryLangs", "SubCategory_SubCategoryID");
            CreateIndex("dbo.StatusLangs", "Status_StatusID");
            CreateIndex("dbo.LocationLangs", "Location_Number");
            CreateIndex("dbo.DepartmentLangs", "Department_DepartmentID");
            CreateIndex("dbo.CategoryLangs", "Category_CategoryID");
            AddForeignKey("dbo.SubCategoryLangs", "SubCategory_SubCategoryID", "dbo.SubCategories", "SubCategoryID");
            AddForeignKey("dbo.StatusLangs", "Status_StatusID", "dbo.Status", "StatusID");
            AddForeignKey("dbo.LocationLangs", "Location_Number", "dbo.Locations", "LocationID");
            AddForeignKey("dbo.DepartmentLangs", "Department_DepartmentID", "dbo.Departments", "DepartmentID");
            AddForeignKey("dbo.CategoryLangs", "Category_CategoryID", "dbo.Categories", "CategoryID");
        }
    }
}
