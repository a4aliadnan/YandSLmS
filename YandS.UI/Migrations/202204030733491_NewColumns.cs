namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "RealEstateYesNo", c => c.String(maxLength: 3));
            AddColumn("dbo.CourtCases", "RealEstateDetail", c => c.String(maxLength: 500));
            AddColumn("dbo.CourtCases", "ReconciliationNo", c => c.String(maxLength: 50));
            AddColumn("dbo.CourtCases", "ReconciliationDep", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCases", "GovernorateId", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCases", "ClaimSummary", c => c.String());
            AddColumn("dbo.SessionsRolls", "FinalJudgement", c => c.Boolean(nullable: false, defaultValue: false));
            DropColumn("dbo.CourtCasesDetails", "RealEstateYesNo");
            DropColumn("dbo.CourtCasesDetails", "RealEstateDetail");
            DropColumn("dbo.CourtCasesDetails", "ClaimSummary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtCasesDetails", "ClaimSummary", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "RealEstateDetail", c => c.String(maxLength: 500));
            AddColumn("dbo.CourtCasesDetails", "RealEstateYesNo", c => c.String(maxLength: 3));
            DropColumn("dbo.SessionsRolls", "FinalJudgement");
            DropColumn("dbo.CourtCases", "ClaimSummary");
            DropColumn("dbo.CourtCases", "GovernorateId");
            DropColumn("dbo.CourtCases", "ReconciliationDep");
            DropColumn("dbo.CourtCases", "ReconciliationNo");
            DropColumn("dbo.CourtCases", "RealEstateDetail");
            DropColumn("dbo.CourtCases", "RealEstateYesNo");
        }
    }
}
