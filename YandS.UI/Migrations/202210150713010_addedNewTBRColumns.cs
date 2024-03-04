namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedNewTBRColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "OnHoldDone", c => c.String(maxLength: 1));
            AddColumn("dbo.CaseRegistrations", "ConsultantId", c => c.String(maxLength: 5));
            AddColumn("dbo.CaseRegistrations", "StopRegEmailDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "StopRegUserName", c => c.String(maxLength: 10));
            AddColumn("dbo.CaseRegistrations", "StopEnfRequest", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "StopEnfRequest");
            DropColumn("dbo.CaseRegistrations", "StopRegUserName");
            DropColumn("dbo.CaseRegistrations", "StopRegEmailDate");
            DropColumn("dbo.CaseRegistrations", "ConsultantId");
            DropColumn("dbo.CaseRegistrations", "OnHoldDone");
        }
    }
}
