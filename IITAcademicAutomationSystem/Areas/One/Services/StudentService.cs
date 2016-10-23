using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IStudentService
    {
        Student ViewStudent(int id);
        void Dispose();
    }
    public class StudentService : IStudentService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        public StudentService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // View student
        public Student ViewStudent(int id)
        {
            return unitOfWork.StudentRepository.GetStudentById(id);
        }


        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}