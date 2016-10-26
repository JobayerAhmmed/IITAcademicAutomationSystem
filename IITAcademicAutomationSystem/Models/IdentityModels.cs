using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IITAcademicAutomationSystem.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace IITAcademicAutomationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ProfileLink { get; set; } 
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in 
            // CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    /*
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole()
        {
            
        }
    }
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {

    }
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {

    }

    public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            //this.Id = default(int);
        }
        public ApplicationRole(string name)
            //: this()
        {
            Name = name;
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, int, 
        ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }*/

        //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        //{
        //    public ApplicationDbContext()
        //        : base("DefaultConnection", throwIfV1Schema: false)
        //    {
        //    }

        //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //    {
        //        base.OnModelCreating(modelBuilder);

        //        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        //        modelBuilder.Entity<IdentityRole>().ToTable("Role");
        //        modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
        //        modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        //        modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        //    }

        //    public static ApplicationDbContext Create()
        //    {
        //        return new ApplicationDbContext();
        //    }
        //}
    }