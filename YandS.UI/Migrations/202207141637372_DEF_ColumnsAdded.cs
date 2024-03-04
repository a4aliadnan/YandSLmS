namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DEF_ColumnsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "DEF_DateOfContact", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo", c => c.String(maxLength: 20));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_Corresponding", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "DEF_Corresponding");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_DateOfContact");
        }
    }
}
