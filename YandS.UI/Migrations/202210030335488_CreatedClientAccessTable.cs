namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedClientAccessTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CLIENT_ACCESS",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientCode = c.String(maxLength: 5),
                        UserName = c.String(maxLength: 5),
                        DisplayName = c.String(maxLength: 200),
                        PassWord = c.String(maxLength: 15),
                        Inactive = c.Boolean(nullable: false),
                        LastModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CLIENT_ACCESS");
        }
    }
}
