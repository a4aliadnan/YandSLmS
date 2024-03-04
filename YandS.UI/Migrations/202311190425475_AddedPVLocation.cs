namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPVLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "PVLocation", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "PVLocation");
        }
    }
}
