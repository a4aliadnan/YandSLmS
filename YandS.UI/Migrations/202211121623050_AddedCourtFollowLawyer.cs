namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourtFollowLawyer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "CourtFollow_LawyerId", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "CourtFollow_LawyerId");
        }
    }
}
