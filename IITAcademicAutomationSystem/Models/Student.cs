using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProgramId { get; set; }
        public int BatchIdOriginal { get; set; }
        public int BatchIdCurrent { get; set; }
        public int SemesterId { get; set; }
        public string OriginalRoll { get; set; }
        public string CurrentRoll { get; set; }
        public string RegistrationNo { get; set; }
        public string AdmissionSession { get; set; }
        public string CurrentSession { get; set; }
        public string GuardianPhone { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }
}