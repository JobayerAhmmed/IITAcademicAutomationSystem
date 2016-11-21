using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchCoordinatorViewModel
    {
        public int SemesterNo { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}