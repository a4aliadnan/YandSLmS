namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRealEstate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "RealEstateYesNo", c => c.String());
            AddColumn("dbo.CaseRegistrations", "RealEstateDetail", c => c.String());
            DropColumn("dbo.CaseRegistrations", "RealstateDetail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CaseRegistrations", "RealstateDetail", c => c.Boolean(nullable: false));
            DropColumn("dbo.CaseRegistrations", "RealEstateDetail");
            DropColumn("dbo.CaseRegistrations", "RealEstateYesNo");
        }
    }
}
