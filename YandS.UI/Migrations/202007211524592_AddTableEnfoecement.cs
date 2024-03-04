namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableEnfoecement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtCasesEnforcements",
                c => new
                    {
                        EnforcementId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        EnforcementNo = c.String(),
                        Courtid = c.String(),
                        RegistrationDate = c.DateTime(),
                        EnforcementlevelId = c.String(),
                        SuspensionStartDate = c.DateTime(),
                        SuspensionPeriod = c.Int(),
                        SuspensionCauseId = c.String(),
                        ArrestOrderIssued = c.Boolean(),
                        ArrestOrderStatusId = c.String(),
                        ArrestOrderNo = c.String(),
                        ArrestOrderIssueDate = c.DateTime(),
                        PoliceStationid = c.String(),
                        ObjectionDetail = c.String(),
                        ObjectionType = c.String(),
                        PrimaryObjectionNo = c.String(),
                        PrimaryObjectionCourt = c.String(),
                        ApealObjectionNo = c.String(),
                        ApealObjectionCourt = c.String(),
                        PlaintDetail = c.String(),
                        PlaintType = c.String(),
                        PrimaryPlaintNo = c.String(),
                        PrimaryPlaintCourt = c.String(),
                        ApealPlaintNo = c.String(),
                        ApealPlaintCourt = c.String(),
                        ApealPlaintCourtName = c.String(),
                        PrimaryPlaintCourtName = c.String(),
                        PrimaryObjectionCourtName = c.String(),
                        ApealObjectionCourtName = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.EnforcementId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtCasesEnforcements", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesEnforcements", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesEnforcements", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.CourtCasesEnforcements", new[] { "UpdatedBy" });
            DropIndex("dbo.CourtCasesEnforcements", new[] { "CreatedBy" });
            DropIndex("dbo.CourtCasesEnforcements", new[] { "CaseId" });
            DropTable("dbo.CourtCasesEnforcements");
        }
    }
}
