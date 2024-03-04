namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSelectedBeforeCourt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "SelectedBeforeCourt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "SelectedBeforeCourt");
        }
    }
}
