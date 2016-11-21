namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BatchCoordinator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BatchCoordinator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                        TeacherId = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BatchCoordinator");
        }
    }
}
