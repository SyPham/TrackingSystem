namespace DemoDoan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamLangs", "Team_TeamID", "dbo.Teams");
            RenameColumn(table: "dbo.TeamLangs", name: "Team_TeamID", newName: "Team_ID");
            RenameIndex(table: "dbo.TeamLangs", name: "IX_Team_TeamID", newName: "IX_Team_ID");
            DropPrimaryKey("dbo.Teams");
            AddColumn("dbo.Teams", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Teams", "ID");
            AddForeignKey("dbo.TeamLangs", "Team_ID", "dbo.Teams", "ID");
            DropColumn("dbo.Teams", "TeamID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "TeamID", c => c.Int(nullable: false));
            DropForeignKey("dbo.TeamLangs", "Team_ID", "dbo.Teams");
            DropPrimaryKey("dbo.Teams");
            DropColumn("dbo.Teams", "ID");
            AddPrimaryKey("dbo.Teams", "TeamID");
            RenameIndex(table: "dbo.TeamLangs", name: "IX_Team_ID", newName: "IX_Team_TeamID");
            RenameColumn(table: "dbo.TeamLangs", name: "Team_ID", newName: "Team_TeamID");
            AddForeignKey("dbo.TeamLangs", "Team_TeamID", "dbo.Teams", "TeamID");
        }
    }
}
