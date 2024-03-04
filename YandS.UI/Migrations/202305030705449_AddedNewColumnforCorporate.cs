namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnforCorporate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "CorporateFee", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.CourtCases", "CorporateWorkDetail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "CorporateWorkDetail");
            DropColumn("dbo.CourtCases", "CorporateFee");
        }
    }
}
