namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropMaster_sTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "CreatedBy" });
            DropIndex("dbo.MASTER_S", new[] { "UpdatedBy" });
            DropTable("dbo.MASTER_S");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.MstId);
            
            CreateIndex("dbo.MASTER_S", "UpdatedBy");
            CreateIndex("dbo.MASTER_S", "CreatedBy");
            AddForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS", "UserId");
        }
    }
}
