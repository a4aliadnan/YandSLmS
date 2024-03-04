namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSessionRoll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "SessionClientId", c => c.String(maxLength: 5));
            AddColumn("dbo.SessionsRolls", "PrimaryFinalJDAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.SessionsRolls", "PrimaryFinalJudgedInterests", c => c.String(maxLength: 255));
            AddColumn("dbo.SessionsRolls", "PrimaryAllJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "AppealFinalJDAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.SessionsRolls", "AppealFinalJudgedInterests", c => c.String(maxLength: 255));
            AddColumn("dbo.SessionsRolls", "AppealAllJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "FollowNotes", c => c.String());
            DropColumn("dbo.SessionsRolls", "ExpectedDtToClose");
            DropColumn("dbo.SessionsRolls", "SessionRollClientName");
            DropColumn("dbo.SessionsRolls", "CaseDismisses");
            DropColumn("dbo.SessionsRolls", "CourtRegistration");
            DropColumn("dbo.SessionsRolls", "FinalJDDate");
            DropColumn("dbo.SessionsRolls", "FinalJDAmount");
            DropColumn("dbo.SessionsRolls", "FinalJudgedInterests");
            DropColumn("dbo.SessionsRolls", "FinalReceiveDate");
            DropColumn("dbo.SessionsRolls", "IsJudgementMatching");
            DropColumn("dbo.SessionsRolls", "AllJudgements");
            DropColumn("dbo.SessionsRolls", "SupremeJudgementsDate");
            DropColumn("dbo.SessionsRolls", "SupremeJudgements");
            DropColumn("dbo.SessionsRolls", "SupremeIsFavorable");
            DropColumn("dbo.SessionsRolls", "SupremeJDReceiveDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "SupremeJDReceiveDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "SupremeIsFavorable", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "SupremeJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "SupremeJudgementsDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "AllJudgements", c => c.String());
            AddColumn("dbo.SessionsRolls", "IsJudgementMatching", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "FinalReceiveDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "FinalJudgedInterests", c => c.String(maxLength: 255));
            AddColumn("dbo.SessionsRolls", "FinalJDAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.SessionsRolls", "FinalJDDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "CourtRegistration", c => c.String());
            AddColumn("dbo.SessionsRolls", "CaseDismisses", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "SessionRollClientName", c => c.String(maxLength: 255));
            AddColumn("dbo.SessionsRolls", "ExpectedDtToClose", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("dbo.SessionsRolls", "FollowNotes");
            DropColumn("dbo.SessionsRolls", "AppealAllJudgements");
            DropColumn("dbo.SessionsRolls", "AppealFinalJudgedInterests");
            DropColumn("dbo.SessionsRolls", "AppealFinalJDAmount");
            DropColumn("dbo.SessionsRolls", "PrimaryAllJudgements");
            DropColumn("dbo.SessionsRolls", "PrimaryFinalJudgedInterests");
            DropColumn("dbo.SessionsRolls", "PrimaryFinalJDAmount");
            DropColumn("dbo.SessionsRolls", "SessionClientId");
        }
    }
}
