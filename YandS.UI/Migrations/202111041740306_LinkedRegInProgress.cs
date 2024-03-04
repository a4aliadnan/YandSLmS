namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkedRegInProgress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "PrimaryCrtInRegInProgress", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "AppealCrtInRegInProgress", c => c.String(maxLength: 1));
            AddColumn("dbo.SessionsRolls", "EnforcementCrtInRegInProgress", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "EnforcementCrtInRegInProgress");
            DropColumn("dbo.SessionsRolls", "AppealCrtInRegInProgress");
            DropColumn("dbo.SessionsRolls", "PrimaryCrtInRegInProgress");
        }
    }
}
