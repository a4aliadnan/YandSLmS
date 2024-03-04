namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormalizeEmpColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String(maxLength: 200));
            AlterColumn("dbo.HR_Employee_s", "ContactNumber", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HR_Employee_s", "ContactNumber", c => c.String());
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
