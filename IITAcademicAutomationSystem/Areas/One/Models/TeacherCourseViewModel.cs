using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class TeacherCourseViewModel
    {
        public string TeacherId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public string ProgramName { get; set; }
    }
}