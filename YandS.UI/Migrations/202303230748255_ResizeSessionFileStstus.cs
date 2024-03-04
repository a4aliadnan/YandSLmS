namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResizeSessionFileStstus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SessionsRolls", "SessionFileStatus", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SessionsRolls", "SessionFileStatus", c => c.String(maxLength: 2));
        }
    }
}
