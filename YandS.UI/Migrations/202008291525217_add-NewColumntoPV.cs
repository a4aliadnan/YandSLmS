namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumntoPV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "TransTypeCode", c => c.String(maxLength: 3));
            AddColumn("dbo.PayVoucher", "TransReasonCode", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "TransReasonCode");
            DropColumn("dbo.PayVoucher", "TransTypeCode");
        }
    }
}
