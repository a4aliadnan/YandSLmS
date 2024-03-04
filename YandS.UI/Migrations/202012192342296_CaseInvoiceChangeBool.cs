namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseInvoiceChangeBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CaseInvoices", "ShowInInvoice", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CaseInvoices", "IsLastInvoice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CaseInvoices", "IsLastInvoice", c => c.Boolean());
            AlterColumn("dbo.CaseInvoices", "ShowInInvoice", c => c.Boolean());
        }
    }
}
