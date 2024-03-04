namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSoftDeletedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "IsDeleted");
        }
    }
}
