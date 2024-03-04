namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceandInvoiceFeeTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseInvoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        InvoiceNumber = c.String(),
                        CaseId = c.Int(nullable: false),
                        CourtType = c.String(nullable: false, maxLength: 2),
                        InvoiceStatus = c.String(),
                        TransferType = c.String(),
                        TransferNumber = c.String(),
                        TransferDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.CourtCases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CaseId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.CaseInvoiceFees",
                c => new
                    {
                        FeeId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        FeeTypeId = c.String(),
                        FeeAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FeeId)
                .ForeignKey("dbo.CaseInvoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseInvoices", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.CaseInvoiceFees", "InvoiceId", "dbo.CaseInvoices");
            DropForeignKey("dbo.CaseInvoices", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.CaseInvoices", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.CaseInvoiceFees", new[] { "InvoiceId" });
            DropIndex("dbo.CaseInvoices", new[] { "UpdatedBy" });
            DropIndex("dbo.CaseInvoices", new[] { "CreatedBy" });
            DropIndex("dbo.CaseInvoices", new[] { "CaseId" });
            DropTable("dbo.CaseInvoiceFees");
            DropTable("dbo.CaseInvoices");
        }
    }
}
