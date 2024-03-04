namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaseInvoiceNewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CaseInvoices", "CourtLocation", c => c.String());
            AddColumn("dbo.CaseInvoices", "IsLastInvoice", c => c.Boolean());
            AddColumn("dbo.CaseInvoices", "ExpectedFees", c => c.String());
            AddColumn("dbo.CaseInvoices", "BeforeCourtStage", c => c.String());
            AddColumn("dbo.CaseInvoices", "EnforcementStage", c => c.String());
            AddColumn("dbo.CaseInvoices", "EnforcementStageNo", c => c.String());
            AddColumn("dbo.CaseInvoices", "CounsultingFeeType", c => c.String());
            AddColumn("dbo.CaseInvoices", "HourlyNoOfHours", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CaseInvoices", "HourlyRate", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.CaseInvoices", "FixedAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.CaseInvoices", "RetainershipMonth", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CaseInvoiceFeeCalculations", "ShowInInvoice", c => c.Boolean(nullable: false));
            AddColumn("dbo.CaseInvoiceFeeCalculations", "ClaimAmount", c => c.Decimal(precision: 18, scale: 3));
            AddColumn("dbo.CaseInvoiceFees", "FeeTypeCascadeDetail", c => c.String());
            AlterColumn("dbo.CaseInvoices", "ShowInInvoice", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CaseInvoices", "ShowInInvoice", c => c.Boolean(nullable: false));
            DropColumn("dbo.CaseInvoiceFees", "FeeTypeCascadeDetail");
            DropColumn("dbo.CaseInvoiceFeeCalculations", "ClaimAmount");
            DropColumn("dbo.CaseInvoiceFeeCalculations", "ShowInInvoice");
            DropColumn("dbo.CaseInvoices", "RetainershipMonth");
            DropColumn("dbo.CaseInvoices", "FixedAmount");
            DropColumn("dbo.CaseInvoices", "HourlyRate");
            DropColumn("dbo.CaseInvoices", "HourlyNoOfHours");
            DropColumn("dbo.CaseInvoices", "CounsultingFeeType");
            DropColumn("dbo.CaseInvoices", "EnforcementStageNo");
            DropColumn("dbo.CaseInvoices", "EnforcementStage");
            DropColumn("dbo.CaseInvoices", "BeforeCourtStage");
            DropColumn("dbo.CaseInvoices", "ExpectedFees");
            DropColumn("dbo.CaseInvoices", "IsLastInvoice");
            DropColumn("dbo.CaseInvoices", "CourtLocation");
        }
    }
}
