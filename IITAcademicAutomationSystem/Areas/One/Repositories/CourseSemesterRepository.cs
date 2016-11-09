using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface ICourseSemesterRepository
    {
        CourseSemester GetCourseSemester(int batchId, int semesterId, int courseId);
        IEnumerable<CourseSemester> GetSemesterCourses(int batchId, int semesterId);
        IEnumerable<CourseSemester> GetSemesterTeachers(int batchId, int semesterId);
        IEnumerable<CourseSemester> GetAllocatedCourseSemesterOfBatch(int batchId);
        void AddCourseToSemester(CourseSemester courseSemester);
        void RemoveCourseFromSemester(CourseSemester courseSemester);
    }
    public class CourseSemesterRepository : ICourseSemesterRepository
    {
        private ApplicationDbContext context;
        public CourseSemesterRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public CourseSemester GetCourseSemester(int batchId, int semesterId, int courseId)
        {
            return context.CourseSemesters.Where(c =>
                c.BatchId == batchId &&
                c.SemesterId == semesterId &&
                c.CourseId == courseId).FirstOrDefault();
        }

        public IEnumerable<CourseSemester> GetSemesterCourses(int batchId, int semesterId)
        {
            return context.CourseSemesters.Where(c => 
                c.SemesterId == semesterId && 
                c.BatchId == batchId).GroupBy(c => c.CourseId).Select(c => c.FirstOrDefault()).ToList();
        }
        public IEnumerable<CourseSemester> GetSemesterTeachers(int batchId, int semesterId)
        {
            return context.CourseSemesters.Where(t =>
                t.TeacherId != null &&
                t.BatchId == batchId &&
                t.SemesterId == semesterId).GroupBy(t => t.TeacherId).Select(t => t.FirstOrDefault()).ToList();
        }

        public IEnumerable<CourseSemester> GetAllocatedCourseSemesterOfBatch(int batchId)
        {
            return context.CourseSemesters.Where(c => c.BatchId == batchId).ToList();
        }
        public void AddCourseToSemester(CourseSemester courseSemester)
        {
            context.CourseSemesters.Add(courseSemester);
        }
        public void RemoveCourseFromSemester(CourseSemester courseSemester)
        {
            context.CourseSemesters.Remove(courseSemester);
        }
    }
}