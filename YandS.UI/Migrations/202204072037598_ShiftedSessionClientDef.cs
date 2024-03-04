namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShiftedSessionClientDef : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "SessionClientId", c => c.String(maxLength: 5));
            AddColumn("dbo.CourtCases", "SessionRollDefendentName", c => c.String(maxLength: 255));
            DropColumn("dbo.SessionsRolls", "SessionClientId");
            DropColumn("dbo.SessionsRolls", "SessionRollDefendentName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "SessionRollDefendentName", c => c.String(maxLength: 255));
            AddColumn("dbo.SessionsRolls", "SessionClientId", c => c.String(maxLength: 5));
            DropColumn("dbo.CourtCases", "SessionRollDefendentName");
            DropColumn("dbo.CourtCases", "SessionClientId");
        }
    }
}
