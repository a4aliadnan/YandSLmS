namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayVoucher", "Credit_Account", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "Credit_Account", c => c.Int());
        }
    }
}
