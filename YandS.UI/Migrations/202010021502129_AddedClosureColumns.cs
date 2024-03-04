namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClosureColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "ClosedbyStaff", c => c.String());
            AddColumn("dbo.CourtCasesDetails", "ClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCasesDetails", "ClosedbyStaff", c => c.String());
            AddColumn("dbo.CourtCasesEnforcements", "ClosureDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCasesEnforcements", "ClosedbyStaff", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "ClosedbyStaff");
            DropColumn("dbo.CourtCasesEnforcements", "ClosureDate");
            DropColumn("dbo.CourtCasesDetails", "ClosedbyStaff");
            DropColumn("dbo.CourtCasesDetails", "ClosureDate");
            DropColumn("dbo.CourtCases", "ClosedbyStaff");
            DropColumn("dbo.CourtCases", "ClosureDate");
        }
    }
}
