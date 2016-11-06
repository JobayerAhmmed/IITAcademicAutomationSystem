using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterIdCurrent { get; set; }
        public string BatchStatus { get; set; }
        public string AdmissionSession { get; set; }
        public string CurrentSession { get; set; }
    }
}