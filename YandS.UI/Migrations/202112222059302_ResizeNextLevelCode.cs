namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResizeNextLevelCode : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevelCode", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesDetails", "NextCaseLevelCode", c => c.String(maxLength: 5));
        }
    }
}
