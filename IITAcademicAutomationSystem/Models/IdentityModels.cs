using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IITAcademicAutomationSystem.DAL;

namespace IITAcademicAutomationSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string ProfileLink { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserRole : IdentityUserRole<int>
    {

    }
    public class UserClaim : IdentityUserClaim<int>
    {

    }
    public class UserLogin : IdentityUserLogin<int>
    {

    }

    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<ApplicationUser, Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

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