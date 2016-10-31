using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RequestDto
{
    public class AcademicCalendarReqDto
    {
        public class UploadAcademicCalendarReqDto
        {
            public string title { get; set; }
            public string description { get; set; }
            public string path { get; set; }

            public int programId { get; set; }
            public int semesterId { get; set; }
            public int batchId { get; set; }
        }


        public class EditAcademicCalendarReqDto
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
        }
    }
}