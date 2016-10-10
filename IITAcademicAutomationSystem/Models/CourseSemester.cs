using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class CourseSemester
    {
        public int Id { get; set; }
        public int BatchId { get; set; }    // why BatchIdCurrent ?
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public string TeacherId { get; set; }
    }
}