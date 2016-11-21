using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchIndexViewModel
    {
        public int Id { get; set; }
        public int BatchNo { get; set; }
        public int SemesterNoCurrent { get; set; }
        public string Status { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }
    }
}