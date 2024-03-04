namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShiftedSessionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "SessionRemarks", c => c.String());
            AddColumn("dbo.CourtCases", "CurrentHearingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "CourtDecision", c => c.String());
            DropColumn("dbo.CourtCasesEnforcements", "CurrentHearingDate");
            DropColumn("dbo.CourtCasesEnforcements", "CourtDecision");
            DropColumn("dbo.CourtCasesEnforcements", "SessionRemarks");
            DropColumn("dbo.SessionsRolls", "SessionRemarks");
            DropColumn("dbo.SessionsRolls", "CurrentHearingDate");
            DropColumn("dbo.SessionsRolls", "CourtDecision");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "CourtDecision", c => c.String());
            AddColumn("dbo.SessionsRolls", "CurrentHearingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "SessionRemarks", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SessionRemarks", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "CourtDecision", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "CurrentHearingDate", c => c.DateTime());
            DropColumn("dbo.CourtCases", "CourtDecision");
            DropColumn("dbo.CourtCases", "CurrentHearingDate");
            DropColumn("dbo.CourtCases", "SessionRemarks");
        }
    }
}
