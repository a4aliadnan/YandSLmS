namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedprecisionVATAmt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayVoucher", "VatAmount", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "VatAmount", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
