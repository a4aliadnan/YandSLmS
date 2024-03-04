namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDDLColumnSize_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "OfficeFileNo", c => c.String(maxLength: 10));
            AlterColumn("dbo.CourtCases", "ClientCode", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.CourtCases", "Defendant", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.CourtCases", "AgainstCode", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "ReceiveLevelCode", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "AccountContractNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "ClientFileNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "CaseTypeCode", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "CaseLevelCode", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "ParentCourtId", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "LoanManager", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "ODBLoanCode", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCases", "ODBBankBranch", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCases", "PoliceNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "PoliceStation", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCases", "PublicProsecutionNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "PublicPlace", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCases", "PAPCNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "PAPCPlace", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCases", "LaborConflicNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCases", "LaborConflicPlace", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCases", "SelectedBeforeCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCases", "CaseStatus", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCases", "ClientCaseType", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCases", "IdRegistrationNo", c => c.String(maxLength: 255));
            AlterColumn("dbo.CourtCases", "CRRegistrationNo", c => c.String(maxLength: 50));
            AlterColumn("dbo.CourtCases", "OmaniExp", c => c.String(maxLength: 1));
            AlterColumn("dbo.CourtCases", "ClosedbyStaff", c => c.String(maxLength: 20));
            AlterColumn("dbo.CourtCases", "ClientClassificationCode", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.CourtCases", "CaseSubject", c => c.String(maxLength: 3));
            AlterColumn("dbo.MASTER_S", "Mst_Desc", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.MASTER_S", "Mst_Value", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.MASTER_S", "Remarks", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MASTER_S", "Remarks", c => c.String());
            AlterColumn("dbo.MASTER_S", "Mst_Value", c => c.String(nullable: false));
            AlterColumn("dbo.MASTER_S", "Mst_Desc", c => c.String(nullable: false));
            AlterColumn("dbo.CourtCases", "CaseSubject", c => c.String());
            AlterColumn("dbo.CourtCases", "ClientClassificationCode", c => c.String(nullable: false));
            AlterColumn("dbo.CourtCases", "ClosedbyStaff", c => c.String());
            AlterColumn("dbo.CourtCases", "OmaniExp", c => c.String());
            AlterColumn("dbo.CourtCases", "CRRegistrationNo", c => c.String());
            AlterColumn("dbo.CourtCases", "IdRegistrationNo", c => c.String());
            AlterColumn("dbo.CourtCases", "ClientCaseType", c => c.String());
            AlterColumn("dbo.CourtCases", "CaseStatus", c => c.String());
            AlterColumn("dbo.CourtCases", "SelectedBeforeCourt", c => c.String());
            AlterColumn("dbo.CourtCases", "LaborConflicPlace", c => c.String());
            AlterColumn("dbo.CourtCases", "LaborConflicNo", c => c.String());
            AlterColumn("dbo.CourtCases", "PAPCPlace", c => c.String());
            AlterColumn("dbo.CourtCases", "PAPCNo", c => c.String());
            AlterColumn("dbo.CourtCases", "PublicPlace", c => c.String());
            AlterColumn("dbo.CourtCases", "PublicProsecutionNo", c => c.String());
            AlterColumn("dbo.CourtCases", "PoliceStation", c => c.String());
            AlterColumn("dbo.CourtCases", "PoliceNo", c => c.String());
            AlterColumn("dbo.CourtCases", "ODBBankBranch", c => c.String());
            AlterColumn("dbo.CourtCases", "ODBLoanCode", c => c.String());
            AlterColumn("dbo.CourtCases", "LoanManager", c => c.String());
            AlterColumn("dbo.CourtCases", "ParentCourtId", c => c.String());
            AlterColumn("dbo.CourtCases", "CaseLevelCode", c => c.String());
            AlterColumn("dbo.CourtCases", "CaseTypeCode", c => c.String());
            AlterColumn("dbo.CourtCases", "ClientFileNo", c => c.String());
            AlterColumn("dbo.CourtCases", "AccountContractNo", c => c.String());
            AlterColumn("dbo.CourtCases", "ReceiveLevelCode", c => c.String());
            AlterColumn("dbo.CourtCases", "AgainstCode", c => c.String());
            AlterColumn("dbo.CourtCases", "Defendant", c => c.String(nullable: false));
            AlterColumn("dbo.CourtCases", "ClientCode", c => c.String(nullable: false));
            AlterColumn("dbo.CourtCases", "OfficeFileNo", c => c.String());
        }
    }
}
