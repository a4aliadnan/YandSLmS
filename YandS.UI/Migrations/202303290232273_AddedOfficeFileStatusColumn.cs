namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOfficeFileStatusColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "OfficeFileStatus", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "OfficeFileStatus");
        }
    }
}
