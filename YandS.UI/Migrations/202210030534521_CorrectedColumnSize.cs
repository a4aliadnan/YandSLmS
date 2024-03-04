namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedColumnSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CLIENT_ACCESS", "UserName", c => c.String(maxLength: 256));
            AlterColumn("dbo.CLIENT_ACCESS", "DisplayName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CLIENT_ACCESS", "DisplayName", c => c.String(maxLength: 200));
            AlterColumn("dbo.CLIENT_ACCESS", "UserName", c => c.String(maxLength: 5));
        }
    }
}
