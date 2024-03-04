namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewColumnInCourtCases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "IdRegistrationNo", c => c.String());
            AddColumn("dbo.CourtCases", "ParentCourtId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "ParentCourtId");
            DropColumn("dbo.CourtCases", "IdRegistrationNo");
        }
    }
}
