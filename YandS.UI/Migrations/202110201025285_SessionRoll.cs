namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionRoll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SessionsRolls",
                c => new
                    {
                        SessionRollId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        ExpectedDtToClose = c.DateTime(precision: 7, storeType: "datetime2"),
                        SessionRollClientName = c.String(maxLength: 255),
                        SessionRollDefendentName = c.String(maxLength: 255),
                        SessionLevel = c.String(maxLength: 2),
                        CountLocationId = c.String(maxLength: 5),
                        CaseType = c.String(maxLength: 2),
                        LawyerId = c.String(maxLength: 5),
                        SessionRemarks = c.String(),
                        CurrentHearingDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CourtDecision = c.String(),
                        NextHearingDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CaseDismisses = c.String(maxLength: 1),
                        Requirements = c.String(maxLength: 500),
                        CourtRegistration = c.String(),
                        BeforeDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FollowerId = c.String(maxLength: 5),
                        FinalJDDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FinalJDAmount = c.Decimal(precision: 18, scale: 3),
                        FinalJudgedInterests = c.String(maxLength: 255),
                        FinalReceiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        IsJudgementMatching = c.String(maxLength: 1),
                        AllJudgements = c.String(),
                        PrimaryJudgementsDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PrimaryJudgements = c.String(),
                        PrimaryIsFavorable = c.String(maxLength: 1),
                        PrimaryJDReceiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AppealJudgementsDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        AppealJudgements = c.String(),
                        AppealIsFavorable = c.String(maxLength: 1),
                        AppealJDReceiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SupremeJudgementsDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SupremeJudgements = c.String(),
                        SupremeIsFavorable = c.String(maxLength: 1),
                        SupremeJDReceiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EnforcementJudgementsDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        EnforcementJudgements = c.String(),
                        EnforcementIsFavorable = c.String(maxLength: 1),
                        EnforcementJDReceiveDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LastDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        WorkRequired = c.String(maxLength: 500),
                        SessionNotes = c.String(maxLength: 500),
                        DeletedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                        DeletedBy = c.Int(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SessionRollId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SessionsRolls", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.SessionsRolls", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.SessionsRolls", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.SessionsRolls", new[] { "UpdatedBy" });
            DropIndex("dbo.SessionsRolls", new[] { "CreatedBy" });
            DropIndex("dbo.SessionsRolls", new[] { "CaseId" });
            DropTable("dbo.SessionsRolls");
        }
    }
}
