namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version112 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Roles", "RoleSym", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "RoleSym");
            DropTable("dbo.RoleUsers");
        }
    }
}
