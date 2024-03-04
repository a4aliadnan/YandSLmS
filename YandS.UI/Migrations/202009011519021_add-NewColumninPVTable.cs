namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumninPVTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "Payment_Head_Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "Payment_Head_Remarks");
        }
    }
}
