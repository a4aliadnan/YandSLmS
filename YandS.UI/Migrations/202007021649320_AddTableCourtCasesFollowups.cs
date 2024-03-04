namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableCourtCasesFollowups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtCasesFollowups",
                c => new
                    {
                        FollowupId = c.Int(nullable: false, identity: true),
                        DetailId = c.Int(nullable: false),
                        HearingDate = c.DateTime(nullable: false),
                        HearingRemarks = c.String(),
                        NextHearingDate = c.DateTime(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.FollowupId)
                .ForeignKey("dbo.CourtCasesDetails", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.DetailId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtCasesFollowups", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesFollowups", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.CourtCasesFollowups", "DetailId", "dbo.CourtCasesDetails");
            DropIndex("dbo.CourtCasesFollowups", new[] { "UpdatedBy" });
            DropIndex("dbo.CourtCasesFollowups", new[] { "CreatedBy" });
            DropIndex("dbo.CourtCasesFollowups", new[] { "DetailId" });
            DropTable("dbo.CourtCasesFollowups");
        }
    }
}
