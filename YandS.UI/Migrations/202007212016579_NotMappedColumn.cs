namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotMappedColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtName");
            DropColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtName");
            DropColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtName");
            DropColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtCasesEnforcements", "ApealObjectionCourtName", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "PrimaryObjectionCourtName", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "PrimaryPlaintCourtName", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ApealPlaintCourtName", c => c.String());
        }
    }
}
