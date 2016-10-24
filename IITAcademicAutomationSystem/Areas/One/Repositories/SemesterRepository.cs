using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface ISemesterRepository
    {
        Semester GetSemesterById(int id);
    }
    public class SemesterRepository : ISemesterRepository
    {
        private ApplicationDbContext context;
        public SemesterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get Student by Id
        public Semester GetSemesterById(int id)
        {
            return context.Semesters.Find(id);
        }
    }
}