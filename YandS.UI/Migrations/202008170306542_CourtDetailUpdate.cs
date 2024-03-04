namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourtDetailUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesDetails", "ApealByWho", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesDetails", "ApealByWho");
        }
    }
}
