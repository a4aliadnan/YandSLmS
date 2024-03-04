namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoiceFees", "InvClassification", c => c.String());
            DropColumn("dbo.CaseInvoices", "InvClassification");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CaseInvoices", "InvClassification", c => c.String());
            DropColumn("dbo.CaseInvoiceFees", "InvClassification");
        }
    }
}
