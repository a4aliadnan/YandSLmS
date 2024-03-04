namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEnforcementAdminColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "EnforcementAdmin", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "EnforcementAdmin");
        }
    }
}
