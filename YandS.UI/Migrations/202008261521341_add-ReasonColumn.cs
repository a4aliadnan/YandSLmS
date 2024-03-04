namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReasonColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCaseStatusDetails", "ReasonCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCaseStatusDetails", "ReasonCode");
        }
    }
}
