namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormPrintingColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "FormPrintDefendant", c => c.String());
            AddColumn("dbo.CaseRegistrations", "FormPrintLastDate", c => c.DateTime());
            AddColumn("dbo.CaseRegistrations", "FormPrintWorkRequired", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "FormPrintWorkRequired");
            DropColumn("dbo.CaseRegistrations", "FormPrintLastDate");
            DropColumn("dbo.CaseRegistrations", "FormPrintDefendant");
        }
    }
}
