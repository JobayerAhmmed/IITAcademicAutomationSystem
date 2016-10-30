using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RequestDto
{
    public class AttendanceReqDto
    {
        
    }

    public class GiveAttendanceResDto
    {
        public int programId { get; set;}
        public int semesterId { get; set; }
        public int courseId { get; set; }
        public int batchId { get; set; }
        public int classNo { get; set; }
        public string classDate { get; set; }
        public AttendanceIndividualToSaveResDto[] attendances { get; set; }
    }

    public class AttendanceIndividualToSaveResDto
    {
        public int studentId { get; set; }
        public bool isPresent { get; set; }
    }

    public class EditAttendanceReqDto
    {
        public EditAttendanceIndividualReqDto[] attendances { get; set; } 
    }

    public class EditAttendanceIndividualReqDto
    {
        public int attendanceId { get; set; }
        public bool isPresent { get; set; }
    }



}