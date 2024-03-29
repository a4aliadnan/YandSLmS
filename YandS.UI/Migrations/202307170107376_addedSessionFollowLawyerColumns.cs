namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSessionFollowLawyerColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "CourtFollow_LawyerId", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "CourtFollow_LawyerId");
        }
    }
}
