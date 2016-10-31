using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetCoursesResDto
    {
        public CourseResDto[] courses { get; set; }
    }
    public class CourseResDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}