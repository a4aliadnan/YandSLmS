namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewVoucherColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "Payment_To", c => c.String(maxLength: 3));
            AddColumn("dbo.PayVoucher", "Cheque_Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "Cheque_Date");
            DropColumn("dbo.PayVoucher", "Payment_To");
        }
    }
}
