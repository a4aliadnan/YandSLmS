namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEmployeeDocumentTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmpDocs", "EmployeeId", "dbo.HR_Employee_s");
            DropIndex("dbo.EmpDocs", new[] { "EmployeeId" });
            DropTable("dbo.EmpDocs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmpDocs",
                c => new
                    {
                        DocId = c.Int(nullable: false, identity: true),
                        DocTypeId = c.Int(nullable: false),
                        OriginalFileName = c.String(),
                        FileName = c.String(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.DocId);
            
            CreateIndex("dbo.EmpDocs", "EmployeeId");
            AddForeignKey("dbo.EmpDocs", "EmployeeId", "dbo.HR_Employee_s", "EmployeeId");
        }
    }
}
