namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnMstParentId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MASTER_S", "MstParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MASTER_S", "MstParentId", c => c.Int());
        }
    }
}
