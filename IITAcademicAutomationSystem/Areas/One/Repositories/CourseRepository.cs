using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface ICourseRepository
    {
        Course GetCourseById(int courseId);
        Course GetCourseByCourseCode(int programId, string courseCode);
        IEnumerable<Course> GetCoursesOfProgram(int programId);
        //IEnumerable<Course> GetDeletedCoursesOfProgram(int? programId);
        void CreateCourse(Course course);
        void EditCourse(Course course);
        void DeleteCourse(int courseId);
    }
    public class CourseRepository : ICourseRepository
    {
        private ApplicationDbContext context;
        public CourseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Get course by id
        public Course GetCourseById(int courseId)
        {
            return context.Courses.Find(courseId);
        }

        // Get course by course code
        public Course GetCourseByCourseCode(int programId, string courseCode)
        {
            return context.Courses.Where(c => c.ProgramId == programId && c.CourseCode == courseCode && c.IsDelete == false).FirstOrDefault();
        }

        // Get courses of program
        public IEnumerable<Course> GetCoursesOfProgram(int programId)
        {
            return context.Courses.Where(c => c.ProgramId == programId && c.IsDelete == false).OrderBy(o => o.CourseCode).ToList();
        }

        // Get deleted courses
        //public IEnumerable<Course> GetDeletedCoursesOfProgram(int? programId)
        //{
        //    return context.Courses.Where(c => c.ProgramId == programId && c.IsDelete == true).ToList();
        //}

        // Create course
        public void CreateCourse(Course course)
        {
            context.Courses.Add(course);
        }

        // Edit course
        public void EditCourse(Course course)
        {
            context.Entry(course).State = EntityState.Modified;
        }

        // Delete course
        public void DeleteCourse(int courseId)
        {
            Course course = context.Courses.Find(courseId);
            course.IsDelete = true;
            context.Entry(course).State = EntityState.Modified;
        }
    }
}