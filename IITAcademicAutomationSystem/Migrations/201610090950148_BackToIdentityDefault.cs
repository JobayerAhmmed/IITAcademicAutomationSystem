namespace IITAcademicAutomationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToIdentityDefault : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropPrimaryKey("dbo.Role");
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserLogin");
            AlterColumn("dbo.Role", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserRole", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.User", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserClaim", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserLogin", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Role", "Id");
            AddPrimaryKey("dbo.UserRole", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.UserLogin", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.UserRole", "UserId");
            CreateIndex("dbo.UserRole", "RoleId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserLogin", "UserId");
            AddForeignKey("dbo.UserRole", "RoleId", "dbo.Role", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogin", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "UserId", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropPrimaryKey("dbo.UserLogin");
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.UserRole");
            DropPrimaryKey("dbo.Role");
            AlterColumn("dbo.UserLogin", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserClaim", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.User", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.UserRole", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserRole", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Role", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserLogin", new[] { "LoginProvider", "ProviderKey", "UserId" });
            AddPrimaryKey("dbo.User", "Id");
            AddPrimaryKey("dbo.UserRole", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.Role", "Id");
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.UserRole", "RoleId");
            CreateIndex("dbo.UserRole", "UserId");
            AddForeignKey("dbo.UserRole", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogin", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "RoleId", "dbo.Role", "Id", cascadeDelete: true);
        }
    }
}
