namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShiftedSessionUpdateNext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "NextHearingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.SessionsRolls", "NextHearingDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "NextHearingDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.CourtCases", "NextHearingDate");
        }
    }
}
