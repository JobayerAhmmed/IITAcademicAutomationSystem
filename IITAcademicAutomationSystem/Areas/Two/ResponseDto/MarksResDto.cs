using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class MarksResDto
    {
    }

    public class GetGivenMarksResto
    {
        public HeadResDto[] heads { get; set; }
        public SubHeadForMarksResDto[] subHeads { get; set; }
        public StudentMarksResDto[] marksOfStudents { get; set; }
    }

    public class SubHeadForMarksResDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string headName { get; set; }
        public double examMarks { get; set; }
        
    }

    public class StudentMarksResDto
    {
        public string studentName { get; set; }
        public string classRoll { get; set; }
        public string examRoll { get; set; }
        public MarksOfAStudentResDto[] marksOfAllSubHeads { get; set; }
    }

    public class MarksOfAStudentResDto
    {
        public string subHead { get; set; }
        public string marks { get; set; }
    }



    public class GetGivenMarksToEditResDto
    {
        public double examMarks { get; set; }
        public GetObtainedMarksToEditResDto[] obtainedMarks { get; set; }
    }

    public class GetObtainedMarksToEditResDto
    {
        public int id { get; set; }
        public string studentClassRoll { get; set; }
        public string studentName { get; set; }
        public double marks { get; set; }
    }


    public class AllHeadsMarksStatusOfACourseGivingInfoResDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public bool ifAlreadyFinallySubmitted { get; set; }
        public IndividualHeadMarksGivingInfoResDto[] headMarksInfo { get; set; }
    }


    public class IndividualHeadMarksGivingInfoResDto
    {
        public int headId { get; set; }
        public string name { get; set; }
        public bool isMarksGiven { get; set; }
    }


    public class AllFinalSubmissionResDto
    {
        public IndividualFinalSubmissionOfACoaurseResDto[] submissionInfos { get; set; }
    }

    public class IndividualFinalSubmissionOfACoaurseResDto
    {
        public int courseId { get; set; }
        public string courseName { get; set; }
        public bool isFinallySubmitted { get; set; }
    }


}