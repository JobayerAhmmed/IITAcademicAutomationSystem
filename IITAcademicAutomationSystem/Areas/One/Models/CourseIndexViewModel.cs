using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class CourseIndexViewModel
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
    }
}