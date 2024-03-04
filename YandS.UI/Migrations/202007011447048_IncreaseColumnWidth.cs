namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseColumnWidth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HR_Employee_s", "LocationCode", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HR_Employee_s", "LocationCode", c => c.String(maxLength: 3));
        }
    }
}
