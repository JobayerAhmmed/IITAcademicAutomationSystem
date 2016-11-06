using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class SemesterAddStudentViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public List<CheckBoxListItem> Students { get; set; }

        public SemesterAddStudentViewModel()
        {
            Students = new List<CheckBoxListItem>();
        }
    }
}