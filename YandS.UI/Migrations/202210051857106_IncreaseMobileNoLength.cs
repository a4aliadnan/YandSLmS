namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseMobileNoLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "DEF_MobileNo", c => c.String(maxLength: 20));
        }
    }
}
