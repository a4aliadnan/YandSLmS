namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShiftRequirementColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "Requirements", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "Requirements");
        }
    }
}
