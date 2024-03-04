namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String());
        }
    }
}
