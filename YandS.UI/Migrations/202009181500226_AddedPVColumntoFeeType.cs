namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPVColumntoFeeType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoiceFees", "PV_No", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseInvoiceFees", "PV_No");
        }
    }
}
