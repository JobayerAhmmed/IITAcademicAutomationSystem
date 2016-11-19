namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                "dbo.Batch",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramId = c.Int(nullable: false),
                        BatchNo = c.Int(nullable: false),
                        SemesterIdCurrent = c.Int(nullable: false),
                        BatchStatus = c.String(),
                        AdmissionSession = c.String(),
                        CurrentSession = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseContent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.String(),
                        ContentTitle = c.String(),
                        ContentDescription = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        FilePath = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramId = c.Int(nullable: false),
                        CourseCode = c.String(),
                        CourseTitle = c.String(),
                        CourseCredit = c.Double(nullable: false),
                        CreditTheory = c.Double(nullable: false),
                        CreditLab = c.Double(nullable: false),
                        DependentCourseId1 = c.Int(nullable: false),
                        DependentCourseId2 = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseSemester",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        TeacherId = c.String(),
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
            
            CreateTable(
                "dbo.Program",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Semester",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgramId = c.Int(nullable: false),
                        SemesterNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        GradePoint = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ProgramId = c.Int(nullable: false),
                        BatchIdOriginal = c.Int(nullable: false),
                        BatchIdCurrent = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        OriginalRoll = c.String(),
                        CurrentRoll = c.String(),
                        RegistrationNo = c.String(),
                        AdmissionSession = c.String(),
                        CurrentSession = c.String(),
                        GuardianPhone = c.String(),
                        CurrentAddress = c.String(),
                        PermanentAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Designation = c.String(),
                        ProfileLink = c.String(),
                        ImagePath = c.String(),
                        Status = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.StudentSemester");
            DropTable("dbo.Student");
            DropTable("dbo.StudentCourse");
            DropTable("dbo.Semester");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Program");
            DropTable("dbo.MarksSubHead");
            DropTable("dbo.MarksHead");
            DropTable("dbo.MarksDistribution");
            DropTable("dbo.Marks");
            DropTable("dbo.CourseSemester");
            DropTable("dbo.Course");
            DropTable("dbo.CourseContent");
            DropTable("dbo.Batch");
            DropTable("dbo.Attendance");
            DropTable("dbo.AcademicFile");
        }
    }
}
