namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSessionNewRemarks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SessionsRolls", "SessionNote_Remark", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SessionsRolls", "SessionNote_Remark");
        }
    }
}
