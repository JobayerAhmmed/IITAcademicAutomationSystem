using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ResponseDto
{
    public class AttendanceResDto
    {

    }

    public class GetAttendanceForEditing
    {
        public AttendanceInfoForEditing[] attendances { get; set; }
    }

    public class AttendanceInfoForEditing
    {
        public int id { get; set; }
        public string classRoll { get; set; }
        public string name { get; set; }
        public bool isPresent { get; set; }
    }

    //................course wise report ....................

    public class GetCLassesNumbersAndDatesResDto
    {
        public GetClassesNumberAndDateIndividualResDto[] classesNubesAndDates { get; set; }
    }

    public class GetClassesNumberAndDateIndividualResDto
    {
        public int classNo { get; set; }
        public string date { get; set; }
    }


    public class AttendanceHistoryResDto
    {
        public GetClassesNumberAndDateIndividualResDto[] classNoAndDate { get; set; }
        public AllAttendanceHistoryOfAStudentResDto[] attendanceHistoryAll { get; set; }
    }


    public class AllAttendanceHistoryOfAStudentResDto
    {
        public string name { get; set; }
        public string classRoll { get; set; }
        public IndividualAttendanceHistoryOfAStudentResDto[] attendanceHistoryIndividual { get; set; }
     }
    public class IndividualAttendanceHistoryOfAStudentResDto
    {
        public int id { get; set; }
        public bool isPresent { get; set; }
    }

    //..........individual report..............


    public class AllCourseAttendanceHistoryOfAStudentResDto
    {
        public string studentName { get; set; }
        public string classRoll { get; set; }
        public String program { get; set; }
        public string semester { get; set; }
        public string batch { get; set; }
        public AllCourseAttendanceHistoryOnlyOfAStudentResDto[] attendanceOfAllCourses { get; set; }
    }

    public class AllCourseAttendanceHistoryOnlyOfAStudentResDto
    {
        public string courseName { get; set; }
        public AClassAttendanceHistoryOfAStudentResDto[] attendances { get; set; }
    }

    public class AClassAttendanceHistoryOfAStudentResDto
    {
        public int classNo { get; set; }
        public string date { get; set; }
        public bool isPresent { get; set; }
    }
}