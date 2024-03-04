namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPVTableAndLinkTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Legal_Court_Type", "CreatedBy", "dbo.USERS");
            DropForeignKey("dbo.Legal_Court_Type", "UpdatedBy", "dbo.USERS");
            DropIndex("dbo.Legal_Court_Type", new[] { "CreatedBy" });
            DropIndex("dbo.Legal_Court_Type", new[] { "UpdatedBy" });
            CreateTable(
                "dbo.PayVoucher",
                c => new
                    {
                        Voucher_No = c.Int(nullable: false, identity: true),
                        Voucher_Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Payment_Type = c.String(nullable: false),
                        Debit_Account = c.Int(nullable: false),
                        Payment_Head = c.String(maxLength: 3),
                        Credit_Account = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        Payment_Mode = c.String(nullable: false),
                        Cheque_Number = c.String(nullable: false),
                        Received_on = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Status = c.String(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Voucher_No)
                .ForeignKey("dbo.USERS", t => t.CreatedBy)
                .ForeignKey("dbo.USERS", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Link_Bank_Account",
                c => new
                    {
                        LinkId = c.Int(nullable: false, identity: true),
                        BankId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LinkId);
            
            DropTable("dbo.Legal_Court_Type");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Legal_Court_Type",
                c => new
                    {
                        Legal_Court_TypeId = c.Int(nullable: false, identity: true),
                        Legal_Court_Type_Desc = c.String(),
                        Active_Flag = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Legal_Court_TypeId);
            
            DropForeignKey("dbo.PayVoucher", "UpdatedBy", "dbo.USERS");
            DropForeignKey("dbo.PayVoucher", "CreatedBy", "dbo.USERS");
            DropIndex("dbo.PayVoucher", new[] { "UpdatedBy" });
            DropIndex("dbo.PayVoucher", new[] { "CreatedBy" });
            DropTable("dbo.Link_Bank_Account");
            DropTable("dbo.PayVoucher");
            CreateIndex("dbo.Legal_Court_Type", "UpdatedBy");
            CreateIndex("dbo.Legal_Court_Type", "CreatedBy");
            AddForeignKey("dbo.Legal_Court_Type", "UpdatedBy", "dbo.USERS", "UserId");
            AddForeignKey("dbo.Legal_Court_Type", "CreatedBy", "dbo.USERS", "UserId");
        }
    }
}
