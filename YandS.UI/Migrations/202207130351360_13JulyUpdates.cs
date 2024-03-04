namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13JulyUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "LawyerId", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "LawyerId");
        }
    }
}
