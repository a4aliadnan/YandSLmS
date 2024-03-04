namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourtApprovalColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCasesEnforcements", "CourtApproval", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCasesEnforcements", "CourtApproval");
        }
    }
}
