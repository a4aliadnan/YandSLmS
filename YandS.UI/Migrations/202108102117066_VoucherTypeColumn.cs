namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoucherTypeColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "Voucher_No", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "Voucher_No");
        }
    }
}
