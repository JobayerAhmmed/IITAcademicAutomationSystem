using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IITAcademicAutomationSystem.Areas.Two.RequestDto.AcademicCalendarReqDto;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    interface AcademicCalendarManagementSerI
    {
        void uploadAcademicCalendar(UploadAcademicCalendarReqDto uploadAcademicCalendarReqDto, string uploaderId);
        AcademicCalendarAllResDto getAcademicCalendars_teacherProgramOfficer(int programId, int semesterId);
        AcademicCalendarAllResDto getAcademicCalendars_student(int studentId);
        void editAcademicCalendar(EditAcademicCalendarReqDto editAcademicCalendarReqDto);
        void deleteAcademicCalendar(int academicCalendarId);
        bool checkIfAcademicCalendarUploaded(int programId,int semesterId);
    }
}
