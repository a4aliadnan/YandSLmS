namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDisputeandTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "CurrentDisputeLevelandType", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "CurrentDisputeLevelandType");
        }
    }
}
