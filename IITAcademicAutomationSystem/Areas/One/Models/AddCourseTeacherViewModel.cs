using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class AddCourseTeacherViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public List<CheckBoxListItem> Teachers { get; set; }

        public AddCourseTeacherViewModel()
        {
            Teachers = new List<CheckBoxListItem>();
        }
    }
}