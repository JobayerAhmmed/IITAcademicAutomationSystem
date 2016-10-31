using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Marks
    {
        public int Id { get; set; }
        public double examMarks { get; set; }
        public double obtainedMarks { get; set; }
        public int marksDistributionId { get; set; }
        public int subheadId { get; set; }
        public int studentId { get; set; }
        public string marksGiverId { get; set; }
    }
}