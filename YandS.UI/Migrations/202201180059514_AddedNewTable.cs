namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefendentTransfer",
                c => new
                    {
                        DefendentTransferId = c.Int(nullable: false, identity: true),
                        CaseId = c.Int(nullable: false),
                        CaseLevelCode = c.String(maxLength: 2),
                        TransferDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 3),
                    })
                .PrimaryKey(t => t.DefendentTransferId);
            
            AddColumn("dbo.SessionsRolls", "SuspendedWorkRequired", c => c.String(maxLength: 500));
            AddColumn("dbo.SessionsRolls", "SuspendedSessionNotes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "SuspendedSessionNotes");
            DropColumn("dbo.SessionsRolls", "SuspendedWorkRequired");
            DropTable("dbo.DefendentTransfer");
        }
    }
}
