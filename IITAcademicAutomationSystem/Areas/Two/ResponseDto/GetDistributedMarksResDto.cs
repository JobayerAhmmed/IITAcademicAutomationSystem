using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class GetDistributedMarksResDto
    {
        public bool isFinallySubmitted { get; set; }
        public DistributedMarkResDto[] distributedMarks { get; set; }
    }

    public class DistributedMarkResDto
    {
        public int id { get; set; }
        public HeadResDto head { get; set; }
        public double weight { get; set; }
        public string isVisibleToStudent { get; set; }
        public string avarageOrBestCount { get; set; }
    }

    public class GetDistributedMarksPartialResDto
    {
        public DistributedMarkPartialResDto[] distributedMarks { get; set; }
    }

    public class DistributedMarkPartialResDto
    {
        public int id { get; set; }
        public HeadResDto head { get; set; }
    }
}