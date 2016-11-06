using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class StudentSemester
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int SemesterId { get; set; }
        public int StudentId { get; set; }
        public double GPA { get; set; }
    }
}