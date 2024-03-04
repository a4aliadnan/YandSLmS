namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewColumntocases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "SpecialInstructions", c => c.String());
            AddColumn("dbo.CourtCases", "ClientCaseType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "ClientCaseType");
            DropColumn("dbo.CourtCases", "SpecialInstructions");
        }
    }
}
