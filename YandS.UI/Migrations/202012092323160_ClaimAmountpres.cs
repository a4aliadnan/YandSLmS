namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClaimAmountpres : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "ClaimAmount", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "ClaimAmount", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
