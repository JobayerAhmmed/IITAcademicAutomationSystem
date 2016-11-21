using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class BatchCoordinator
    {
        public int Id { get; set; }
        public int BatchId { get; set; }
        public int SemesterId { get; set; }
        public string TeacherId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}