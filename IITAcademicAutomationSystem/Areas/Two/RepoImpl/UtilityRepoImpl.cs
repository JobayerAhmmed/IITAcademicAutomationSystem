using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl

{
    public class UtilityRepoImpl : UtilityRepoI
    {

        ApplicationDbContext db = new ApplicationDbContext();
        public List<Program> getAllPrograms()
        {
            try
            {
                var query = (from Program in db.Programs
                                select Program);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Program> getProgramsOfATeacher(string teacherId)//has to be changed
        {
            try
            {
                
                var query = (from CourseSemester  in db.CourseSemesters
                            join Semester in db.Semesters on CourseSemester.SemesterId equals Semester.Id
                            join Program in db.Programs on Semester.ProgramId equals Program.Id
                            where CourseSemester.TeacherId==teacherId 
                            select Program).Distinct();
                return query.ToList();
               
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Program getProgramByProgramId(int programId)
        {
            try
            {
                var query = (from Program in db.Programs where Program.Id == programId select Program).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Semester> getSemestersOfAProgram(int programId)
        {
            try
            {
                var query = (from Semester in db.Semesters where Semester.ProgramId == programId select Semester).ToList();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Semester> getSemestersOfATeacherOfAProgram(string teacherId, int programId)//has to be changed
        {   
            try
            {
                var query = (from CourseSemester in db.CourseSemesters
                            join Semester in db.Semesters on CourseSemester.SemesterId equals Semester.Id
                            where CourseSemester.TeacherId == teacherId && Semester.ProgramId == programId
                            select Semester).Distinct();
                return query.ToList();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Course> getCoursesOfATeacherOfASemesterOfAProgram(string teacherId, int programId, int semesterId)
        {
            
            try
            {
                var query = from CourseSemester in db.CourseSemesters
                            join Semester in db.Semesters on CourseSemester.SemesterId equals Semester.Id
                            join Course in db.Courses on CourseSemester.CourseId equals Course.Id
                            where CourseSemester.TeacherId == teacherId && Semester.ProgramId == programId
                            && Semester.Id== semesterId
                            select Course;

                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Batch getBatch(int programId, int semesterId)
        {
            try
            {
                var query = (from Batch in db.Batches where Batch.ProgramId == programId && Batch.SemesterIdCurrent == semesterId select Batch).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Semester getSemesterBySemesterId(int semesterId)
        {
            try
            {
                var query = (from Semester in db.Semesters where Semester.Id == semesterId select Semester).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Semester> getStudentsAllSemester(int programId,int currentSemesterNo)
        {
            try
            {
                var query = (from Semester in db.Semesters
                    where ((Semester.SemesterNo == currentSemesterNo || Semester.SemesterNo < currentSemesterNo) && Semester.ProgramId == programId)
                             orderby Semester.SemesterNo descending
                             select Semester);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Batch> getBatchesOfAProgram(int programId)
        {
            try
            {
                var query = (from Batch in db.Batches where Batch.ProgramId == programId && Batch.BatchStatus == "Active" select Batch);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void savePassFailInfoOfAStudnet(StudentSemester studentSemester)
        {
            try
            {
                db.StudentSemesters.Add(studentSemester);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editPassFailInfoOfAStudnet(StudentSemester studentSemester)
        {
            try
            {
                bool temp = checkIfAStudentHasFailedACourseInASemester(studentSemester.StudentId,
                    studentSemester.SemesterId, studentSemester.BatchId);
                if (temp == true)
                    studentSemester.GPA = 0.00;
   
                var query = (from StudentSemester in db.StudentSemesters where StudentSemester.SemesterId == studentSemester.SemesterId && StudentSemester.BatchId == studentSemester.BatchId && StudentSemester.StudentId == studentSemester.StudentId select StudentSemester).FirstOrDefault();

                if (query != null)
                {
                    query.GPA = studentSemester.GPA;
                    db.Entry(query).CurrentValues.SetValues(query);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int checkIfPassedFailInfoIsSaved(int semesterId, int batchId, int studentId)
        {
            try
            {
                var query = (from StudentSemester in db.StudentSemesters where StudentSemester.SemesterId == semesterId && StudentSemester.BatchId == batchId && StudentSemester.StudentId == studentId select StudentSemester).FirstOrDefault();
                if (query == null)
                    return -1; //has to be created
                if (query.GPA == 0 || query.GPA == 0.00)
                    return 0; //has to be edited
                if (query != null)
                    return 1; //nothing to do
                return
                    2; //this path will never reach
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveCourseWiseGPAOfAStudent(StudentCourse studentCourse)
        {
            try
            {
                db.StudentCourses.Add(studentCourse);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editCourseWiseGPAOfAStudent(StudentCourse studentCourse)
        {
            try
            {
                var query = (from StudentCourse in db.StudentCourses where StudentCourse.SemesterId == studentCourse.SemesterId  && StudentCourse.BatchId == studentCourse.BatchId && StudentCourse.CourseId == studentCourse.CourseId && StudentCourse.StudentId == studentCourse.StudentId select StudentCourse).FirstOrDefault();

                if (query != null)
                {
                    query.GradePoint = studentCourse.GradePoint;
                    db.Entry(query).CurrentValues.SetValues(query);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int checkIfCourseWiseGPAIsSaved(int semesterId, int batchId, int courseId, int studentId)
        {
            try
            {
                var query = (from StudentCourse in db.StudentCourses where StudentCourse.SemesterId == semesterId && StudentCourse.CourseId==courseId && StudentCourse.BatchId == batchId && StudentCourse.StudentId == studentId select StudentCourse).FirstOrDefault();
                if (query == null)
                    return -1; //has to be created
                if(query.GradePoint==0 || query.GradePoint == 0.00)
                    return 0; //has to be edited
                if (query != null)
                    return 1; //nothing to do
                return
                    2; //this path will never reach
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Course> getCoursesOfAStudentOfASemesterOfAProgram(int studentId, int programId, int semesterId, int batchId)
        {
            try
            {
                var query = from StudentCourse in db.StudentCourses
                            join Course in db.Courses on StudentCourse.CourseId equals Course.Id
                            where StudentCourse.StudentId == studentId && StudentCourse.SemesterId == semesterId
                            && StudentCourse.BatchId == batchId
                            select Course;
                var v = query;

                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int getStudentIdByUserId(string userId)
        {
            try
            {
                var query = (from Student in db.Students where Student.UserId == userId select Student).FirstOrDefault();
                return query.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Semester getSemesterOfAStudentByStudentId(int studentId)
        {
            try
            {
                var query = (from Student in db.Students
                            join Semester in db.Semesters on Student.SemesterId equals Semester.Id
                            where Student.Id == studentId 
                            select Semester).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Program getProgramOfAStudentBySemesterId(int semesterId)
        {
            try
            {
                var query = (from Semester in db.Semesters
                             join Program in db.Programs on Semester.ProgramId equals Program.Id
                             where Semester.Id == semesterId
                             select Program).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Batch getCurrentBatchOfAStudentByStudentId(int studentId)
        {
            try
            {
                var query = (from Student in db.Students
                             join Batch in db.Batches on Student.BatchIdCurrent equals Batch.Id
                             where Student.Id == studentId
                             select Batch).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Student> getStudentsOfASemester(int programId, int semesterId, int batchId)
        {
            try
            {
                var query = (from StudentSemester in db.StudentSemesters
                    join Student in db.Students on StudentSemester.StudentId equals Student.Id
                    where StudentSemester.SemesterId == semesterId && StudentSemester.BatchId == batchId
                    select Student);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Student> getStudentsOfACOurse(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var query = (from StudentCourse in db.StudentCourses
                             join Student in db.Students on StudentCourse.StudentId equals Student.Id
                             where StudentCourse.SemesterId == semesterId && StudentCourse.BatchId == batchId
                             && StudentCourse.CourseId==courseId
                             select Student);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Student getStudentByStudentId(int studentId)
        {
            try
            {
                var query = (from Student in db.Students where Student.Id == studentId select Student).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ApplicationUser getUserByUserId(string userId)
        {
            try
            {
                var query = (from User in db.Users where User.Id == userId select User).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Batch getBatchByBatchId(int batchId)
        {
            try
            {
                var query = (from Batch in db.Batches where Batch.Id == batchId select Batch).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Course getCourseByCourseId(int courseId)
        {
            try
            {
                var query = (from Course in db.Courses where Course.Id == courseId select Course).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Course> getAllCoursesOfASemester(int programId, int semesterId, int batchId)
        {
            try
            {
                var query = (from CourseSemester in db.CourseSemesters
                             join Course in db.Courses on CourseSemester.CourseId equals Course.Id
                             where CourseSemester.SemesterId == semesterId && CourseSemester.BatchId == batchId
                             select Course);
                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool checkIfAStudentHasFailedACourseInASemester(int studentId, int semesterId,int batchId)
        {
            var query = (from StudentCourse in db.StudentCourses where StudentCourse.StudentId == studentId && StudentCourse.SemesterId == semesterId && StudentCourse.BatchId == batchId &&  StudentCourse.GradePoint == 0.00 select StudentCourse).FirstOrDefault();

            if (query != null)
                return true;
            else
                return false;
        }
    }
}