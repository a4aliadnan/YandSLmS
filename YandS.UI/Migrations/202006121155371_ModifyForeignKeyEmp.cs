namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyForeignKeyEmp : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HR_Employee_s", new[] { "Created_Id" });
            DropColumn("dbo.HR_Employee_s", "CreatedBy");
            DropColumn("dbo.HR_Employee_s", "UpdatedBy");
            RenameColumn(table: "dbo.HR_Employee_s", name: "Created_Id", newName: "CreatedBy");
            RenameColumn(table: "dbo.HR_Employee_s", name: "Modified_Id", newName: "UpdatedBy");
            RenameIndex(table: "dbo.HR_Employee_s", name: "IX_Modified_Id", newName: "IX_UpdatedBy");
            AlterColumn("dbo.HR_Employee_s", "CreatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.HR_Employee_s", "CreatedBy");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HR_Employee_s", new[] { "CreatedBy" });
            AlterColumn("dbo.HR_Employee_s", "CreatedBy", c => c.Int());
            RenameIndex(table: "dbo.HR_Employee_s", name: "IX_UpdatedBy", newName: "IX_Modified_Id");
            RenameColumn(table: "dbo.HR_Employee_s", name: "UpdatedBy", newName: "Modified_Id");
            RenameColumn(table: "dbo.HR_Employee_s", name: "CreatedBy", newName: "Created_Id");
            AddColumn("dbo.HR_Employee_s", "UpdatedBy", c => c.Int());
            AddColumn("dbo.HR_Employee_s", "CreatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.HR_Employee_s", "Created_Id");
        }
    }
}
