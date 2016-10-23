using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchIndexViewModel
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchNo { get; set; }
    }
}