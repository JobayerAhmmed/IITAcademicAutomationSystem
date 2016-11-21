using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class AddFailedStudentsNewBatchViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchIdAssigned { get; set; }
        public int SemesterIdAssigned { get; set; }
        public List<CheckBoxListItem> Students { get; set; }
        public IEnumerable<Batch> Batches { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }

        public IEnumerable<SelectListItem> SemesterList
        {
            get
            {
                var allSemesters = Semesters.Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = "Semester " + f.SemesterNo,
                    //Selected = (SemesterIdCurrent == f.Id ) ? true : false
                    Selected = false
                });
                return allSemesters;
            }
        }
        public IEnumerable<SelectListItem> BatchList
        {
            get
            {
                var allBatches = Batches.Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = ProgramName + " " + f.BatchNo,
                    //Selected = (SemesterIdCurrent == f.Id ) ? true : false
                    Selected = false
                });
                return allBatches;
            }
        }
        public AddFailedStudentsNewBatchViewModel()
        {
            Students = new List<CheckBoxListItem>();
        }
    }
}