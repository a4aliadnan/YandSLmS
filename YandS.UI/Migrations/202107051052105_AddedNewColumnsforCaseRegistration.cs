namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewColumnsforCaseRegistration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "SendDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "OmanPostNo", c => c.String());
            AddColumn("dbo.CaseRegistrations", "FirstReminderDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "ReminderNo", c => c.String());
            AddColumn("dbo.CaseRegistrations", "CourtRequestDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "OfficeProcedure", c => c.String());
            AddColumn("dbo.CaseRegistrations", "PaymentDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "AssignedTo", c => c.String());
            AddColumn("dbo.CaseRegistrations", "AssignedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.CaseRegistrations", "CourtDetailRegistered", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.CaseRegistrations", "AdminFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "AdminFile");
            DropColumn("dbo.CaseRegistrations", "CourtDetailRegistered");
            DropColumn("dbo.CaseRegistrations", "AssignedDate");
            DropColumn("dbo.CaseRegistrations", "AssignedTo");
            DropColumn("dbo.CaseRegistrations", "PaymentDate");
            DropColumn("dbo.CaseRegistrations", "OfficeProcedure");
            DropColumn("dbo.CaseRegistrations", "CourtRequestDate");
            DropColumn("dbo.CaseRegistrations", "ReminderNo");
            DropColumn("dbo.CaseRegistrations", "FirstReminderDate");
            DropColumn("dbo.CaseRegistrations", "OmanPostNo");
            DropColumn("dbo.CaseRegistrations", "SendDate");
        }
    }
}
