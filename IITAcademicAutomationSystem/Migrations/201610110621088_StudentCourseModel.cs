namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourse", "BatchId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentCourse", "StudentId", c => c.Int(nullable: false));
            DropColumn("dbo.StudentCourse", "BatchIdCurrent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentCourse", "BatchIdCurrent", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentCourse", "StudentId", c => c.String());
            DropColumn("dbo.StudentCourse", "BatchId");
        }
    }
}
