namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRefColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "SessionRollId", c => c.Int());
            AddColumn("dbo.SessionsRolls", "CaseRegistrationId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "CaseRegistrationId");
            DropColumn("dbo.CaseRegistrations", "SessionRollId");
        }
    }
}
