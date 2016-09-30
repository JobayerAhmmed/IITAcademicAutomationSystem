using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Batch
    {
        public int BatchId { get; set; }
        public int ProgramId { get; set; }
        public string BatchNo { get; set; }
        public int SemesterIdCurrent { get; set; }
        public string BatchStatus { get; set; }
    }
}