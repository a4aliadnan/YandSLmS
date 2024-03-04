namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnBankTransferAuth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "BankTransferStaff", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "BankTransferStaff");
        }
    }
}
