namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_SessionUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "CurrentHearingDate", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "CourtDecision", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SessionRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "SessionRemarks");
            DropColumn("dbo.CourtCasesEnforcements", "CourtDecision");
            DropColumn("dbo.CourtCasesEnforcements", "CurrentHearingDate");
        }
    }
}
