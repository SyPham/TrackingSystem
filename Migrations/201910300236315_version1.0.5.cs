namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version105 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teams", "Avatar");
            DropColumn("dbo.Teams", "Description");
            DropColumn("dbo.Teams", "Position");
            DropColumn("dbo.Teams", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teams", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.Teams", "Avatar", c => c.String(maxLength: 255));
        }
    }
}
