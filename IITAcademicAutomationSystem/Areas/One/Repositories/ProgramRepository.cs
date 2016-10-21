using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IProgramRepository
    {
        IEnumerable<Program> GetPrograms();
        Program GetProgramById(int programId);
    }

    public class ProgramRepository : IProgramRepository
    {
        private ApplicationDbContext context;
        public ProgramRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Program> GetPrograms()
        {
            return context.Programs.ToList();
        }

        public Program GetProgramById(int programId)
        {
            return context.Programs.Find(programId);
        }
    }
}