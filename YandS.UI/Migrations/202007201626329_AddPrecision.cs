namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrecision : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HR_Employee_s", "BasicSalary", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.HR_Employee_s", "Allowance", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.PayVoucher", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 3));
            AlterColumn("dbo.CaseInvoiceFees", "FeeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CaseInvoiceFees", "FeeAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PayVoucher", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HR_Employee_s", "Allowance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.HR_Employee_s", "BasicSalary", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
