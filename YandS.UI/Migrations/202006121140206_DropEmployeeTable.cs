namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropEmployeeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HR_Employee_s", "Created_Id", "dbo.USERS");
            DropForeignKey("dbo.HR_Employee_s", "Modified_Id", "dbo.USERS");
            DropForeignKey("dbo.HR_Employee_s", "UserId_Id", "dbo.USERS");
            DropIndex("dbo.HR_Employee_s", new[] { "Created_Id" });
            DropIndex("dbo.HR_Employee_s", new[] { "Modified_Id" });
            DropIndex("dbo.HR_Employee_s", new[] { "UserId_Id" });
            DropTable("dbo.HR_Employee_s");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HR_Employee_s",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(),
                        FullName = c.String(),
                        DOB = c.DateTime(nullable: false),
                        DOA = c.DateTime(nullable: false),
                        DOR = c.DateTime(nullable: false),
                        Gender = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                        Created_Id = c.Int(),
                        Modified_Id = c.Int(),
                        UserId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateIndex("dbo.HR_Employee_s", "UserId_Id");
            CreateIndex("dbo.HR_Employee_s", "Modified_Id");
            CreateIndex("dbo.HR_Employee_s", "Created_Id");
            AddForeignKey("dbo.HR_Employee_s", "UserId_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.HR_Employee_s", "Modified_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.HR_Employee_s", "Created_Id", "dbo.USERS", "UserId");
        }
    }
}
