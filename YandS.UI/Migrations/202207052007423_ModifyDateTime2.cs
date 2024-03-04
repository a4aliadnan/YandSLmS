namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "ActionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "AnnouncementCompleteDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "SubmissionDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "ApprovalDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "InquiryDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "MOHResultDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "ROPResultDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "BankResultDt", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "JUDAuctionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "DateOfInstruction", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesEnforcements", "MoneyTrRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "MoneyTrRequestDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "DateOfInstruction", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "JUDAuctionDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestedDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "BankResultDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "ROPResultDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "MOHResultDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "InquiryDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "ApprovalDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "SubmissionDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "AnnouncementCompleteDt", c => c.DateTime());
            AlterColumn("dbo.CourtCasesEnforcements", "ActionDate", c => c.DateTime());
        }
    }
}
