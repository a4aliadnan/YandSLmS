namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVATAmtColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "VatAmount", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "VatAmount");
        }
    }
}
