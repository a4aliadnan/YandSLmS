namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDiputeRegCoulmn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "DisputrRegisterDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "DisputrRegisterDate");
        }
    }
}
