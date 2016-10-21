using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class CourseDetailsViewModel : CourseIndexViewModel
    {
        public double CourseCredit { get; set; }
        public double CreditTheory { get; set; }
        public double CreditLab { get; set; }
        public string DependentCourse1 { get; set; }
        public string DependentCourse2 { get; set; }
    }
}