namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedInvestDDL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesDetails", "DepartmentType", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesDetails", "DepartmentType");
        }
    }
}
