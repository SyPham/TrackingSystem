namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version104 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "LanguageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "LanguageID", c => c.String());
        }
    }
}
