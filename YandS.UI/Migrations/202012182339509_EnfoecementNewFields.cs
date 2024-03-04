namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnfoecementNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "EnforcementBy", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ArrestName", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ArrestIDNo", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SupremeObjectionNo", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SupremeObjectionCourt", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SupremePlaintNo", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "SupremePlaintCourt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "SupremePlaintCourt");
            DropColumn("dbo.CourtCasesEnforcements", "SupremePlaintNo");
            DropColumn("dbo.CourtCasesEnforcements", "SupremeObjectionCourt");
            DropColumn("dbo.CourtCasesEnforcements", "SupremeObjectionNo");
            DropColumn("dbo.CourtCasesEnforcements", "ArrestIDNo");
            DropColumn("dbo.CourtCasesEnforcements", "ArrestName");
            DropColumn("dbo.CourtCasesEnforcements", "EnforcementBy");
        }
    }
}
