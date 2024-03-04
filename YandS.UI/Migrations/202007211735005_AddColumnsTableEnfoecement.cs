namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsTableEnfoecement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "CourtLocationid", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtLocationid", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtLocationid", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtLocationid", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtLocationid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtLocationid");
            DropColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtLocationid");
            DropColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtLocationid");
            DropColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtLocationid");
            DropColumn("dbo.CourtCasesEnforcements", "CourtLocationid");
        }
    }
}
