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
        List<Program> getAllPrograms();
        List<Program> getProgramsOfATeacher(int teacherId);
        List<Semester> getSemestersOfAProgram(int programId);
        List<Semester> getSemestersOfATeacherOfAProgram(int teacherId, int programId);
        List<Course> getCoursesOfATeacherOfASemesterOfAProgram(int teacherId, int programId, int semesterId);
        Batch getBatch(int programId, int semesterId);
    }
}
