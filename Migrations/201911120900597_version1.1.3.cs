namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version113 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DepartmentLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LocationLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StatusLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubCategoryLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TeamLangs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LinkID = c.Int(nullable: false),
                        LangId = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TeamLangs");
            DropTable("dbo.SubCategoryLangs");
            DropTable("dbo.StatusLangs");
            DropTable("dbo.LocationLangs");
            DropTable("dbo.DepartmentLangs");
            DropTable("dbo.CategoryLangs");
        }
    }
}
