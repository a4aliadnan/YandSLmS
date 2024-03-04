namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumns14Jun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "FinalOnvoiceOnHold", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCases", "SuggestedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "TransportationFee", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "TransportationFee");
            DropColumn("dbo.CourtCases", "SuggestedDate");
            DropColumn("dbo.CourtCases", "FinalOnvoiceOnHold");
        }
    }
}
