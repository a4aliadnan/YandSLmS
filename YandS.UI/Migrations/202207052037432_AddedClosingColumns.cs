namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClosingColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ReasonCode", c => c.String());
            AddColumn("dbo.CourtCases", "ClosureLevel", c => c.String());
            AddColumn("dbo.CourtCases", "FileAllocation", c => c.String());
            AddColumn("dbo.CourtCases", "LitigationFileClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "LitigationFileClosureDate");
            DropColumn("dbo.CourtCases", "FileAllocation");
            DropColumn("dbo.CourtCases", "ClosureLevel");
            DropColumn("dbo.CourtCases", "ReasonCode");
        }
    }
}
