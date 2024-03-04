namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArcheveInPayVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "IsDeleted");
        }
    }
}
