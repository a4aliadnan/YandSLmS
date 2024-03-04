namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_ModifyPayvoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "PaymentHeadDetail", c => c.String(maxLength: 6));
            AlterColumn("dbo.PayVoucher", "Payment_Type", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.PayVoucher", "Payment_Head", c => c.String(maxLength: 6));
            AlterColumn("dbo.PayVoucher", "Payment_Head_Remarks", c => c.String(maxLength: 500));
            AlterColumn("dbo.PayVoucher", "Payment_To", c => c.String(maxLength: 10));
            AlterColumn("dbo.PayVoucher", "Payment_Mode", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.PayVoucher", "Status", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.PayVoucher", "BillNumber", c => c.String(maxLength: 1000));
            AlterColumn("dbo.PayVoucher", "PDCRefNo", c => c.String(maxLength: 255));
            AlterColumn("dbo.PayVoucher", "SpecialNotification", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "SpecialNotification", c => c.String());
            AlterColumn("dbo.PayVoucher", "PDCRefNo", c => c.String());
            AlterColumn("dbo.PayVoucher", "BillNumber", c => c.String());
            AlterColumn("dbo.PayVoucher", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.PayVoucher", "Payment_Mode", c => c.String(nullable: false));
            AlterColumn("dbo.PayVoucher", "Payment_To", c => c.String(maxLength: 5));
            AlterColumn("dbo.PayVoucher", "Payment_Head_Remarks", c => c.String());
            AlterColumn("dbo.PayVoucher", "Payment_Head", c => c.String(maxLength: 3));
            AlterColumn("dbo.PayVoucher", "Payment_Type", c => c.String(nullable: false));
            DropColumn("dbo.PayVoucher", "PaymentHeadDetail");
        }
    }
}
