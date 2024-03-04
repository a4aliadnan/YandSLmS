namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewColumntoInvoiceFee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoices", "CourtRefNo", c => c.String());
            AddColumn("dbo.CaseInvoices", "InvClassification", c => c.String());
            AddColumn("dbo.CaseInvoiceFees", "CaseLevel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseInvoiceFees", "CaseLevel");
            DropColumn("dbo.CaseInvoices", "InvClassification");
            DropColumn("dbo.CaseInvoices", "CourtRefNo");
        }
    }
}
