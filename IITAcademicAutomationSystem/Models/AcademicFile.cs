using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class AcademicFile
    {
        public int Id { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public string date { get; set; }
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public string uploaderId { get; set; }
        public bool isDeleted { get; set; }
        public bool isForAll { get; set; }
    }
}