namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEnforcementEnhancement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ClientReply", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCases", "CourtFollow", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCasesEnforcements", "AnnouncementTypeId", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "AnnouncementCompleteDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "SubmissionDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "ApprovalDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "InquiryDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "MOHResultDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "MOHResult", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "ROPResultDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "ROPResult", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "BankResultDt", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "BankResult", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "Arrested", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCasesEnforcements", "ArrestedDate", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "AuctionProcess", c => c.String(maxLength: 3));
            AddColumn("dbo.CourtCasesEnforcements", "JUDAuctionDate", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "JUDAuctionValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CourtCasesEnforcements", "JUDAuctionRepeat", c => c.String(maxLength: 3));
            AddColumn("dbo.CourtCasesEnforcements", "JUDDecisionId", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "CauseOfRecoveryId", c => c.String(maxLength: 2));
            AddColumn("dbo.CourtCasesEnforcements", "DateOfInstruction", c => c.DateTime());
            AddColumn("dbo.CourtCasesEnforcements", "MoneyTrRequestDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestOrderNo", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestOrderNo", c => c.String());
            DropColumn("dbo.CourtCasesEnforcements", "MoneyTrRequestDate");
            DropColumn("dbo.CourtCasesEnforcements", "DateOfInstruction");
            DropColumn("dbo.CourtCasesEnforcements", "CauseOfRecoveryId");
            DropColumn("dbo.CourtCasesEnforcements", "JUDDecisionId");
            DropColumn("dbo.CourtCasesEnforcements", "JUDAuctionRepeat");
            DropColumn("dbo.CourtCasesEnforcements", "JUDAuctionValue");
            DropColumn("dbo.CourtCasesEnforcements", "JUDAuctionDate");
            DropColumn("dbo.CourtCasesEnforcements", "AuctionProcess");
            DropColumn("dbo.CourtCasesEnforcements", "ArrestedDate");
            DropColumn("dbo.CourtCasesEnforcements", "Arrested");
            DropColumn("dbo.CourtCasesEnforcements", "BankResult");
            DropColumn("dbo.CourtCasesEnforcements", "BankResultDt");
            DropColumn("dbo.CourtCasesEnforcements", "ROPResult");
            DropColumn("dbo.CourtCasesEnforcements", "ROPResultDt");
            DropColumn("dbo.CourtCasesEnforcements", "MOHResult");
            DropColumn("dbo.CourtCasesEnforcements", "MOHResultDt");
            DropColumn("dbo.CourtCasesEnforcements", "InquiryDt");
            DropColumn("dbo.CourtCasesEnforcements", "ApprovalDt");
            DropColumn("dbo.CourtCasesEnforcements", "SubmissionDt");
            DropColumn("dbo.CourtCasesEnforcements", "AnnouncementCompleteDt");
            DropColumn("dbo.CourtCasesEnforcements", "AnnouncementTypeId");
            DropColumn("dbo.CourtCases", "CourtFollow");
            DropColumn("dbo.CourtCases", "ClientReply");
        }
    }
}
