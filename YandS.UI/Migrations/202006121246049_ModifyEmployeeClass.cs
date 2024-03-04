namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEmployeeClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HR_Employee_s", "EmployeeNumber", c => c.String(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "DOR", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HR_Employee_s", "DOR", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String());
            AlterColumn("dbo.HR_Employee_s", "EmployeeNumber", c => c.String());
        }
    }
}
