namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionRollLatestChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "ShowFollowup", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.SessionsRolls", "ShowSuspend", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.SessionsRolls", "DifferentPanelYesSet", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "DifferentPanelNotes", c => c.String());
            AddColumn("dbo.SessionsRolls", "SupremeJudgementsDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "SupremeJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "SupremeIsFavorable", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "SupremeJDReceiveDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "SupremeFinalJDAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.SessionsRolls", "SupremeFinalJudgedInterests", c => c.String(maxLength: 255));
            DropColumn("dbo.SessionsRolls", "PrimaryAllJudgements");
            DropColumn("dbo.SessionsRolls", "AppealAllJudgements");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "AppealAllJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "PrimaryAllJudgements", c => c.String());
            DropColumn("dbo.SessionsRolls", "SupremeFinalJudgedInterests");
            DropColumn("dbo.SessionsRolls", "SupremeFinalJDAmount");
            DropColumn("dbo.SessionsRolls", "SupremeJDReceiveDate");
            DropColumn("dbo.SessionsRolls", "SupremeIsFavorable");
            DropColumn("dbo.SessionsRolls", "SupremeJudgements");
            DropColumn("dbo.SessionsRolls", "SupremeJudgementsDate");
            DropColumn("dbo.SessionsRolls", "DifferentPanelNotes");
            DropColumn("dbo.SessionsRolls", "DifferentPanelYesSet");
            DropColumn("dbo.SessionsRolls", "ShowSuspend");
            DropColumn("dbo.SessionsRolls", "ShowFollowup");
        }
    }
}
