namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourtType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Legal_Court_Type",
                c => new
                    {
                        Legal_Court_TypeId = c.Int(nullable: false, identity: true),
                        Legal_Court_Type_Desc = c.String(),
                        Active_Flag = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Legal_Court_TypeId)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Legal_Court_Type", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.Legal_Court_Type", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.Legal_Court_Type", new[] { "UpdatedBy" });
            DropIndex("dbo.Legal_Court_Type", new[] { "CreatedBy" });
            DropTable("dbo.Legal_Court_Type");
        }
    }
}
