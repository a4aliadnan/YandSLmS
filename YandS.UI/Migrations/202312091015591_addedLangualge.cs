namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLangualge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HR_Employee_s", "MessageLang", c => c.String(maxLength: 3, defaultValue: "1"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HR_Employee_s", "MessageLang");
        }
    }
}
