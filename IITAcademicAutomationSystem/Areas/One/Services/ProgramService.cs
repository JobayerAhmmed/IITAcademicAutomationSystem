using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IProgramService
    {
        Program ViewProgram(int id);
        IEnumerable<Program> GetPrograms();
    }
    public class ProgramService : IProgramService
    {
        private ModelStateDictionary modelState;
        private UnitOfWork unitOfWork;
        public ProgramService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        public Program ViewProgram(int id)
        {
            return unitOfWork.ProgramRepository.GetProgramById(id);
        }

        public IEnumerable<Program> GetPrograms()
        {
            return unitOfWork.ProgramRepository.GetPrograms();
        }
    }
}