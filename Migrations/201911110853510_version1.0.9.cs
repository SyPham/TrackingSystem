namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version109 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameAction = c.String(),
                        CodeAction = c.String(),
                        PermisionID = c.Int(nullable: false),
                        Permission_PermissionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permissions", t => t.Permission_PermissionID)
                .Index(t => t.Permission_PermissionID);
            
            AddColumn("dbo.Permissions", "Name", c => c.String());
            AddColumn("dbo.Permissions", "Description", c => c.String());
            DropColumn("dbo.Permissions", "RoleID");
            DropColumn("dbo.Permissions", "Url");
            DropColumn("dbo.Permissions", "LanguageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "LanguageID", c => c.String());
            AddColumn("dbo.Permissions", "Url", c => c.String());
            AddColumn("dbo.Permissions", "RoleID", c => c.Int(nullable: false));
            DropForeignKey("dbo.PermissionDetails", "Permission_PermissionID", "dbo.Permissions");
            DropIndex("dbo.PermissionDetails", new[] { "Permission_PermissionID" });
            DropColumn("dbo.Permissions", "Description");
            DropColumn("dbo.Permissions", "Name");
            DropTable("dbo.PermissionDetails");
        }
    }
}
