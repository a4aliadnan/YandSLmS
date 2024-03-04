namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyNullFK1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Legal_Court_Type", new[] { "UpdatedBy" });
            AlterColumn("dbo.Legal_Court_Type", "UpdatedBy", c => c.Int());
            CreateIndex("dbo.Legal_Court_Type", "UpdatedBy");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Legal_Court_Type", new[] { "UpdatedBy" });
            AlterColumn("dbo.Legal_Court_Type", "UpdatedBy", c => c.Int(nullable: false));
            CreateIndex("dbo.Legal_Court_Type", "UpdatedBy");
        }
    }
}
