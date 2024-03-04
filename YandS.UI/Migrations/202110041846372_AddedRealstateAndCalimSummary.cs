namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRealstateAndCalimSummary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "RealstateDetail", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.CaseRegistrations", "ClaimSummary", c => c.String());
            AddColumn("dbo.CaseRegistrations", "CourtFeeAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.CaseRegistrations", "ElectronicNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "ElectronicNo");
            DropColumn("dbo.CaseRegistrations", "CourtFeeAmount");
            DropColumn("dbo.CaseRegistrations", "ClaimSummary");
            DropColumn("dbo.CaseRegistrations", "RealstateDetail");
        }
    }
}
