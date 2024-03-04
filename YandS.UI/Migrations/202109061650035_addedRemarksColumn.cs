namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRemarksColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "Remarks");
        }
    }
}
