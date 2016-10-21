using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IProgramService
    {
        Program GetProgramById(int id);
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

        public Program GetProgramById(int id)
        {
            return unitOfWork.ProgramRepository.GetProgramById(id);
        }
    }
}