namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSessionFileStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "SessionFileStatus", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "SessionFileStatus");
        }
    }
}
