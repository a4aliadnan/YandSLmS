namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMasterSetupTable : DbMigration
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
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.USERS", t => t.Created_Id)
                .ForeignKey("dbo.USERS", t => t.Modified_Id)
                .ForeignKey("dbo.USERS", t => t.UserId_Id)
                .Index(t => t.Created_Id)
                .Index(t => t.Modified_Id)
                .Index(t => t.UserId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HR_Employee_s", "UserId_Id", "dbo.USERS");
            DropForeignKey("dbo.HR_Employee_s", "Modified_Id", "dbo.USERS");
            DropForeignKey("dbo.HR_Employee_s", "Created_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.HR_Employee_s", new[] { "UserId_Id" });
            DropIndex("dbo.HR_Employee_s", new[] { "Modified_Id" });
            DropIndex("dbo.HR_Employee_s", new[] { "Created_Id" });
            DropIndex("dbo.MASTER_S", new[] { "UpdatedBy" });
            DropIndex("dbo.MASTER_S", new[] { "CreatedBy" });
            DropTable("dbo.HR_Employee_s");
            DropTable("dbo.MASTER_S");
        }
    }
}
