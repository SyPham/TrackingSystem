namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLanguages", "LanguageID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserLanguages", "LanguageID", c => c.Int(nullable: false));
        }
    }
}
