namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwoPropertiesAddedToBatch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batch", "AdmissionSession", c => c.String());
            AddColumn("dbo.Batch", "CurrentSession", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Batch", "CurrentSession");
            DropColumn("dbo.Batch", "AdmissionSession");
        }
    }
}
