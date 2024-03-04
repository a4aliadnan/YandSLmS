namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnEmp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HR_Employee_s", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HR_Employee_s", "Gender");
        }
    }
}
