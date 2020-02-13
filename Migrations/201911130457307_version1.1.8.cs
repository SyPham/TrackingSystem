namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version118 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        FontawareSome = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Menus", "RoleID", "dbo.Roles");
            DropIndex("dbo.Menus", new[] { "RoleID" });
            DropTable("dbo.Menus");
        }
    }
}
