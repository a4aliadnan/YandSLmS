namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyColumnDatatypeRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmpDocs", "DocTypeId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmpDocs", "DocTypeId", c => c.String());
        }
    }
}
