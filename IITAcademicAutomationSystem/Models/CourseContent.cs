using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class CourseContent
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string TeacherId { get; set; }
        public string ContentTitle { get; set; }
        public string ContentDescription { get; set; }
        public DateTime UploadDate { get; set; }
        public string FilePath { get; set; }
        public bool IsDelete { get; set; }
    }
}