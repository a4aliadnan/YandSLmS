namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataTypePVVoucherType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayVoucher", "VoucherType", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.PayVoucher", "VoucherStatus", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "VoucherStatus", c => c.Int());
            AlterColumn("dbo.PayVoucher", "VoucherType", c => c.Int(nullable: false));
        }
    }
}
