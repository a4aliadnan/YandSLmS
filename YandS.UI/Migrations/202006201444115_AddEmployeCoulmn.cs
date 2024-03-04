namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeCoulmn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HR_Employee_s", "Nationality", c => c.String(nullable: false, maxLength: 3));
            AddColumn("dbo.HR_Employee_s", "Designation", c => c.String(maxLength: 3));
            AddColumn("dbo.HR_Employee_s", "Department", c => c.String(maxLength: 3));
            AddColumn("dbo.HR_Employee_s", "LeaveAllowed", c => c.Int(nullable: false));
            AddColumn("dbo.HR_Employee_s", "PAddress", c => c.String());
            AddColumn("dbo.HR_Employee_s", "CAddress", c => c.String());
            AddColumn("dbo.HR_Employee_s", "ContactNumber", c => c.String());
            AddColumn("dbo.HR_Employee_s", "BasicSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.HR_Employee_s", "Allowance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HR_Employee_s", "Allowance");
            DropColumn("dbo.HR_Employee_s", "BasicSalary");
            DropColumn("dbo.HR_Employee_s", "ContactNumber");
            DropColumn("dbo.HR_Employee_s", "CAddress");
            DropColumn("dbo.HR_Employee_s", "PAddress");
            DropColumn("dbo.HR_Employee_s", "LeaveAllowed");
            DropColumn("dbo.HR_Employee_s", "Department");
            DropColumn("dbo.HR_Employee_s", "Designation");
            DropColumn("dbo.HR_Employee_s", "Nationality");
        }
    }
}
