namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseInvoiceChangerequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CaseInvoices", "ExpectedFees", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CaseInvoices", "ExpectedFees", c => c.String());
        }
    }
}
