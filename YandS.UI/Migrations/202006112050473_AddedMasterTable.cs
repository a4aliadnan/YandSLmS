namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMasterTable : DbMigration
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
                        Parent_MstId = c.Int(),
                    })
                .PrimaryKey(t => t.MstId)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .ForeignKey("dbo.MASTER_S", t => t.Parent_MstId)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.Parent_MstId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MASTER_S", "Parent_MstId", "dbo.MASTER_S");
            DropForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "Parent_MstId" });
            DropIndex("dbo.MASTER_S", new[] { "UpdatedBy" });
            DropIndex("dbo.MASTER_S", new[] { "CreatedBy" });
            DropTable("dbo.MASTER_S");
        }
    }
}
