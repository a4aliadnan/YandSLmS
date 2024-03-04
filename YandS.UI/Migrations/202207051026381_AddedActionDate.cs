namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActionDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "ActionDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "ActionDate");
        }
    }
}
