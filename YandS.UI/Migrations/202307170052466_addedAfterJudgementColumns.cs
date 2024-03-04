namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAfterJudgementColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "ActionDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CourtCases", "EnforcementAdmin", c => c.String(maxLength: 5));
            AddColumn("dbo.CourtCases", "SessionNote_Remark", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "SessionNote_Remark");
            DropColumn("dbo.CourtCases", "EnforcementAdmin");
            DropColumn("dbo.CourtCases", "ActionDate");
        }
    }
}
