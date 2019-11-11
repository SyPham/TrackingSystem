namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version102 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "ColorCodeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "ColorCodeID");
        }
    }
}
