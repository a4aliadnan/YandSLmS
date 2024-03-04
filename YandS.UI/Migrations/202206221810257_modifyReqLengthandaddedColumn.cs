namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyReqLengthandaddedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "JudRecRedStamp", c => c.String(maxLength: 1));
            AlterColumn("dbo.CourtCases", "Requirements", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "Requirements", c => c.String(maxLength: 500));
            DropColumn("dbo.CourtCases", "JudRecRedStamp");
        }
    }
}
