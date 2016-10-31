using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int class_no { get; set; }
        public string classDate { get; set; }
        public bool is_present { get; set; }
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public int courseId { get; set; }
        public int studentId { get; set; }
        public string teacherId { get; set; }
    }
}