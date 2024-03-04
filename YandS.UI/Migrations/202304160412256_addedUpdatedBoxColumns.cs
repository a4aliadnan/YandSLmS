namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUpdatedBoxColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "UpdateBoxDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "UpdateBoxBy", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "UpdateBoxBy");
            DropColumn("dbo.CourtCases", "UpdateBoxDate");
        }
    }
}
