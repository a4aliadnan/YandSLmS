namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransportationSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "TransportationSource", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "TransportationSource");
        }
    }
}
