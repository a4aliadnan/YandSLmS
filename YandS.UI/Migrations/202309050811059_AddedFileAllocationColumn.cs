namespace YandS.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFileAllocationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClosurePartialDetails", "FileAllocation", c => c.String(maxLength: 3));
            AlterColumn("dbo.CourtCases", "ReasonCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.CourtCases", "FileAllocation", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourtCases", "FileAllocation", c => c.String());
            AlterColumn("dbo.CourtCases", "ReasonCode", c => c.String());
            DropColumn("dbo.ClosurePartialDetails", "FileAllocation");
        }
    }
}
