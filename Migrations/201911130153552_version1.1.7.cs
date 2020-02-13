namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version117 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(maxLength: 3000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(maxLength: 20));
        }
    }
}
