namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAuditandArchieveColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClosurePartialDetails", "StoreDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ClosurePartialDetails", "StoreNotes", c => c.String());
            AddColumn("dbo.ClosurePartialDetails", "AccountAuditDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClosurePartialDetails", "AccountAuditDate");
            DropColumn("dbo.ClosurePartialDetails", "StoreNotes");
            DropColumn("dbo.ClosurePartialDetails", "StoreDate");
        }
    }
}
