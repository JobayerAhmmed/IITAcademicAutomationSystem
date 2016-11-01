using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        //public DbSet<Menu> Menus { get; set; }
        //public DbSet<IdentityUserRole> UserRoles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }
        public DbSet<CourseSemester> CourseSemesters { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<Marks> Marks { get; set; }
        public DbSet<MarksDistribution> MarksDistributions { get; set; }
        public DbSet<MarksHead> MarksHeads { get; set; }
        public DbSet<MarksSubHead> MarksSubHeads { get; set; }
        public DbSet<AcademicFile> AcademicFiles { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ApplicationUser>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationRole>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //modelBuilder.Entity<ApplicationUser>().HasKey<int>(l => l.Id);
            //modelBuilder.Entity<ApplicationUser>().Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<ApplicationUserLogin>().HasKey<int>(l => l.UserId);
            //modelBuilder.Entity<ApplicationRole>().HasKey<int>(r => r.Id);
            //modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
    
}