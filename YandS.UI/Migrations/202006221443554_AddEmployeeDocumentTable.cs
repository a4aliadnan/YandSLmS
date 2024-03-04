namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeDocumentTable : DbMigration
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
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.DocId)
                .ForeignKey("dbo.HR_Employee_s", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmpDocs", "EmployeeId", "dbo.HR_Employee_s");
            DropIndex("dbo.EmpDocs", new[] { "EmployeeId" });
            DropTable("dbo.EmpDocs");
        }
    }
}
