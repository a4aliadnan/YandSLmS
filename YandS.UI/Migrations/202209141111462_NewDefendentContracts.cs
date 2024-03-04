namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDefendentContracts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo2", c => c.String(maxLength: 20));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo3", c => c.String(maxLength: 20));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo4", c => c.String(maxLength: 20));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo5", c => c.String(maxLength: 20));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_CallerName", c => c.String(maxLength: 5));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_LawyerId", c => c.String(maxLength: 500));
            AddColumn("dbo.CourtCasesEnforcements", "DEF_VisitDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "DEF_VisitDate");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_LawyerId");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_CallerName");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo5");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo4");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo3");
            DropColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo2");
        }
    }
}
