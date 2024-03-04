namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCLosingWindowsEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ReOpenEnforcement", c => c.String(maxLength: 1, defaultValue: "0"));
            AddColumn("dbo.CourtCases", "FinanceFileClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "FinanceFileClosureDate");
            DropColumn("dbo.CourtCases", "ReOpenEnforcement");
        }
    }
}
