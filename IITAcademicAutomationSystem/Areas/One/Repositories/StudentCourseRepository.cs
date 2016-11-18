using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IStudentCourseRepository
    {
        IEnumerable<StudentCourse> GetPassedStudentCoursesOfSemester(int batchId, int semesterId);
        IEnumerable<StudentCourse> GetStudentCoursesOfStudent(int batchId, int semesterId, int studentId);
        IEnumerable<StudentCourse> GetCourseStudents(int batchId, int semesterId, int courseId);
        StudentCourse GetStudentCourse(int batchId, int studentId, int courseId);
        void AddStudentCourse(StudentCourse studentCourse);
    }
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private ApplicationDbContext context;
        public StudentCourseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<StudentCourse> GetPassedStudentCoursesOfSemester(int batchId, int semesterId)
        {
            return context.StudentCourses.Where(s =>
                s.SemesterId == semesterId &&
                s.BatchId == batchId &&
                s.GradePoint >= 2.00).ToList();
        }
        public IEnumerable<StudentCourse> GetStudentCoursesOfStudent(int batchId, int semesterId, int studentId)
        {
            return context.StudentCourses.Where(s =>
                s.BatchId == batchId &&
                s.SemesterId == semesterId &&
                s.StudentId == studentId).ToList();
        }
        public IEnumerable<StudentCourse> GetCourseStudents(int batchId, int semesterId, int courseId)
        {
            return context.StudentCourses.Where(s =>
                s.BatchId == batchId &&
                s.SemesterId == semesterId &&
                s.CourseId == courseId).ToList();
        }
        public StudentCourse GetStudentCourse(int batchId, int studentId, int courseId)
        {
            return context.StudentCourses.Where(s =>
                s.BatchId == batchId &&
                s.StudentId == studentId &&
                s.CourseId == courseId).FirstOrDefault();
        }
        public void AddStudentCourse(StudentCourse studentCourse)
        {
            context.StudentCourses.Add(studentCourse);
        }

    }
}