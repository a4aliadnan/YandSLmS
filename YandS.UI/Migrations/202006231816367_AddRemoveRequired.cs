namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRemoveRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PayVoucher", "Cheque_Number", c => c.String());
            AlterColumn("dbo.PayVoucher", "Cheque_Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayVoucher", "Cheque_Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PayVoucher", "Cheque_Number", c => c.String(nullable: false));
        }
    }
}
