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
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Program> getProgramsOfATeacher(int teacherId)//has to be changed
        {

            try
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

        public List<Semester> getSemestersOfATeacherOfAProgram(int teacherId, int programId)//has to be changed
        {            
            List<Semester> semesterList = new List<Semester>();
            try
            {
                var query = (from Semester in db.Semesters where Semester.ProgramId == programId select Semester).ToList();
                return query;
            }
            catch(Exception e)
            {
                    throw e;
            }
        }

        public List<Course> getCoursesOfATeacherOfASemesterOfAProgram(int teacherId, int programId, int semesterId)
        {
            List<Course> courseList = new List<Course>();
            try
            {
                Course course = new Course();
                course.Id = 1;
                course.CourseCode = "CSE-801";
                courseList.Add(course);

                course = new Course();
                course.Id = 2;
                course.CourseCode = "CSE-802";
                courseList.Add(course);

                course = new Course();
                course.Id = 3;
                course.CourseCode = "CSE-803";
                courseList.Add(course);

                return courseList;
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
                Batch batch = new Batch();
                batch.Id = 4;
                batch.BatchNo = 5;
                return batch;
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
                List<Batch> batchList=new List<Batch>();
                Batch batch=new Batch();
                batch.Id = 4;
                batch.BatchNo = 5;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 2;
                batch.BatchNo = 4;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 3;
                batch.BatchNo = 3;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 6;
                batch.BatchNo = 6;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 7;
                batch.BatchNo = 7;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 8;
                batch.BatchNo = 8;
                batchList.Add(batch);

                batch = new Batch();
                batch.Id = 9;
                batch.BatchNo = 9;
                batchList.Add(batch);

                return batchList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}