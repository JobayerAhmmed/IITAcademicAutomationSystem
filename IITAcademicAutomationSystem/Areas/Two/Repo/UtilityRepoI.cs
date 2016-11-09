using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface UtilityRepoI
    {
        Program getProgramByProgramId(int programId);
        List<Program> getAllPrograms();
        List<Program> getProgramsOfATeacher(int teacherId);
        List<Semester> getSemestersOfAProgram(int programId);
        List<Semester> getSemestersOfATeacherOfAProgram(int teacherId, int programId);
        Semester getSemesterBySemesterId(int semesterId);
        List<Semester> getStudentsAllSemester(int programId,int currentSemesterNo);
        List<Course> getCoursesOfATeacherOfASemesterOfAProgram(int teacherId, int programId, int semesterId);
        Batch getBatch(int programId, int semesterId);
        List<Batch> getBatchesOfAProgram(int programId);

        void savePassFailInfoOfAStudnet(StudentSemester studentSemester);
        void editPassFailInfoOfAStudnet(StudentSemester studentSemester);
        int checkIfPassedFailInfoIsSaved(int semesterId, int batchId, int studentId);
        void saveCourseWiseGPAOfAStudent(StudentCourse studentCourse);
        void editCourseWiseGPAOfAStudent(StudentCourse studentCourse);
        int checkIfCourseWiseGPAIsSaved(int semesterId, int batchId, int courseId, int studentId);
    }
}
