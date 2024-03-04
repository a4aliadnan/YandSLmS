namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedJudgementLevelColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "JudgementLevel", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "JudgementLevel");
        }
    }
}
