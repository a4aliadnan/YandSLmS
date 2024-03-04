namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveClosureLevel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourtCases", "ClosureLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourtCases", "ClosureLevel", c => c.String());
        }
    }
}
