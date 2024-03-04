namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseSizeREDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCases", "RealEstateDetail", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "RealEstateDetail", c => c.String(maxLength: 500));
        }
    }
}
