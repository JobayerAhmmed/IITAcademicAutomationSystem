using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    interface UtilitySerI
    {

        int getIdOfLoggedInTeacher();
        int getIdOfLoggedInStudent();
        string getIdOfLoggedInProgramOfficer();
        GetProgramsResDto getAllPrograms();
        GetProgramsResDto getProgramsOfATeacher(int teacherId);
        GetSemestersResDto getSemestersOfAProgram(int programId);
        GetSemestersResDto getSemestersOfATeacherOfAProgram(int teacherId, int programId);
        GetCoursesResDto getCoursesOfATeacherOfASemesterOfAProgram(int teacherId, int programId, int semesterId);
        GetCoursesResDto getCoursesOfAStudent();
        GetCoursesResDto getCoursesOfAStudent(int studentId);

        BatchResDto getBatch(int programId,int semesterId);
        ProgramSemesterBatchResDto getProgramSemesterBatchOfLoggedInStudent();
        GetStudentsResponseDto getStudentsOfASemester(int programId, int semesterId, int batchId);
        GetStudentsResponseDto getStudentsOfACOurse(int programId, int semesterId, int batchId, int courseId);
        StudentFullInfoResDto getStudentByStudentId(int studentId);
        CourseResDto getCourse(int courseId);
        List<CourseResDto> getAllCoursesOfASemester(int programId, int semesterId, int batchId);

        BatchResDto[] getBatchesOfAProgram(int programId);
    }
}
