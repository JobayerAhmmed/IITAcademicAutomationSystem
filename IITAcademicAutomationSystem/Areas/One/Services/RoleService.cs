using IITAcademicAutomationSystem.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> ViewRoles();
        IdentityRole GetRoleById(string id);
        IdentityRole GetRoleByName(string name);
        string CreateRole(IdentityRole role);
        bool EditRole(IdentityRole role);
        bool RoleExist(string name);
        bool RoleExistExcept(string name, string id);
    }
    public class RoleService : IRoleService
    {
        private ModelStateDictionary modelState;
        private UnitOfWork unitOfWork;

        public RoleService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<IdentityRole> ViewRoles()
        {
            return unitOfWork.RoleRepository.GetRoles();
        }

        public IdentityRole GetRoleById(string id)
        {
            return unitOfWork.RoleRepository.GetRoleById(id);
        }

        public IdentityRole GetRoleByName(string name)
        {
            return unitOfWork.RoleRepository.GetRoleByName(name);
        }

        public string CreateRole(IdentityRole role)
        {
            if (!ValidateRole(role))
                return null;

            try
            {
                unitOfWork.RoleRepository.CreateRole(role);
                unitOfWork.Save();
                return role.Id;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return null;
            }
        }

        public bool EditRole(IdentityRole role)
        {
            if (!ValidateRole(role))
                return false;

            try
            {
                unitOfWork.RoleRepository.EditRole(role);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public bool DeleteRole(string id)
        {
            try
            {
                unitOfWork.RoleRepository.DeleteRole(id);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to delete, try again.");
                return false;
            }
        }

        public bool RoleExist(string name)
        {
            IdentityRole role = unitOfWork.RoleRepository.GetRoleByName(name);
            if (role == null)
                return false;
            return true;
        }
        public bool RoleExistExcept(string name, string id)
        {
            IdentityRole role = unitOfWork.RoleRepository.GetRoleByName(name);
            if (role == null)
                return false;
            if (role.Id == id)
                return false;
            return true;
        }
        protected bool ValidateRole(IdentityRole role)
        {
            return modelState.IsValid;
        }
    }
}