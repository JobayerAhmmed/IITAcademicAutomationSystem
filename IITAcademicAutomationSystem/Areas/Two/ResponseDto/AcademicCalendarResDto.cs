using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class AcademicCalendarResDto
    {
    }

    public class AcademicCalendarAllResDto
    {
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }

        public AcademicCalendarIndividualResDto academicCalendar { get; set; }
    }

    public class AcademicCalendarIndividualResDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string path { get; set; }
        public string date { get; set; }
    }
}