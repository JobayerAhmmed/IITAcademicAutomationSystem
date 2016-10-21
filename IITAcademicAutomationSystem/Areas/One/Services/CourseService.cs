using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface ICourseService
    {
        bool CourseExist(string courseCode, int programId);
        bool CourseExistExcept(string courseCode, int id, int programId);
        Course ViewCourse(int id);
        IEnumerable<Course> ViewCoursesOfProgram(int programId);
        int CreateCourse(Course course);
        bool EditCourse(Course course);
        bool DeleteCourse(int id);
        Program GetProgramById(int programId);
        void Dispose();
    }
    public class CourseService : ICourseService
    {
        private ModelStateDictionary modelState;
        private UnitOfWork unitOfWork;
        public CourseService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // Validate course
        protected bool ValidateCourse(Course course)
        {
            if (course.CreditTheory > course.CourseCredit)
                modelState.AddModelError("CreditTheory", "Credit theory is greater than total credit.");

            if (course.CreditLab > course.CourseCredit)
                modelState.AddModelError("CreditLab", "Credit lab is greater than total credit.");

            if ((course.CreditTheory + course.CreditLab) != course.CourseCredit)
                modelState.AddModelError("CreditLab", "Lab + theory is not equal to total credit.");

            if (course.DependentCourseId1 == 0 && course.DependentCourseId2 != 0)
                modelState.AddModelError("DependentCourseId1", "Select dependent course 1 first.");

            if ((course.DependentCourseId1 != 0 && course.DependentCourseId2 != 0) 
                && (course.DependentCourseId1 == course.DependentCourseId2))
                modelState.AddModelError("DependentCourseId2", "Dependent courses are same, select different courses.");

            return modelState.IsValid;
        }

        // Check existing course
        public bool CourseExist(string courseCode, int programId)
        {
            Course course = unitOfWork.CourseRepository.GetCourseByCourseCode(programId, courseCode);
            if (course == null)
                return false;
            return true;
        }

        // Check existing course except current one
        public bool CourseExistExcept(string courseCode, int id, int programId)
        {
            Course course = unitOfWork.CourseRepository.GetCourseByCourseCode(programId, courseCode);
            if (course == null)
                return false;
            if (course.Id == id)
                return false;
            return true;
        }

        // View course
        public Course ViewCourse(int id)
        {
            return unitOfWork.CourseRepository.GetCourseById(id);
        }

        // View courses of program
        public IEnumerable<Course> ViewCoursesOfProgram(int programId)
        {
            return unitOfWork.CourseRepository.GetCoursesOfProgram(programId);
        }

        // View deleted courses
        //public IEnumerable<CourseIndexViewModel> ViewDeletedCoursesOfProgram(int? programId)
        //{
        //    return null;
        //}

        // Create view of CreateCourse
        //public CourseViewModel CreateCourseView(int? programId)
        //{
        //    Program program = unitOfWork.ProgramRepository.GetProgramById(programId);
        //    if (program == null)
        //    {
        //        return null;
        //    }
        //    IEnumerable<Course> courses = unitOfWork.CourseRepository.GetCoursesOfProgram(programId);
        //    CourseViewModel courseToCreate = new CourseViewModel();
        //    courseToCreate.Courses = courses;
        //    return courseToCreate;
        //}
        public int CreateCourse(Course course)
        {
            if (!ValidateCourse(course))
                return -1;

            try
            {
                unitOfWork.CourseRepository.CreateCourse(course);
                unitOfWork.Save();
                return course.Id;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return -1;
            }
        }

        // Edit course
        public bool EditCourse(Course course)
        {
            if (!ValidateCourse(course))
                return false;

            try
            {
                unitOfWork.CourseRepository.EditCourse(course);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        // Delete course
        public bool DeleteCourse(int id)
        {
            try
            {
                unitOfWork.CourseRepository.DeleteCourse(id);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to delete the course, please try again.");
                return false;
            }
        }

        // Get program
        public Program GetProgramById(int programId)
        {
            return unitOfWork.ProgramRepository.GetProgramById(programId);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}