using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetStudentsResponseDto
    {
        public StudentResDto[] students { get; set; }
    }

    public class StudentResDto
    {
        public int id { get; set; }
        public string classRoll { get; set; }
        public string examRoll { get; set; }
        public string name { get; set; }
    }


    public class StudentFullInfoResDto
    {
        public int id { get; set; }
        public string classRoll { get; set; }
        public string examRoll { get; set; }
        public string name { get; set; }

        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }

        public string programName { get; set; }
        public string semesterName { get; set; }
        public string batchName { get; set; }

    }
}