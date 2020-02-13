namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version119 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "RoleSym");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "RoleSym", c => c.Int(nullable: false));
        }
    }
}
