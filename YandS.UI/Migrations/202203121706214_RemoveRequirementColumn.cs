namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequirementColumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SessionsRolls", "Requirements");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SessionsRolls", "Requirements", c => c.String(maxLength: 500));
        }
    }
}
