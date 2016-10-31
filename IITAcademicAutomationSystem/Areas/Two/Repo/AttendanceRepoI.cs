using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface AttendanceRepoI
    {
        int getLastClassNumber(int programId, int semesterId, int batchId, int courseId);
        void saveAttendance(Attendance[] attendanceList);
        List<Attendance> getAttendance(int programId, int semesterId, int batchId, int courseId);
        List<Attendance> getAttendance(int programId, int semesterId, int batchId, int courseId,int classNo);
        List<Attendance> getAttendanceOfAStudentOfAllCourses(int programId, int semesterId, int batchId, int studentId);
        void saveEditedAttendance(Attendance[] attendanceList);
    }
}
