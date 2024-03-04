namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPaToBeneficiries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "PaymentToBenificry", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PayVoucher", "PaymentToBenificry");
        }
    }
}
