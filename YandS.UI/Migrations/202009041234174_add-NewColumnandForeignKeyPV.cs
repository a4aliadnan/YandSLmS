namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewColumnandForeignKeyPV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PayVoucher", "CaseId", c => c.Int());
            AddColumn("dbo.PayVoucher", "CaseInvoices", c => c.String());
            CreateIndex("dbo.PayVoucher", "CaseId");
            AddForeignKey("dbo.PayVoucher", "CaseId", "dbo.CourtCases", "CaseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PayVoucher", "CaseId", "dbo.CourtCases");
            DropIndex("dbo.PayVoucher", new[] { "CaseId" });
            DropColumn("dbo.PayVoucher", "CaseInvoices");
            DropColumn("dbo.PayVoucher", "CaseId");
        }
    }
}
