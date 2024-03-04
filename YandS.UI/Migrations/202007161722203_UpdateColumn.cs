namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourtCasesDetails", "CourtRefNo", c => c.String(nullable: false));
            AlterColumn("dbo.CourtCasesDetails", "RegistrationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCasesDetails", "RegistrationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CourtCasesDetails", "CourtRefNo", c => c.String());
        }
    }
}
