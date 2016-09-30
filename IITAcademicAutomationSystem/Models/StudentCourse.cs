using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public int BatchIdCurrent { get; set; }
        public int SemesterId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}