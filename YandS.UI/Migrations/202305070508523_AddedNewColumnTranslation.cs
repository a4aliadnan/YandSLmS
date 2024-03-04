namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnTranslation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourtCases", "Translation", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourtCases", "Translation");
        }
    }
}
