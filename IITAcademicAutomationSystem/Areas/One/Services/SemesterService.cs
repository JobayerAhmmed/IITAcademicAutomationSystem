using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface ISemesterService
    {
        Semester ViewSemester(int id);
        Semester GetFirstSemester(int programId);
        void Dispose();
    }
    public class SemesterService : ISemesterService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        public SemesterService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // View Semester
        public Semester ViewSemester(int id)
        {
            return unitOfWork.SemesterRepository.GetSemesterById(id);
        }

        public Semester GetFirstSemester(int programId)
        {
            return unitOfWork.SemesterRepository.GetFirstSemester(programId);
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}