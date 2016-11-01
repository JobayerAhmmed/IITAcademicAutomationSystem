using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class StudentPromotionResDto
    {

    }



    public class IndividualStudentPromotionResDto
    {
        public int studentId { get; set; }
        public string name { get; set; }
        public string examRoll { get; set; }
        public string classRoll { get; set; }
        public bool isPassedAllCourses { get; set; }
    }
}