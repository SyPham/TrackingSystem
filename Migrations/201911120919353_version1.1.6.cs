namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version116 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryLangs", "LanguageID", c => c.String());
            AddColumn("dbo.DepartmentLangs", "LanguageID", c => c.String());
            AddColumn("dbo.LocationLangs", "LanguageID", c => c.String());
            AddColumn("dbo.StatusLangs", "LanguageID", c => c.String());
            AddColumn("dbo.SubCategoryLangs", "LanguageID", c => c.String());
            AddColumn("dbo.TeamLangs", "LanguageID", c => c.String());
            DropColumn("dbo.CategoryLangs", "LinkID");
            DropColumn("dbo.DepartmentLangs", "LinkID");
            DropColumn("dbo.LocationLangs", "LinkID");
            DropColumn("dbo.StatusLangs", "LinkID");
            DropColumn("dbo.SubCategoryLangs", "LinkID");
            DropColumn("dbo.TeamLangs", "LinkID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamLangs", "LinkID", c => c.Int(nullable: false));
            AddColumn("dbo.SubCategoryLangs", "LinkID", c => c.Int(nullable: false));
            AddColumn("dbo.StatusLangs", "LinkID", c => c.Int(nullable: false));
            AddColumn("dbo.LocationLangs", "LinkID", c => c.Int(nullable: false));
            AddColumn("dbo.DepartmentLangs", "LinkID", c => c.Int(nullable: false));
            AddColumn("dbo.CategoryLangs", "LinkID", c => c.Int(nullable: false));
            DropColumn("dbo.TeamLangs", "LanguageID");
            DropColumn("dbo.SubCategoryLangs", "LanguageID");
            DropColumn("dbo.StatusLangs", "LanguageID");
            DropColumn("dbo.LocationLangs", "LanguageID");
            DropColumn("dbo.DepartmentLangs", "LanguageID");
            DropColumn("dbo.CategoryLangs", "LanguageID");
        }
    }
}
