namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version110 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionDetails", "Permission_PermissionID", "dbo.Permissions");
            DropIndex("dbo.PermissionDetails", new[] { "Permission_PermissionID" });
            DropColumn("dbo.PermissionDetails", "PermisionID");
            RenameColumn(table: "dbo.PermissionDetails", name: "Permission_PermissionID", newName: "PermisionID");
            AlterColumn("dbo.PermissionDetails", "PermisionID", c => c.Int(nullable: false));
            CreateIndex("dbo.PermissionDetails", "PermisionID");
            AddForeignKey("dbo.PermissionDetails", "PermisionID", "dbo.Permissions", "PermissionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissionDetails", "PermisionID", "dbo.Permissions");
            DropIndex("dbo.PermissionDetails", new[] { "PermisionID" });
            AlterColumn("dbo.PermissionDetails", "PermisionID", c => c.Int());
            RenameColumn(table: "dbo.PermissionDetails", name: "PermisionID", newName: "Permission_PermissionID");
            AddColumn("dbo.PermissionDetails", "PermisionID", c => c.Int(nullable: false));
            CreateIndex("dbo.PermissionDetails", "Permission_PermissionID");
            AddForeignKey("dbo.PermissionDetails", "Permission_PermissionID", "dbo.Permissions", "PermissionID");
        }
    }
}
