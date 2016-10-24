using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchStudentsViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public IEnumerable<BatchStudent> Students { get; set; }
        
    }

    public class BatchStudent
    {
        public int StudentId { get; set; }
        public string UserId { get; set; }
        public int SemesterIdCurrent { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
    }
}