namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTime2ColumnDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Legal_Court_Type", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Legal_Court_Type", "UpdatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HR_Employee_s", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.HR_Employee_s", "UpdatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MASTER_S", "CreatedOn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.MASTER_S", "UpdatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MASTER_S", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.MASTER_S", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.HR_Employee_s", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Legal_Court_Type", "UpdatedOn", c => c.DateTime());
            AlterColumn("dbo.Legal_Court_Type", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
