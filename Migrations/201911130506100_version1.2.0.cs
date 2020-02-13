namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version120 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.UserAccounts", "RoleID");
            AddForeignKey("dbo.UserAccounts", "RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            DropColumn("dbo.UserAccounts", "DepartmentHeadID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccounts", "DepartmentHeadID", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserAccounts", "RoleID", "dbo.Roles");
            DropIndex("dbo.UserAccounts", new[] { "RoleID" });
        }
    }
}
