namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PVNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "BillNumber", c => c.String());
            AddColumn("dbo.PayVoucher", "FutureChequeDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "FutureChequeDate");
            DropColumn("dbo.PayVoucher", "BillNumber");
        }
    }
}
