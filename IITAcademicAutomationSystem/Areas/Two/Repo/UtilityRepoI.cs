using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity;
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
        List<Program> getProgramsOfATeacher(string teacherId);
        List<Semester> getSemestersOfAProgram(int programId);
        List<Semester> getSemestersOfATeacherOfAProgram(string teacherId, int programId);
        Semester getSemesterBySemesterId(int semesterId);
        List<Semester> getStudentsAllSemester(int programId,int currentSemesterNo);
        List<Course> getCoursesOfATeacherOfASemesterOfAProgram(string teacherId, int programId, int semesterId);
        List<Course> getCoursesOfAStudentOfASemesterOfAProgram(int studentId, int programId, int semesterId,int batchId);
        Batch getBatch(int programId, int semesterId);
        List<Batch> getBatchesOfAProgram(int programId);
        Batch getBatchByBatchId(int batchId);
        void savePassFailInfoOfAStudnet(StudentSemester studentSemester);
        void editPassFailInfoOfAStudnet(StudentSemester studentSemester);
        int checkIfPassedFailInfoIsSaved(int semesterId, int batchId, int studentId);
        void saveCourseWiseGPAOfAStudent(StudentCourse studentCourse);
        void editCourseWiseGPAOfAStudent(StudentCourse studentCourse);
        int checkIfCourseWiseGPAIsSaved(int semesterId, int batchId, int courseId, int studentId);
        int getStudentIdByUserId(string userId);
        Semester getSemesterOfAStudentByStudentId(int studentId);
        Program getProgramOfAStudentBySemesterId(int semesterId);
        Batch getCurrentBatchOfAStudentByStudentId(int studentId);
        List<Student> getStudentsOfASemester(int programId, int semesterId, int batchId);
        List<Student> getStudentsOfACOurse(int programId, int semesterId, int batchId, int courseId);
        Student getStudentByStudentId(int studentId);
        ApplicationUser getUserByUserId(string userId);
        Course getCourseByCourseId(int courseId);
        List<Course> getAllCoursesOfASemester(int programId, int semesterId, int batchId);
        List<Semester> getSemestersOfABatchCoordinator(string batchCoordinatorId,int semesterId);
    }
}
