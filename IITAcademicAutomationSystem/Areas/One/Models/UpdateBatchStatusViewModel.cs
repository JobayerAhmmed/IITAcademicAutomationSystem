using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class UpdateBatchStatusViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int CurrentSemesterNo { get; set; }
        public string BatchStatus { get; set; }
        public string BatchStatusNew { get; set; }
        public IEnumerable<string> StatusValues { get; set; }
        public IEnumerable<SelectListItem> StatusList
        {
            get
            {
                var allValues = StatusValues.Select(f => new SelectListItem
                {
                    Value = f,
                    Text = f,
                    //Selected = (SemesterIdCurrent == f.Id ) ? true : false
                    Selected = false
                });
                return allValues;
            }
        }
    }
}