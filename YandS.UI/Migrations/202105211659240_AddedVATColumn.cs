namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVATColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoiceFees", "VATPct", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseInvoiceFees", "VATPct");
        }
    }
}
