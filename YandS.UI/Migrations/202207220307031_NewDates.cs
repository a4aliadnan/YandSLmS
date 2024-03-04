namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "FirstEmailDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "CommissioningDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "CommissioningDate");
            DropColumn("dbo.CourtCases", "FirstEmailDate");
        }
    }
}
