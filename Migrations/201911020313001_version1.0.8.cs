namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version108 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColorCodes", "CssCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ColorCodes", "CssCode");
        }
    }
}
