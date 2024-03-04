namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSyncColumninCourtDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesDetails", "RealEstateYesNo", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "RealEstateDetail", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "ClaimSummary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesDetails", "ClaimSummary");
            DropColumn("dbo.CourtCasesDetails", "RealEstateDetail");
            DropColumn("dbo.CourtCasesDetails", "RealEstateYesNo");
        }
    }
}
