using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public interface IStudentSemesterRepository
    {
        StudentSemester GetStudentSemester(int batchId, int semesterId, int studentId);
        IEnumerable<StudentSemester> GetStudentSemestersForSemester(int batchId, int semesterId);
        void AddStudentSemester(StudentSemester studentSemester);
        void RemoveStudentSemester(StudentSemester studentSemester);
    }
    public class StudentSemesterRepository : IStudentSemesterRepository
    {
        public ApplicationDbContext context;
        public StudentSemesterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public StudentSemester GetStudentSemester(int batchId, int semesterId, int studentId)
        {
            return context.StudentSemesters.Where(s =>
            s.BatchId == batchId &&
            s.SemesterId == semesterId &&
            s.StudentId == studentId).FirstOrDefault();
        }
        public IEnumerable<StudentSemester> GetStudentSemestersForSemester(int batchId, int semesterId)
        {
            return context.StudentSemesters.Where(s => s.BatchId == batchId && s.SemesterId == semesterId).ToList();
        }
        public void AddStudentSemester(StudentSemester studentSemester)
        {
            context.StudentSemesters.Add(studentSemester);
        }
        public void RemoveStudentSemester(StudentSemester studentSemester)
        {
            context.StudentSemesters.Remove(studentSemester);
        }

    }
}