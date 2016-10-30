using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class MarksDistribution
    {
        public int Id { get; set; }
        public double marksEvaluated { get; set; }
        public bool avarageOrBestCount { get; set; }
        public bool isFinallySubmitted { get; set; }
        public bool isVisibleToStudent { get; set; }

        public int programId { get; set; }
        public int semesterId { get; set; }
        public int batchId { get; set; }
        public int courseId { get; set; }
        public int headId { get; set; }
        public string marksDistributorId { get; set; }






    }
}