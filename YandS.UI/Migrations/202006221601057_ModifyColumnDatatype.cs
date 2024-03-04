namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyColumnDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmpDocs", "DocTypeId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmpDocs", "DocTypeId", c => c.Int(nullable: false));
        }
    }
}
