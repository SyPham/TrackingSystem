namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version122 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Title");
        }
    }
}
