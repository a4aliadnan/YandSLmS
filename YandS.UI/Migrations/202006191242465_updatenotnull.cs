namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayVoucher", "Credit_Account", c => c.Int());
            AlterColumn("dbo.PayVoucher", "Received_on", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "Received_on", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.PayVoucher", "Credit_Account", c => c.Int(nullable: false));
        }
    }
}
