namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewToBeRegisterFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ClientClassificationCode", c => c.String(nullable: false));
            AddColumn("dbo.CourtCases", "CaseSubject", c => c.String());
            AddColumn("dbo.CourtCases", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "Subject");
            DropColumn("dbo.CourtCases", "CaseSubject");
            DropColumn("dbo.CourtCases", "ClientClassificationCode");
        }
    }
}
