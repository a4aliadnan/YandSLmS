namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedArchievebyColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClosurePartialDetails", "ClosureArchieveddBy", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClosurePartialDetails", "ClosureArchieveddBy");
        }
    }
}
