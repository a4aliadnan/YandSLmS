namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoiceCalculationTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CaseInvoiceFeeCalculations", "ClaimAmountPct", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.CaseInvoiceFeeCalculations", "FeeAmountPct", c => c.Decimal(nullable: false, precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CaseInvoiceFeeCalculations", "FeeAmountPct", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CaseInvoiceFeeCalculations", "ClaimAmountPct", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
