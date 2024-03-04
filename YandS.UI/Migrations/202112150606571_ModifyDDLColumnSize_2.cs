namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyDDLColumnSize_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CaseRegistrations", "ActionLevel", c => c.String(maxLength: 3));
            AlterColumn("dbo.CaseRegistrations", "CourtRegistration", c => c.String(maxLength: 3));
            AlterColumn("dbo.CaseRegistrations", "FileStatus", c => c.String(maxLength: 3));
            AlterColumn("dbo.CaseRegistrations", "FileStatusRemarks", c => c.String(maxLength: 255));
            AlterColumn("dbo.CaseRegistrations", "AssignedTo", c => c.String(maxLength: 255));
            AlterColumn("dbo.CaseRegistrations", "DepartmentType", c => c.String(maxLength: 3));
            AlterColumn("dbo.CaseRegistrations", "RealEstateYesNo", c => c.String(maxLength: 3));
            AlterColumn("dbo.CaseRegistrations", "ElectronicNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesDetails", "Courtid", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCasesDetails", "CourtRefNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesDetails", "CourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesDetails", "CourtDepartment", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCasesDetails", "CaseLevelCode", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCasesDetails", "CourtStatus", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesDetails", "ApealByWho", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCasesDetails", "ClosedbyStaff", c => c.String(maxLength: 50));
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevel", c => c.String(maxLength: 50));
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevelCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.CourtCasesDetails", "RealEstateYesNo", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCasesDetails", "RealEstateDetail", c => c.String(maxLength: 4000));
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "Courtid", c => c.String(maxLength: 2));
            AlterColumn("dbo.CourtCasesEnforcements", "CourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementlevelId", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCasesEnforcements", "SuspensionCauseId", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCasesEnforcements", "PoliceStationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtLocationid", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementInfoInvoice", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ClosedbyStaff", c => c.String(maxLength: 50));
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementBy", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestName", c => c.String(maxLength: 255));
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestIDNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "SupremeObjectionNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "SupremeObjectionCourt", c => c.String(maxLength: 5));
            AlterColumn("dbo.CourtCasesEnforcements", "SupremePlaintNo", c => c.String(maxLength: 100));
            AlterColumn("dbo.CourtCasesEnforcements", "SupremePlaintCourt", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "SupremePlaintCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "SupremePlaintNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "SupremeObjectionCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "SupremeObjectionNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestIDNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestName", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementBy", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ClosedbyStaff", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementInfoInvoice", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealPlaintNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "ApealObjectionNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourt", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionNo", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "PoliceStationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "SuspensionCauseId", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementlevelId", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "CourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "Courtid", c => c.String());
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementNo", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "RealEstateDetail", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "RealEstateYesNo", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevelCode", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevel", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "ClosedbyStaff", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "ApealByWho", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "CourtStatus", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "CaseLevelCode", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "CourtDepartment", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "CourtLocationid", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "CourtRefNo", c => c.String());
            AlterColumn("dbo.CourtCasesDetails", "Courtid", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "ElectronicNo", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "RealEstateYesNo", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "DepartmentType", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "AssignedTo", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "FileStatusRemarks", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "FileStatus", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "CourtRegistration", c => c.String());
            AlterColumn("dbo.CaseRegistrations", "ActionLevel", c => c.String());
        }
    }
}
