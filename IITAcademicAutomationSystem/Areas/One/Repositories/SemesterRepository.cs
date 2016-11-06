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
        Semester GetSemesterBySemesterNo(int programId, int semesterNo);
        Semester GetFirstSemester(int programId);
        IEnumerable<Semester> GetSemestersOfProgram(int programId);
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

        public Semester GetSemesterBySemesterNo(int programId, int semesterNo)
        {
            return context.Semesters.Where(s => s.ProgramId == programId && s.SemesterNo == semesterNo).FirstOrDefault();
        }
        public Semester GetFirstSemester(int programId)
        {
            return context.Semesters.Where(s => s.ProgramId == programId && s.SemesterNo == 1).FirstOrDefault();
        }

        public IEnumerable<Semester> GetSemestersOfProgram(int programId)
        {
            return context.Semesters.Where(s => s.ProgramId == programId).ToList();
        }
    }
}