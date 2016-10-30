using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetProgramsResDto
    {
        public ProgramResDto[] programs { get; set; }
    }

    public class ProgramResDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}