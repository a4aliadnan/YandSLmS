namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropMasterSetupTablefull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MASTER_S", "Created_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "Modified_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "Parent_MstId", "dbo.MASTER_S");
            DropForeignKey("dbo.MASTER_S", "ApplicationUser_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "ApplicationUser_Id1", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "Created_Id" });
            DropIndex("dbo.MASTER_S", new[] { "Modified_Id" });
            DropIndex("dbo.MASTER_S", new[] { "Parent_MstId" });
            DropIndex("dbo.MASTER_S", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MASTER_S", new[] { "ApplicationUser_Id1" });
            DropTable("dbo.MASTER_S");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MASTER_S",
                c => new
                    {
                        MstId = c.Int(nullable: false, identity: true),
                        Mst_Desc = c.String(nullable: false),
                        Mst_Value = c.String(nullable: false),
                        DisplaySeq = c.Int(),
                        Active_Flag = c.Boolean(nullable: false),
                        Remarks = c.String(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                        Created_Id = c.Int(),
                        Modified_Id = c.Int(),
                        Parent_MstId = c.Int(),
                        ApplicationUser_Id = c.Int(),
                        ApplicationUser_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.MstId);
            
            CreateIndex("dbo.MASTER_S", "ApplicationUser_Id1");
            CreateIndex("dbo.MASTER_S", "ApplicationUser_Id");
            CreateIndex("dbo.MASTER_S", "Parent_MstId");
            CreateIndex("dbo.MASTER_S", "Modified_Id");
            CreateIndex("dbo.MASTER_S", "Created_Id");
            AddForeignKey("dbo.MASTER_S", "ApplicationUser_Id1", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "ApplicationUser_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "Parent_MstId", "dbo.MASTER_S", "MstId");
            AddForeignKey("dbo.MASTER_S", "Modified_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "Created_Id", "dbo.USERS", "UserId");
        }
    }
}
