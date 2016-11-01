using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class ResultResDto
    {
       

    }

    public class AllStudentsResultResDto
    {
        public string program { get; set; }
        public string semester { get; set; }
        public string batch { get; set; }
        public string[] courses { get; set; }
        public IndividualResultResDto[] results { get; set; }
    }

    public class IndividualResultResDto
    {
        public int id { get; set; }
        public string studentName { get; set; }
        public string classRoll { get; set; }
        public string examRoll { get; set; }
        public IndividualCourseResultResDto[] result { get; set; }
    }

    public class IndividualCourseResultResDto
    {
        public string courseName { get; set; }
        public double GPA { get; set; }

    }
}