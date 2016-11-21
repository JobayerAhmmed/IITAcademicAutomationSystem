using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IUserService
    {
        ApplicationUser ViewUser(string userId);
        bool CreateUser(ApplicationUser user);
        bool UpdateUser(ApplicationUser user);
        bool DeleteUser(string userId);
        bool UserExist(string email);
        IEnumerable<IdentityRole> GetUserRoles(string userId);
        void Dispose();
    }

    public class UserService : IUserService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;

        public UserService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // View User
        public ApplicationUser ViewUser(string userId)
        {
            return unitOfWork.UserRepository.GetUserById(userId);
        }

        // Create
        public bool CreateUser(ApplicationUser user)
        {
            return true;
        }

        // Update
        public bool UpdateUser(ApplicationUser user)
        {
            if (!ValidateUser(user))
                return false;

            try
            {
                unitOfWork.UserRepository.UpdateUser(user);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        // Delete
        public bool DeleteUser(string userId)
        {
            return true;
        }

        // User exist
        public bool UserExist(string email)
        {
            ApplicationUser user = unitOfWork.UserRepository.GetUserByUsername(email);
            if (user == null)
                return false;
            return true;
        }

        // Get user roles
        public IEnumerable<IdentityRole> GetUserRoles(string userId)
        {
            return unitOfWork.UserRepository.GetUserRoles(userId);
        }

        public bool ValidateUser(ApplicationUser user)
        {
            return modelState.IsValid;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}