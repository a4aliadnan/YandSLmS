namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedInvoiceProcessedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "InvoiceProcessDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "InvoiceProcessDate");
        }
    }
}
