using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetSubHeadsResDto
    {
        public SubHeadResDto[] subHeads { get; set; }
    }

    public class SubHeadResDto
    {
        public int id { get; set; }
        public string name { get;set;}
        public int headId { get; set; }
    }
}