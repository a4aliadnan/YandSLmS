namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanManagerColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "LoanManager", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "LoanManager");
        }
    }
}
