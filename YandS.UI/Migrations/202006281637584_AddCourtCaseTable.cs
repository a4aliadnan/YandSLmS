namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourtCaseTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtCases",
                c => new
                    {
                        CaseId = c.Int(nullable: false, identity: true),
                        OfficeFileNo = c.String(),
                        ClientCode = c.String(nullable: false),
                        Defendant = c.String(),
                        DefClientCode = c.String(),
                        AgainstCode = c.String(),
                        ReceptionDate = c.DateTime(),
                        ReceiveLevelCode = c.String(),
                        AccountContractNo = c.String(),
                        ClientFileNo = c.String(),
                        ClaimAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CaseTypeCode = c.String(),
                        CaseLevelCode = c.String(),
                        LegalNoticeDate = c.DateTime(),
                        ODBLoanCode = c.String(),
                        ODBBankBranch = c.String(),
                        CaseStatusCode = c.String(),
                        PoliceNo = c.String(),
                        PoliceStation = c.String(),
                        PublicProsecutionNo = c.String(),
                        PublicPlace = c.String(),
                        PAPCNo = c.String(),
                        PAPCPlace = c.String(),
                        LaborConflicNo = c.String(),
                        LaborConflicPlace = c.String(),
                        YandSNotes = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CaseId)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtCases", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCases", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.CourtCases", new[] { "UpdatedBy" });
            DropIndex("dbo.CourtCases", new[] { "CreatedBy" });
            DropTable("dbo.CourtCases");
        }
    }
}
