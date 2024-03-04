namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClosureDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClosurePartialDetails",
                c => new
                    {
                        PartDetailId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        ClosurePartDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        FileTypeClosure = c.String(maxLength: 1),
                        PartNo = c.String(maxLength: 1),
                        ClosingNotesDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ClosingNotes = c.String(),
                        ClosureApprovalStatus = c.String(maxLength: 1),
                        ClosureInitiatedBy = c.String(maxLength: 30),
                        ClosureApproveddBy = c.String(maxLength: 30),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PartDetailId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AddColumn("dbo.CourtCases", "StoreDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "StoreNotes", c => c.String());
            AddColumn("dbo.CourtCases", "FinalClosureApprovalStatus", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCases", "FinalClosureInitiatedBy", c => c.String(maxLength: 30));
            AddColumn("dbo.CourtCases", "FinalClosureApproveddBy", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClosurePartialDetails", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.ClosurePartialDetails", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.ClosurePartialDetails", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.ClosurePartialDetails", new[] { "UpdatedBy" });
            DropIndex("dbo.ClosurePartialDetails", new[] { "CreatedBy" });
            DropIndex("dbo.ClosurePartialDetails", new[] { "CaseId" });
            DropColumn("dbo.CourtCases", "FinalClosureApproveddBy");
            DropColumn("dbo.CourtCases", "FinalClosureInitiatedBy");
            DropColumn("dbo.CourtCases", "FinalClosureApprovalStatus");
            DropColumn("dbo.CourtCases", "StoreNotes");
            DropColumn("dbo.CourtCases", "StoreDate");
            DropTable("dbo.ClosurePartialDetails");
        }
    }
}
