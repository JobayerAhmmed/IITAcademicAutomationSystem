using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RequestDto
{
    public class DistributeMarksFinalReqDto
    {
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public int courseId { get; set; }
        public DistributeMarks[] distribution { get; set; }
    }

    public class EditedDistributeMarksReqDto
    {
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public int courseId { get; set; }
        public DistributeMarks[] distributions { get; set; }
    }

    public class DistributeMarks
    {
        public int id { get; set; }
        public double weight { get; set; }
        public string avarageOrBestCount { get; set; }
        public string isVisibleToStudent { get; set; }
        public int headId { get; set; }
    }
}