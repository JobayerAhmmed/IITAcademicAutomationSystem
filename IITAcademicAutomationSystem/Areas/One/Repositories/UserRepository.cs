using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByUsername(string username);
        IEnumerable<IdentityRole> GetUserRoles(string userId);
        void UpdateUser(ApplicationUser user);
    }
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get user by Id
        public ApplicationUser GetUserById(string id)
        {
            return context.Users.Find(id);
        }

        // Get user by username
        public ApplicationUser GetUserByUsername(string username)
        {
            return context.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        // Get user roles
        public IEnumerable<IdentityRole> GetUserRoles(string userId)
        {
            return context.Roles.Where(r => r.Users.Any(u => u.UserId == userId));
        }

        // Update user
        public void UpdateUser(ApplicationUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}