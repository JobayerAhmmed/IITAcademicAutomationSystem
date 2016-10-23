using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchCurrentStudentsViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchIdCurrent { get; set; }
        public int BatchNo { get; set; }
        public IEnumerable<BatchCurrentStudent> CurrentStudents { get; set; }
        
    }

    public class BatchCurrentStudent
    {
        public int StudentId { get; set; }
        public string UserId { get; set; }
        public int SemesterIdCurrent { get; set; }
        public string CurrentRoll { get; set; }
        public string FullName { get; set; }
    }
}