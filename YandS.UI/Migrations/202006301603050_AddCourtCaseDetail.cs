namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourtCaseDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtCasesDetails",
                c => new
                    {
                        DetailId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        Courtid = c.String(),
                        CourtRefNo = c.String(),
                        CourtLocationid = c.String(),
                        RegistrationDate = c.DateTime(),
                        CourtDepartment = c.String(),
                        CaseLevelCode = c.String(),
                        CourtStatus = c.String(),
                        JudgementDate = c.DateTime(),
                        JudgementReceivingDate = c.DateTime(),
                        JudgementDetails = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.CourtCases", "PrivateFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CourtCases", "DefClientCode");
            DropColumn("dbo.CourtCases", "CaseStatusCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtCases", "CaseStatusCode", c => c.String());
            AddColumn("dbo.CourtCases", "DefClientCode", c => c.String());
            DropForeignKey("dbo.CourtCasesDetails", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesDetails", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesDetails", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.CourtCasesDetails", new[] { "UpdatedBy" });
            DropIndex("dbo.CourtCasesDetails", new[] { "CreatedBy" });
            DropIndex("dbo.CourtCasesDetails", new[] { "CaseId" });
            DropColumn("dbo.CourtCases", "PrivateFee");
            DropTable("dbo.CourtCasesDetails");
        }
    }
}
