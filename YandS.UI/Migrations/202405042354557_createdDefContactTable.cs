namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdDefContactTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefendentContact",
                c => new
                    {
                        DefendentContactId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CaseLevelCode = c.String(maxLength: 2),
                        DEF_DateOfContact = c.DateTime(precision: 7, storeType: "datetime2"),
                        DEF_MobileNo = c.String(maxLength: 4000),
                        DEF_Corresponding = c.String(maxLength: 4000),
                        DEF_CallerName = c.String(maxLength: 5),
                        DEF_LawyerId = c.String(maxLength: 500),
                        DEF_VisitDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        DEF_ContactType = c.String(maxLength: 2),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.DefendentContactId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefendentContact", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.DefendentContact", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.DefendentContact", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.DefendentContact", new[] { "UpdatedBy" });
            DropIndex("dbo.DefendentContact", new[] { "CreatedBy" });
            DropIndex("dbo.DefendentContact", new[] { "CaseId" });
            DropTable("dbo.DefendentContact");
        }
    }
}
