namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShiftedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "StopEnfRequest", c => c.String(maxLength: 1));
            AddColumn("dbo.CourtCasesEnforcements", "CourtDepartment", c => c.String(maxLength: 2));
            DropColumn("dbo.CaseRegistrations", "StopEnfRequest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CaseRegistrations", "StopEnfRequest", c => c.String(maxLength: 1));
            DropColumn("dbo.CourtCasesEnforcements", "CourtDepartment");
            DropColumn("dbo.CourtCases", "StopEnfRequest");
        }
    }
}
