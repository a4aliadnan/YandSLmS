namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropMasterSetupTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "CreatedBy" });
            DropIndex("dbo.MASTER_S", new[] { "UpdatedBy" });
            AddColumn("dbo.MASTER_S", "Created_Id", c => c.Int());
            AddColumn("dbo.MASTER_S", "Modified_Id", c => c.Int());
            AddColumn("dbo.MASTER_S", "ApplicationUser_Id", c => c.Int());
            AddColumn("dbo.MASTER_S", "ApplicationUser_Id1", c => c.Int());
            CreateIndex("dbo.MASTER_S", "Created_Id");
            CreateIndex("dbo.MASTER_S", "Modified_Id");
            CreateIndex("dbo.MASTER_S", "ApplicationUser_Id");
            CreateIndex("dbo.MASTER_S", "ApplicationUser_Id1");
            AddForeignKey("dbo.MASTER_S", "ApplicationUser_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "ApplicationUser_Id1", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "Created_Id", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "Modified_Id", "dbo.USERS", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MASTER_S", "Modified_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "Created_Id", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "ApplicationUser_Id1", "dbo.USERS");
            DropForeignKey("dbo.MASTER_S", "ApplicationUser_Id", "dbo.USERS");
            DropIndex("dbo.MASTER_S", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.MASTER_S", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MASTER_S", new[] { "Modified_Id" });
            DropIndex("dbo.MASTER_S", new[] { "Created_Id" });
            DropColumn("dbo.MASTER_S", "ApplicationUser_Id1");
            DropColumn("dbo.MASTER_S", "ApplicationUser_Id");
            DropColumn("dbo.MASTER_S", "Modified_Id");
            DropColumn("dbo.MASTER_S", "Created_Id");
            CreateIndex("dbo.MASTER_S", "UpdatedBy");
            CreateIndex("dbo.MASTER_S", "CreatedBy");
            AddForeignKey("dbo.MASTER_S", "UpdatedBy", "dbo.USERS", "UserId");
            AddForeignKey("dbo.MASTER_S", "CreatedBy", "dbo.USERS", "UserId");
        }
    }
}
