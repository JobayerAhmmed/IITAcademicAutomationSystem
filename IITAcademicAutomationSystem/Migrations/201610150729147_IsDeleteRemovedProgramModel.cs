namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDeleteRemovedProgramModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Course", "IsDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Program", "IsDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Program", "IsDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Course", "IsDelete");
        }
    }
}
