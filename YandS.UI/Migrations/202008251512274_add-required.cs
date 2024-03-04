namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "Defendant", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "Defendant", c => c.String());
        }
    }
}
