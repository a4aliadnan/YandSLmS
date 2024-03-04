namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "DepartmentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "DepartmentType");
        }
    }
}
