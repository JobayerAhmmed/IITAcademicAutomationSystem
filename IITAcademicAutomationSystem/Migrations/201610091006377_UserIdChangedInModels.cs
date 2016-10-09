namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdChangedInModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseContent", "TeacherId", c => c.String());
            AlterColumn("dbo.CourseSemester", "TeacherId", c => c.String());
            AlterColumn("dbo.StudentCourse", "StudentId", c => c.String());
            AlterColumn("dbo.Student", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentCourse", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseSemester", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseContent", "TeacherId", c => c.Int(nullable: false));
        }
    }
}
