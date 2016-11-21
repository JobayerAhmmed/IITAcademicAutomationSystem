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

        //int getIdOfLoggedInTeacher();
        //int getIdOfLoggedInStudent();
        //string getIdOfLoggedInProgramOfficer();
        GetProgramsResDto getAllPrograms();
        GetProgramsResDto getProgramsOfATeacher(string teacherId);
        GetSemestersResDto getSemestersOfAProgram(int programId);
        GetSemestersResDto getSemestersOfATeacherOfAProgram(string teacherId, int programId);
        GetCoursesResDto getCoursesOfATeacherOfASemesterOfAProgram(string teacherId, int programId, int semesterId);
        //GetCoursesResDto getCoursesOfAStudent();
        GetCoursesResDto getCoursesOfAStudent(int studentId);

        BatchResDto getBatch(int programId,int semesterId);
        ProgramSemesterBatchResDto getProgramSemesterBatchOfLoggedInStudent(int studentId);
        GetStudentsResponseDto getStudentsOfASemester(int programId, int semesterId, int batchId);
        GetStudentsResponseDto getStudentsOfACOurse(int programId, int semesterId, int batchId, int courseId);
        StudentFullInfoResDto getStudentByStudentId(int studentId);
        CourseResDto getCourse(int courseId);
        List<CourseResDto> getAllCoursesOfASemester(int programId, int semesterId, int batchId);

        BatchResDto[] getBatchesOfAProgram(int programId);

        void savePassFailInfoOfAStudnet(int semesterId, int batchId, int studentId,double GPA);
        void editPassFailInfoOfAStudnet(int semesterId, int batchId, int studentId, double GPA);
        int checkIfPassedFailInfoIsSaved(int semesterId, int batchId, int studentId);
        void saveCourseWiseGPAOfAStudent(int semesterId, int batchId,int courseId ,int studentId, double GPA);
        void editCourseWiseGPAOfAStudent(int semesterId, int batchId, int courseId, int studentId, double GPA);
        int checkIfCourseWiseGPAIsSaved(int semesterId, int batchId, int courseId, int studentId);
        int getStudentIdByUserId(string userId);
        BatchResDto[] getBatchesOfABatchCoordinator(string batchCoordinaorId, int programId);
    }
}
