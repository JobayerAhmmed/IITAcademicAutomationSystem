using IITAcademicAutomationSystem.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<IdentityRole> GetRoles();
        IdentityRole GetRoleById(string id);
        IdentityRole GetRoleByName(string name);
        void CreateRole(IdentityRole role);
        void EditRole(IdentityRole role);
        void DeleteRole(string id);
    }
    public class RoleRepository : IRoleRepository
    {
        private ApplicationDbContext context;
        public RoleRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<IdentityRole> GetRoles()
        {
            return context.Roles.ToList();
        }

        public IdentityRole GetRoleById(string id)
        {
            return context.Roles.Find(id);
        }

        public IdentityRole GetRoleByName(string name)
        {
            return context.Roles.Where(r => r.Name == name).FirstOrDefault();
        }

        public void CreateRole(IdentityRole role)
        {
            context.Roles.Add(role);
        }

        public void EditRole(IdentityRole role)
        {
            context.Entry(role).State = EntityState.Modified;
        }

        public void DeleteRole(string id)
        {
            IdentityRole role = context.Roles.Find(id);
            context.Roles.Remove(role);
        }
    }
}