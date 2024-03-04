namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourtAndDateOfRegColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "CourtLocationid", c => c.String());
            AddColumn("dbo.CaseRegistrations", "RegistrationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "RegistrationDate");
            DropColumn("dbo.CaseRegistrations", "CourtLocationid");
        }
    }
}
