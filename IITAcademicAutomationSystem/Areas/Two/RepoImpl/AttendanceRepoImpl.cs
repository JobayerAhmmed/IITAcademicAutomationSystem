using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.DAL;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    class AttendanceRepoImpl : AttendanceRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Attendance> getAttendance(int programId, int semesterId, int batchId, int courseId)// if possible- write query to get unique class
        {
            try
            {
                var query = from Attendance in db.Attendances where Attendance.programId == programId && Attendance.semesterId == semesterId && Attendance.batchId == batchId && Attendance.courseId == courseId select Attendance;
                var attendanceList = query.ToList();

                return attendanceList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Attendance> getAttendance(int programId, int semesterId, int batchId, int courseId, int classNo)
        {
            try
            {
                var query = from Attendance in db.Attendances where Attendance.programId == programId && Attendance.semesterId == semesterId && Attendance.batchId == batchId && Attendance.courseId == courseId && Attendance.class_no == classNo select Attendance;
                var attendanceList = query.ToList();

                return attendanceList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Attendance> getAttendanceOfAStudentOfAllCourses(int programId, int semesterId, int batchId, int studentId)
        {
            try
            {
                var query = from Attendance in db.Attendances where Attendance.programId == programId && Attendance.semesterId == semesterId && Attendance.batchId == batchId && Attendance.studentId == studentId select Attendance;
                var attendanceList = query.ToList();

                return attendanceList;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int getLastClassNumber(int programId, int semesterId, int batchId, int courseId)
        {
           try
            {
                var query = from Attendance in db.Attendances where Attendance.programId == programId && Attendance.semesterId == semesterId && Attendance.batchId == batchId && Attendance.courseId == courseId select Attendance;
                var attendance = query.ToList();

                if (attendance.Count==0)
                    return 0;

                else
                {
                    var max = 0;
                    for(int i=0;i< attendance.Count; i++)
                    {
                        if (attendance.ElementAt(i).class_no > max)
                            max = attendance.ElementAt(i).class_no;
                    }

                    return max;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveAttendance(Attendance[] attendanceList)
        {
           try
            {
                for(int i=0;i< attendanceList.Length; i++)
                {
                    db.Attendances.Add(attendanceList[i]);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveEditedAttendance(Attendance[] attendanceList)
        {
            try
            {
                for (int i = 0; i < attendanceList.Length; i++)
                {

                    var originalMarks = db.Attendances.Find(attendanceList[i].Id);


                    if (originalMarks != null)
                    {
                        originalMarks.is_present = attendanceList[i].is_present;
                        db.Entry(originalMarks).CurrentValues.SetValues(originalMarks);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
