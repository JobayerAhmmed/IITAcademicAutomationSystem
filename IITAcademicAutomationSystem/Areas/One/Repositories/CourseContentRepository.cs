using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface ICourseContentRepository
    {
        IEnumerable<CourseContent> GetCourseContents(int courseId);
        IEnumerable<CourseContent> GetCourseContents(int courseId, string teacherId);
        void Create(CourseContent content);

    }
    public class CourseContentRepository : ICourseContentRepository
    {
        private ApplicationDbContext context;
        public CourseContentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CourseContent> GetCourseContents(int courseId)
        {
            return context.CourseContents.Where(c => c.CourseId == courseId).ToList();
        }
        public IEnumerable<CourseContent> GetCourseContents(int courseId, string teacherId)
        {
            return context.CourseContents.Where(c => 
                c.CourseId == courseId && c.TeacherId == teacherId).ToList();
        }
        public void Create(CourseContent content)
        {
            context.CourseContents.Add(content);
        }

    }
}