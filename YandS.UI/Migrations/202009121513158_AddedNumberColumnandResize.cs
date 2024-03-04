namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberColumnandResize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoiceFees", "FeeTypeDetail", c => c.String());
            AlterColumn("dbo.PayVoucher", "Payment_To", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "Payment_To", c => c.String(maxLength: 3));
            DropColumn("dbo.CaseInvoiceFees", "FeeTypeDetail");
        }
    }
}
