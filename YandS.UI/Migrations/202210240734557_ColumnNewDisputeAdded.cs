namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNewDisputeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "DisputeLevel", c => c.String(maxLength: 1));
            AddColumn("dbo.CaseRegistrations", "DisputeType", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "DisputeType");
            DropColumn("dbo.CaseRegistrations", "DisputeLevel");
        }
    }
}
