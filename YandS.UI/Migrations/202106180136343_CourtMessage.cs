namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourtMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "CourtMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "CourtMessage");
        }
    }
}
