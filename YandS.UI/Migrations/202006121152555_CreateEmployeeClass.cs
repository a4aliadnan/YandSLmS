namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEmployeeClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HR_Employee_s",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeNumber = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        DOB = c.DateTime(nullable: false),
                        DOA = c.DateTime(nullable: false),
                        DOR = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                        Created_Id = c.Int(),
                        Modified_Id = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.USERS", t => t.Created_Id)
                .ForeignKey("dbo.USERS", t => t.Modified_Id)
                .Index(t => t.Created_Id)
                .Index(t => t.Modified_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HR_Employee_s", "Modified_Id", "dbo.USERS");
            DropForeignKey("dbo.HR_Employee_s", "Created_Id", "dbo.USERS");
            DropIndex("dbo.HR_Employee_s", new[] { "Modified_Id" });
            DropIndex("dbo.HR_Employee_s", new[] { "Created_Id" });
            DropTable("dbo.HR_Employee_s");
        }
    }
}
