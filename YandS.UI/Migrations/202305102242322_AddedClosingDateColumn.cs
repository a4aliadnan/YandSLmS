namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClosingDateColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ClosingNotesDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "ClosingNotesDate");
        }
    }
}
