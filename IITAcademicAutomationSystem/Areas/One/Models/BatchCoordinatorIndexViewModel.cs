using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchCoordinatorIndexViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public IEnumerable<BatchCoordinatorViewModel> Coordinators { get; set; }
    }
}