using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IStudentRepository
    {
        Student GetStudentById(int id);
        IEnumerable<Student> GetAllStudents();
        void CreateStudent(Student student);
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
        public IEnumerable<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }
        public void CreateStudent(Student student)
        {
            context.Students.Add(student);
        }
    }
}