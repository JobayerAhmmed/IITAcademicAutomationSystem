using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RequestDto
{
    public class GiveMarksReqDto
    {
        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public int courseId { get; set; }
        public int subHeadId { get; set; }
        public int marksDistributionId { get; set; }
        public double examMarks { get; set; }

        public MarksReqDto[] marks { get; set; }
    }

    public class MarksReqDto
    {
        public int studentId { get; set; }
        public double obtainedMarks { get; set; }
    }

    public class SaveEditedMarksResDto
    {
        public double examMarks { get; set; }
        public int marksDistributionId { get; set; }
        public int subheadId { get; set; }
        public MarksToEditResDto[] marksToEdit { get; set; }
    }

    public class MarksToEditResDto
    {
        public int marksId { get; set; }
        public int studentId { get; set; }
        public double obtainedMarks { get; set; }
        public string studentClassRoll { get; set; }
        public string studentName { get; set; }
    }


}