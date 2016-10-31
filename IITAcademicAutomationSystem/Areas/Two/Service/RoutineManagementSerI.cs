using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    interface RoutineManagementSerI
    {
        void uploadRoutine(UploadRoutineReqDto uploadRoutineReqDto);
        RoutineAllResDto getRoutines_teacherProgramOfficer(int programId, int semesterId);
        RoutineAllResDto getRoutines_student();
        void editRoutine(EditRoutineReqDto editRoutineReqDto);
        void deleteRoutine(int routineId);
        bool checkIfRoutineUploaded(int programId, int semesterId);
    }
}
