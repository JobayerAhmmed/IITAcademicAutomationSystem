using Foolproof;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class CourseCreateViewModel
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }

        [Required(ErrorMessage = "Course code is required.")]
        [Display(Name = "course code")]
        [StringLength(maximumLength:10, ErrorMessage = "Course code can be 5 to 10 characters.", MinimumLength = 5)]
        [RegularExpression(@"([A-Z0-9 \-]+)", ErrorMessage = "Invalid course code")]
        [Remote("CourseExist", "Course", AdditionalFields ="ProgramId", ErrorMessage = "Course already exists")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Course title is required.")]
        [Display(Name = "course title")]
        [StringLength(maximumLength: 100, ErrorMessage = "Course title can be 5 to 100 characters.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z0-9 ,\/\.\&\'\-]+)", ErrorMessage = "Invalid course title.")]
        public string CourseTitle { get; set; }

        [Required(ErrorMessage = "Course credit is required.")]
        [Display(Name = "course credit")]
        [Range(1, 18, ErrorMessage = "Course credit can be 1 to 18.")]
        public double CourseCredit { get; set; }

        [Display(Name = "credit theory")]
        [Range(0, 18, ErrorMessage = "Credit theory can be 0 to 18.")]
        public double CreditTheory { get; set; }

        [Display(Name = "credit lab")]
        [Range(0, 18, ErrorMessage = "Credit lab can be 0 to 18.")]
        public double CreditLab { get; set; }
        public int DependentCourseId1 { get; set; }
        public int DependentCourseId2 { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<SelectListItem> CourseList
        {
            get
            {
                if (Courses == null)
                    return DefaultItem;

                var allCourses = Courses.Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = f.CourseCode + " - " + f.CourseTitle,
                    Selected = (DependentCourseId1 == f.Id || DependentCourseId2 == f.Id) ? true : false
                });
                return DefaultItem.Concat(allCourses);
            }
        }
        private IEnumerable<SelectListItem> DefaultItem
        {
            get
            {
                return Enumerable.Repeat(new SelectListItem
                {
                    Value = "0",
                    Text = "--Select course--"
                }, count: 1);
            }
        }
    }
}