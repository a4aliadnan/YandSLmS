namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnEnforcementInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "EnforcementInfoInvoice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "EnforcementInfoInvoice");
        }
    }
}
