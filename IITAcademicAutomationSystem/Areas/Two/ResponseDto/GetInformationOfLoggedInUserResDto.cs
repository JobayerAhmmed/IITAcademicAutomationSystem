using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetInformationOfLoggedInUserResDto
    {

    }
    public class ProgramSemesterBatchResDto
    {
        public ProgramResDto program { get; set; }
        public SemesterResDto semester { get; set; }
        public BatchResDto batch { get; set; }
    }
}