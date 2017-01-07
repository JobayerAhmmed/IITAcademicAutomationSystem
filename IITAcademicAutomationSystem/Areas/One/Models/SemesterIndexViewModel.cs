using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class SemesterIndexViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int NumberOfEnrolledStudents { get; set; }
        public int NumberOfOfferedCourses { get; set; }
        public int NumberOfCourseTeachers { get; set; }

        public int SemesterIdCurrent { get; set; }
        public int CurrentSemesterNo { get; set; }
        public string Status { get; set; }
        public string BatchCoordinator { get; set; }

        public IEnumerable<StudentIndexViewModel> Students { get; set; }
        public IEnumerable<CourseIndexViewModel> Courses { get; set; }
        public IEnumerable<TeacherIndexViewModel> Teachers { get; set; }

    }
}