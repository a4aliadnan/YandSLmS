namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyEmployeeAddLocationand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HR_Employee_s", "LocationCode", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HR_Employee_s", "LocationCode");
        }
    }
}
