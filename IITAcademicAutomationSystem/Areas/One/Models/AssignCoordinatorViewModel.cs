using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class AssignCoordinatorViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public string CoordinatorId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public IEnumerable<ApplicationUser> Teachers { get; set; }
        public IEnumerable<SelectListItem> TeacherList
        {
            get
            {
                if (Teachers == null)
                    return DefaultItem;

                var allTeachers = Teachers.Select(f => new SelectListItem
                {
                    Value = f.Id,
                    Text = f.FullName,
                    //Selected = (SemesterIdCurrent == f.Id ) ? true : false
                    Selected = false
                });

                return DefaultItem.Concat(allTeachers);
            }
        }
        private IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "--Select Coordinator--",
                }, count: 1);
            }
        }
    }
}