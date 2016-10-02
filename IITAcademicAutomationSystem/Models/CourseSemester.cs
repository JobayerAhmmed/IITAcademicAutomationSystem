using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class CourseSemester
    {
        public int Id { get; set; }
        public int BatchIdCurrent { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
    }
}