using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetHeadsResDto
    {
        public HeadResDto[] heads { get; set;}
    }
    public class HeadResDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}