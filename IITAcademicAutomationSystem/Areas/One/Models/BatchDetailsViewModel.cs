using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchDetailsViewModel
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchNo { get; set; }
        public int CurrentStudent { get; set; }
        public int AdmittedStudent { get; set; }
        public int SemesterIdCurrent { get; set; }
        public int CurrentSemesterNo { get; set; }
        public string Status { get; set; }
        public string BatchCoordinator { get; set; }
    }
}