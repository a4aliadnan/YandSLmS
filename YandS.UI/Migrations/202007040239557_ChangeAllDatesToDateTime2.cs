namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAllDatesToDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "ReceptionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCases", "LegalNoticeDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesDetails", "RegistrationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesDetails", "JudgementReceivingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesFollowups", "HearingDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesFollowups", "NextHearingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HR_Employee_s", "DOB", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HR_Employee_s", "DOA", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HR_Employee_s", "DOR", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PayVoucher", "Cheque_Date", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PayVoucher", "AuthorizeDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "AuthorizeDate", c => c.DateTime());
            AlterColumn("dbo.PayVoucher", "Cheque_Date", c => c.DateTime());
            AlterColumn("dbo.HR_Employee_s", "DOR", c => c.DateTime());
            AlterColumn("dbo.HR_Employee_s", "DOA", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "DOB", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CourtCasesFollowups", "NextHearingDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesFollowups", "HearingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CourtCasesDetails", "JudgementReceivingDate", c => c.DateTime());
            AlterColumn("dbo.CourtCasesDetails", "RegistrationDate", c => c.DateTime());
            AlterColumn("dbo.CourtCases", "LegalNoticeDate", c => c.DateTime());
            AlterColumn("dbo.CourtCases", "ReceptionDate", c => c.DateTime());
        }
    }
}
