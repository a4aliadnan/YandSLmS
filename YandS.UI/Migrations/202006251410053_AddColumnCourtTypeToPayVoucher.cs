namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCourtTypeToPayVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "CourtType", c => c.String(maxLength: 2));
            AlterColumn("dbo.PayVoucher", "VoucherStatus", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "VoucherStatus", c => c.String(maxLength: 1));
            DropColumn("dbo.PayVoucher", "CourtType");
        }
    }
}
