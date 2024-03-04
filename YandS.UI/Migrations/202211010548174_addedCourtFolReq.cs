namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCourtFolReq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "CourtFollowRequirement", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "CourtFollowRequirement");
        }
    }
}
