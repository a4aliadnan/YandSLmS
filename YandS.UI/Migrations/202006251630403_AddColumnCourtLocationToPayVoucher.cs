namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCourtLocationToPayVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "LocationCode", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "LocationCode");
        }
    }
}
