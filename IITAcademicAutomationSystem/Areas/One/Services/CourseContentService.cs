using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface ICourseContentService
    {
        IEnumerable<CourseContent> GetCourseContents(int courseId);
        IEnumerable<CourseContent> GetCourseContents(int courseId, string teacherId);
        bool Create(CourseContent content);
        void Dispose();
    }
    public class CourseContentService : ICourseContentService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        public CourseContentService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<CourseContent> GetCourseContents(int courseId)
        {
            return unitOfWork.CourseContentRepository.GetCourseContents(courseId);
        }
        public IEnumerable<CourseContent> GetCourseContents(int courseId, string teacherId)
        {
            return unitOfWork.CourseContentRepository.GetCourseContents(courseId, teacherId);
        }
        public bool Create(CourseContent content)
        {
            try
            {
                unitOfWork.CourseContentRepository.Create(content);
                unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("", "Unable to save, please try again");
                return false;
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}