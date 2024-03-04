namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnsFromCaseInvoice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CaseInvoices", "ClaimAmountPct");
            DropColumn("dbo.CaseInvoices", "FeeAmountPct");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CaseInvoices", "FeeAmountPct", c => c.Double());
            AddColumn("dbo.CaseInvoices", "ClaimAmountPct", c => c.Double());
        }
    }
}
