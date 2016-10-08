namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramModelIsDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Program", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Program", "IsDelete");
        }
    }
}
