namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Student");
        }
    }
}
