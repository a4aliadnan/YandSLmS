namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFinalClosureColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "AccountAuditDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "AccountAuditBy", c => c.String(maxLength: 30));
            AddColumn("dbo.CourtCases", "ClosureArchieveddBy", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "ClosureArchieveddBy");
            DropColumn("dbo.CourtCases", "AccountAuditBy");
            DropColumn("dbo.CourtCases", "AccountAuditDate");
        }
    }
}
