namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradePointAddedToStudentCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourse", "GradePoint", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourse", "GradePoint");
        }
    }
}
