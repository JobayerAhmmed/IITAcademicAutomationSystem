using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class CourseTeacherViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public IEnumerable<TeacherIndexViewModel> Teachers { get; set; }
    }
}