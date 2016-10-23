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
    }
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get Student by Id
        public ApplicationUser GetUserById(string id)
        {
            return context.Users.Find(id);
        }
    }
}