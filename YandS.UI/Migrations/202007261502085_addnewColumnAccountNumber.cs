namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewColumnAccountNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Link_Bank_Account", "AccountNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Link_Bank_Account", "AccountNumber");
        }
    }
}
