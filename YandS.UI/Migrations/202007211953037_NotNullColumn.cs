namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotNullColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestOrderIssued", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesEnforcements", "ArrestOrderIssued", c => c.Boolean());
        }
    }
}
