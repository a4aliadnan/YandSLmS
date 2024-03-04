namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationErrorLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationErrors",
                c => new
                    {
                        ErrorId = c.Int(nullable: false, identity: true),
                        ErrorMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        EmployeeNumber = c.String(),
                        Url = c.String(),
                        IPAddress = c.String(),
                        PCName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationErrors");
        }
    }
}
