using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class SemesterCourseAllocationViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public List<CheckBoxListItem> Courses { get; set; }

        public SemesterCourseAllocationViewModel()
        {
            Courses = new List<CheckBoxListItem>();
        }
    }
}