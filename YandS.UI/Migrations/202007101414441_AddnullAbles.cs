namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnullAbles : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "ClaimAmount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.CourtCases", "PrivateFee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "PrivateFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CourtCases", "ClaimAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
