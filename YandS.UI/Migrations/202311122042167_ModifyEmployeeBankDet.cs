namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEmployeeBankDet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HR_Employee_s", "BankName", c => c.String(maxLength: 3));
            AddColumn("dbo.HR_Employee_s", "AccountNumber", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HR_Employee_s", "AccountNumber");
            DropColumn("dbo.HR_Employee_s", "BankName");
        }
    }
}
