namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseSemesterBatchIdCurrentToBatchId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseSemester", "BatchId", c => c.Int(nullable: false));
            DropColumn("dbo.CourseSemester", "BatchIdCurrent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseSemester", "BatchIdCurrent", c => c.Int(nullable: false));
            DropColumn("dbo.CourseSemester", "BatchId");
        }
    }
}
