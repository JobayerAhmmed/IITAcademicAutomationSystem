using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    public interface AttendanceManagementSerI
    {
        int getLastClassNumber(int programId, int semesterId, int batchId, int courseId);
        void saveAttendance(GiveAttendanceResDto giveAttendanceResDto);
        GetCLassesNumbersAndDatesResDto getClassesNumbersAndDates(int programId, int semesterId, int batchId, int courseId);
        GetAttendanceForEditing getAttendances(int programId, int semesterId, int batchId, int courseId, int classNo);
        void saveEditedAttendance(EditAttendanceReqDto editAttendanceReqDto);
        AttendanceHistoryResDto getAttendancesCourseWise(int programId, int semesterId, int batchId, int courseId);
        AllCourseAttendanceHistoryOfAStudentResDto getAttendanceOfAStudentOfAllCourses();
    }
}