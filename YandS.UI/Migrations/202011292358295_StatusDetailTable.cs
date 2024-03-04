namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusDetailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCaseStatusDetails", "CurrentCaseLevel", c => c.String());
            AddColumn("dbo.CourtCaseStatusDetails", "ClosureLevel", c => c.String());
            AddColumn("dbo.CourtCaseStatusDetails", "FileAllocation", c => c.String());
            AddColumn("dbo.CourtCaseStatusDetails", "LitigationFileClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCaseStatusDetails", "TempMonth", c => c.Int());
            AddColumn("dbo.CourtCaseStatusDetails", "CreatedBy", c => c.Int());
            AddColumn("dbo.CourtCaseStatusDetails", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCaseStatusDetails", "CreatedOn");
            DropColumn("dbo.CourtCaseStatusDetails", "CreatedBy");
            DropColumn("dbo.CourtCaseStatusDetails", "TempMonth");
            DropColumn("dbo.CourtCaseStatusDetails", "LitigationFileClosureDate");
            DropColumn("dbo.CourtCaseStatusDetails", "FileAllocation");
            DropColumn("dbo.CourtCaseStatusDetails", "ClosureLevel");
            DropColumn("dbo.CourtCaseStatusDetails", "CurrentCaseLevel");
        }
    }
}
