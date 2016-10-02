using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public double CourseCredit { get; set; }
        public double CreditTheory { get; set; }
        public double CreditLab { get; set; }
        public int DependentCourseId1 { get; set; }
        public int DependentCourseId2 { get; set; }
    }
}