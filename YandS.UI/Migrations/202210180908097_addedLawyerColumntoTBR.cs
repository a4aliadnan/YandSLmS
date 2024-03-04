namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLawyerColumntoTBR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseRegistrations", "LawyerId", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CaseRegistrations", "LawyerId");
        }
    }
}
