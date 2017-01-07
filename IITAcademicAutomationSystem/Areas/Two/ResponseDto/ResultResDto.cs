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
        public int programId { get; set; }
        public string programName { get;set;}
        public int semesterId { get; set; }
        public int semesterName { get; set; }
        public int batchId { get; set; }
        public string batchName { get; set; }
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
        public double GPA { get; set; }
        public double CGPA { get; set; }
    }

    public class IndividualCourseResultResDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public double GPA { get; set; }

    }

    public class ResultOfACourseResDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string courseCode { get; set; }
        public ResultOfACourseOfAStudentResDto[] studentResult { get; set; }
    }

    public class ResultOfACourseOfAStudentResDto
    {
        public int id { get; set; }
        public string studentName { get; set; }
        public string classRoll { get; set; }
        public string examRoll { get; set; }
        public double totalMarks { get; set; }
        public double GPA { get; set; }
        public string grade { get; set; }
        public MarksOfHeadResDto[] allHeadMarks { get; set; }
    }

    public class MarksOfHeadResDto
    {
        public int headId { get; set; }
        public string headName { get; set; }
        public double marks { get; set; }
    }
}