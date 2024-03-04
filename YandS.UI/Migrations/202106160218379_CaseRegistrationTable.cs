namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseRegistrationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseRegistrations",
                c => new
                    {
                        CaseRegistrationId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        ActionDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ActionLevel = c.String(),
                        JudgementDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        UrgentCaseDays = c.Int(),
                        EnforcementDispute = c.String(),
                        CourtRegistration = c.String(),
                        FileStatus = c.String(),
                        FileStatusRemarks = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.CaseRegistrationId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseRegistrations", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CaseRegistrations", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.CaseRegistrations", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.CaseRegistrations", new[] { "UpdatedBy" });
            DropIndex("dbo.CaseRegistrations", new[] { "CreatedBy" });
            DropIndex("dbo.CaseRegistrations", new[] { "CaseId" });
            DropTable("dbo.CaseRegistrations");
        }
    }
}
