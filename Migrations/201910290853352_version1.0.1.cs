namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version101 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Departments", "Description");
            DropColumn("dbo.Departments", "Avatar");
            DropColumn("dbo.Departments", "Position");
            DropColumn("dbo.Departments", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Departments", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "Avatar", c => c.String(maxLength: 255));
            AddColumn("dbo.Departments", "Description", c => c.String(maxLength: 255));
        }
    }
}
