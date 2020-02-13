namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version121 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Index");
        }
    }
}
