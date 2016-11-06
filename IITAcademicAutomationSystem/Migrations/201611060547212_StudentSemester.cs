namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentSemester : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSemester",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        GPA = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentSemester");
        }
    }
}
