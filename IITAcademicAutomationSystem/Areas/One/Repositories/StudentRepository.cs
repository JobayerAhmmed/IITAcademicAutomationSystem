using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        Student GetStudentByUserId(string userId);
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Student> GetActiveStudentsOfBatch(int batchId);
        void CreateStudent(Student student);
        void EditStudent(Student student);
    }
    public class StudentRepository : IStudentRepository
    {
        private ApplicationDbContext context;
        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get Student by Id
        public Student GetStudentById(int id)
        {
            return context.Students.Find(id);
        }
        public Student GetStudentByUserId(string userId)
        {
            return context.Students.Where(s => s.UserId == userId).FirstOrDefault();
        }
        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }
        public IEnumerable<Student> GetActiveStudentsOfBatch(int batchId)
        {
            var result = (from user in context.Users
                          join student in context.Students
                          on user.Id equals student.UserId
                          where user.Status == "Active" && student.BatchIdCurrent == batchId
                          select student).ToList();

            return result;
        }
        public void CreateStudent(Student student)
        {
            context.Students.Add(student);
        }

        public void EditStudent(Student student)
        {
            context.Entry(student).State = EntityState.Modified;
        }
    }
}