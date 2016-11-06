using IITAcademicAutomationSystem.Areas.One;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IITAcademicAutomationSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new AppInitializer());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
            CreateRolesAndUsers();
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Admin role and user
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "iit.du.ac.bd@gmail.com";
                user.Email = "iit.du.ac.bd@gmail.com";
                user.FullName = "Institute of Information Technology";
                user.Designation = "University of Dhaka";
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;

                string userPWD = "iit123";

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // Student role
            if (!roleManager.RoleExists("Student"))
            {
                var role = new IdentityRole();
                role.Name = "Student";
                roleManager.Create(role);
            }

            // Teacher role
            if (!roleManager.RoleExists("Teacher"))
            {
                var role = new IdentityRole();
                role.Name = "Teacher";
                roleManager.Create(role);
            }

            // Program Officer Regular role
            if (!roleManager.RoleExists("Program Officer Regular"))
            {
                var role = new IdentityRole();
                role.Name = "Program Officer Regular";
                roleManager.Create(role);
            }

            // Program Officer Evening role
            if (!roleManager.RoleExists("Program Officer Evening"))
            {
                var role = new IdentityRole();
                role.Name = "Program Officer Evening";
                roleManager.Create(role);
            }

            // Batch coordinator role
            if (!roleManager.RoleExists("Batch Coordinator"))
            {
                var role = new IdentityRole();
                role.Name = "Batch Coordinator";
                roleManager.Create(role);
            }
        }
    }
}
