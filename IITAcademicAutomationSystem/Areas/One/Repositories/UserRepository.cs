using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetUserById(string id);
        ApplicationUser GetUserByUsername(string username);
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
    }
}