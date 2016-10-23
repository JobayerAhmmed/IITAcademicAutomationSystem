using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IUserService
    {
        ApplicationUser ViewUser(string id);
        void Dispose();
    }
    public class UserService : IUserService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        private System.Web.Mvc.ModelStateDictionary modelState1;

        public UserService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // View User
        public ApplicationUser ViewUser(string id)
        {
            return unitOfWork.UserRepository.GetUserById(id);
        }


        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}