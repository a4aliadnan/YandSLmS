namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMasterTableWithSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MASTER_S",
                c => new
                    {
                        MstId = c.Int(nullable: false, identity: true),
                        MstParentId = c.Int(),
                        Mst_Desc = c.String(nullable: false),
                        Mst_Value = c.String(nullable: false),
                        DisplaySeq = c.Int(),
                        Active_Flag = c.Boolean(nullable: false),
                        Remarks = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.MstId)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            AlterColumn("dbo.HR_Employee_s", "EmployeeNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "UpdatedBy" });
            DropIndex("dbo.MASTER_S", new[] { "CreatedBy" });
            AlterColumn("dbo.HR_Employee_s", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.HR_Employee_s", "EmployeeNumber", c => c.String(nullable: false));
            DropTable("dbo.MASTER_S");
        }
    }
}
