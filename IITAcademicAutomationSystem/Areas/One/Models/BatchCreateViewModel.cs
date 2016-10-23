using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Models
{
    public class BatchCreateViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }

        [Display(Name = "batch no")]
        [Range(1, 999, ErrorMessage = "Batch no. should be 1 to 3 digits.")]
        [Remote("BatchExist", "Batch", AdditionalFields = "ProgramId", ErrorMessage = "Batch no. already exists.")]
        public int BatchNo { get; set; }
    }
}