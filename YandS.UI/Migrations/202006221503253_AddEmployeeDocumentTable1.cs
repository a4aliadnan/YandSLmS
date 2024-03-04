namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeDocumentTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpDocs",
                c => new
                    {
                        DocId = c.Int(nullable: false, identity: true),
                        DocTypeId = c.Int(nullable: false),
                        OriginalFileName = c.String(),
                        FileName = c.String(),
                        EmployeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocId)
                .ForeignKey("dbo.HR_Employee_s", t => t.EmployeId, cascadeDelete: true)
                .Index(t => t.EmployeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpDocs", "EmployeId", "dbo.HR_Employee_s");
            DropIndex("dbo.EmpDocs", new[] { "EmployeId" });
            DropTable("dbo.EmpDocs");
        }
    }
}
