namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version107 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColorCodes", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ColorCodes", "Code");
        }
    }
}
