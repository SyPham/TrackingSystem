namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version124 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamLangs", "Team_ID", "dbo.Teams");
            DropIndex("dbo.TeamLangs", new[] { "Team_ID" });
            RenameColumn(table: "dbo.TeamLangs", name: "Team_ID", newName: "TeamID");
            AlterColumn("dbo.TeamLangs", "TeamID", c => c.Int(nullable: false));
            CreateIndex("dbo.TeamLangs", "TeamID");
            AddForeignKey("dbo.TeamLangs", "TeamID", "dbo.Teams", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamLangs", "TeamID", "dbo.Teams");
            DropIndex("dbo.TeamLangs", new[] { "TeamID" });
            AlterColumn("dbo.TeamLangs", "TeamID", c => c.Int());
            RenameColumn(table: "dbo.TeamLangs", name: "TeamID", newName: "Team_ID");
            CreateIndex("dbo.TeamLangs", "Team_ID");
            AddForeignKey("dbo.TeamLangs", "Team_ID", "dbo.Teams", "ID");
        }
    }
}
