using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class RoleEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter role name.")]
        [StringLength(50, ErrorMessage = "Role name should be at least 5 characters.", MinimumLength = 5)]
        [RegularExpression(@"([A-Z]+[a-zA-Z ]+)", ErrorMessage = "Role name can contain only alphabet and space.")]
        [Display(Name = "Role")]
        [Remote("RoleExistExcept", "Role", AdditionalFields = "Id", ErrorMessage = "Role already exists.")]
        public string Name { get; set; }
    }
}