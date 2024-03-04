namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PDFRefAndSpecialNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "PDCRefNo", c => c.String());
            AddColumn("dbo.PayVoucher", "SpecialNotification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "SpecialNotification");
            DropColumn("dbo.PayVoucher", "PDCRefNo");
        }
    }
}
