namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSessionNewColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "SuspendedLastDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SessionsRolls", "SuspendedFollowerId", c => c.String(maxLength: 5));
            AddColumn("dbo.SessionsRolls", "SessionOnHold", c => c.String(maxLength: 2));
            AddColumn("dbo.SessionsRolls", "SessionOnHoldUntill", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "SessionOnHoldUntill");
            DropColumn("dbo.SessionsRolls", "SessionOnHold");
            DropColumn("dbo.SessionsRolls", "SuspendedFollowerId");
            DropColumn("dbo.SessionsRolls", "SuspendedLastDate");
        }
    }
}
