namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Marging_Ishmam_Task : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicFile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                        title = c.String(),
                        description = c.String(),
                        path = c.String(),
                        date = c.String(),
                        programId = c.Int(nullable: false),
                        semesterId = c.Int(nullable: false),
                        batchId = c.Int(nullable: false),
                        uploaderId = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        isForAll = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Attendance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        class_no = c.Int(nullable: false),
                        classDate = c.String(),
                        is_present = c.Boolean(nullable: false),
                        programId = c.Int(nullable: false),
                        semesterId = c.Int(nullable: false),
                        batchId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        teacherId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        examMarks = c.Double(nullable: false),
                        obtainedMarks = c.Double(nullable: false),
                        marksDistributionId = c.Int(nullable: false),
                        subheadId = c.Int(nullable: false),
                        studentId = c.Int(nullable: false),
                        marksGiverId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarksDistribution",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        marksEvaluated = c.Double(nullable: false),
                        avarageOrBestCount = c.Boolean(nullable: false),
                        isFinallySubmitted = c.Boolean(nullable: false),
                        isVisibleToStudent = c.Boolean(nullable: false),
                        programId = c.Int(nullable: false),
                        semesterId = c.Int(nullable: false),
                        batchId = c.Int(nullable: false),
                        courseId = c.Int(nullable: false),
                        headId = c.Int(nullable: false),
                        marksDistributorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarksHead",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MarksSubHead",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        headId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MarksSubHead");
            DropTable("dbo.MarksHead");
            DropTable("dbo.MarksDistribution");
            DropTable("dbo.Marks");
            DropTable("dbo.Attendance");
            DropTable("dbo.AcademicFile");
        }
    }
}
