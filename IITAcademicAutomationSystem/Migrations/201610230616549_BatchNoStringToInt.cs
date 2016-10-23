namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BatchNoStringToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Batch", "BatchNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Batch", "BatchNo", c => c.String());
        }
    }
}
