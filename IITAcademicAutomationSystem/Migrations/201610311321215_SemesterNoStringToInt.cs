namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SemesterNoStringToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Semester", "SemesterNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Semester", "SemesterNo", c => c.String());
        }
    }
}
