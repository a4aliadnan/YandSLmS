namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedOfficeFileStstusColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CaseRegistrations", "FileStatus", c => c.String(maxLength: 10));
            AlterColumn("dbo.CourtCasesDetails", "CourtDepartment", c => c.String(maxLength: 10));
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementlevelId", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "EnforcementlevelId", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCasesDetails", "CourtDepartment", c => c.String(maxLength: 2));
            AlterColumn("dbo.CaseRegistrations", "FileStatus", c => c.String(maxLength: 3));
        }
    }
}
