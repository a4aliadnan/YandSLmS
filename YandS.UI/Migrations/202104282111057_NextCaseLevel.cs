namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NextCaseLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ClosingNotes", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "NextCaseLevel", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "NextCaseLevelCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesDetails", "NextCaseLevelCode");
            DropColumn("dbo.CourtCasesDetails", "NextCaseLevel");
            DropColumn("dbo.CourtCases", "ClosingNotes");
        }
    }
}
