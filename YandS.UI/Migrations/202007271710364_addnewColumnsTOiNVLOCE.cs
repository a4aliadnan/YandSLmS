namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewColumnsTOiNVLOCE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoices", "ClaimAmountPct", c => c.Double());
            AddColumn("dbo.CaseInvoices", "FeeAmountPct", c => c.Double());
            AddColumn("dbo.CaseInvoices", "ShowInInvoice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseInvoices", "ShowInInvoice");
            DropColumn("dbo.CaseInvoices", "FeeAmountPct");
            DropColumn("dbo.CaseInvoices", "ClaimAmountPct");
        }
    }
}
