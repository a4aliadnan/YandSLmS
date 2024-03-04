namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnOmaniExp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "OmaniExp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "OmaniExp");
        }
    }
}
