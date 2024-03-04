namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SessionRollUpdatePrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SessionsRolls", "SupremeFinalJDAmount", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SessionsRolls", "SupremeFinalJDAmount", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
