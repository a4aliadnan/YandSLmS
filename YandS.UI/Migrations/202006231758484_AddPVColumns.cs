namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPVColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "VoucherType", c => c.Int(nullable: false));
            AddColumn("dbo.PayVoucher", "PV_No", c => c.String());
            AddColumn("dbo.PayVoucher", "AuthorizeBy", c => c.Int());
            AddColumn("dbo.PayVoucher", "AuthorizeDate", c => c.DateTime());
            AddColumn("dbo.PayVoucher", "VoucherStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "VoucherStatus");
            DropColumn("dbo.PayVoucher", "AuthorizeDate");
            DropColumn("dbo.PayVoucher", "AuthorizeBy");
            DropColumn("dbo.PayVoucher", "PV_No");
            DropColumn("dbo.PayVoucher", "VoucherType");
        }
    }
}
