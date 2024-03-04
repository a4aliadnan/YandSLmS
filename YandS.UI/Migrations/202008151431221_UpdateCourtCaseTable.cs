namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCourtCaseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "IsPrivateFee", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourtCases", "IsCommission", c => c.Boolean(nullable: false));
            AddColumn("dbo.CourtCases", "CRRegistrationNo", c => c.String());
            DropColumn("dbo.CourtCases", "PrivateFee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtCases", "PrivateFee", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.CourtCases", "CRRegistrationNo");
            DropColumn("dbo.CourtCases", "IsCommission");
            DropColumn("dbo.CourtCases", "IsPrivateFee");
        }
    }
}
