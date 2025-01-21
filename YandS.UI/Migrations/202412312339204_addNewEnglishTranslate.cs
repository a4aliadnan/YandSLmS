namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewEnglishTranslate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnglishDecisions",
                c => new
                    {
                        DecisionId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CurrentHearingDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CourtDecision = c.String(),
                    })
                .PrimaryKey(t => t.DecisionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EnglishDecisions");
        }
    }
}
