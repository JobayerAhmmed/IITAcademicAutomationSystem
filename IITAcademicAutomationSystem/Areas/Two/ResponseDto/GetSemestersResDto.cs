using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetSemestersResDto
    {
        public SemesterResDto[] semesters { get; set; }
    }

    public class SemesterResDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}