namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInvoiceCalculationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CaseInvoiceFeeCalculations",
                c => new
                    {
                        FeeCaclId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        ClaimAmountPct = c.Decimal(nullable: false, precision: 18, scale: 3),
                        FeeAmountPct = c.Decimal(nullable: false, precision: 18, scale: 3),
                    })
                .PrimaryKey(t => t.FeeCaclId)
                .ForeignKey("dbo.CaseInvoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CaseInvoiceFeeCalculations", "InvoiceId", "dbo.CaseInvoices");
            DropIndex("dbo.CaseInvoiceFeeCalculations", new[] { "InvoiceId" });
            DropTable("dbo.CaseInvoiceFeeCalculations");
        }
    }
}
