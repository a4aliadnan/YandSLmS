namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceFee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourtCaseStatusDetails",
                c => new
                    {
                        StatusDetailId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        StatusCode = c.String(),
                        StatusDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.StatusDetailId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .Index(t => t.CaseId);
            
            AddColumn("dbo.CaseInvoices", "Credit_Account", c => c.Int(nullable: false));
            AddColumn("dbo.CourtCases", "CaseStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourtCaseStatusDetails", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.CourtCaseStatusDetails", new[] { "CaseId" });
            DropColumn("dbo.CourtCases", "CaseStatus");
            DropColumn("dbo.CaseInvoices", "Credit_Account");
            DropTable("dbo.CourtCaseStatusDetails");
        }
    }
}
