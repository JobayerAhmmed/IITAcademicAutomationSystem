using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class UpdateCurrentSemesterViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int SemesterId { get; set; }
        public int SemesterNo { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public int SemesterIdCurrent { get; set; }
        public int CurrentSemesterNo { get; set; }
        public int SemesterIdUpdated { get; set; }
        public IEnumerable<Semester> Semesters { get; set; }
        public IEnumerable<SelectListItem> SemesterList
        {
            get
            {
                if (Semesters == null)
                    return DefaultItem;

                var allSemesters = Semesters.Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = (f.SemesterNo == 100) ? "Passed" : "Semester " + f.SemesterNo,
                    //Selected = (SemesterIdCurrent == f.Id ) ? true : false
                    Selected = false
                });

                //allSemesters.Concat(new SelectListItem
                //{
                //    Value = "0",
                //    Text = "Passed",
                //    Selected = false
                //});
                return DefaultItem.Concat(allSemesters);
            }
        }
        private IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = SemesterIdCurrent.ToString(),
                    Text = "--Select Semester--",
                }, count: 1);
            }
        }
    }
}