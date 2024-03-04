namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTableForTranslation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DecisionTranslations",
                c => new
                    {
                        TranslationId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CurrentHearingDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CourtDecision = c.String(),
                        CourtDecisionTranslated = c.String(),
                        TranslationDone = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TranslationId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DecisionTranslations", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.DecisionTranslations", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.DecisionTranslations", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.DecisionTranslations", new[] { "UpdatedBy" });
            DropIndex("dbo.DecisionTranslations", new[] { "CreatedBy" });
            DropIndex("dbo.DecisionTranslations", new[] { "CaseId" });
            DropTable("dbo.DecisionTranslations");
        }
    }
}
