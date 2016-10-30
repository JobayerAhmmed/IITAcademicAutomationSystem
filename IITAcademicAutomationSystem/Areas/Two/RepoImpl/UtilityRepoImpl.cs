using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Models;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl

{
    public class UtilityRepoImpl : UtilityRepoI
    {

        public List<Program> getAllPrograms()
        {
            List<Program> programList = new List<Program>();
            try
            {
                Program program = new Program();
                program.Id = 1;
                program.ProgramName = "BSSE";
                programList.Add(program);

                program = new Program();
                program.Id = 2;
                program.ProgramName = "MSSE";
                programList.Add(program);

                program = new Program();
                program.Id = 3;
                program.ProgramName = "PGDIT";
                programList.Add(program);

                program = new Program();
                program.Id = 4;
                program.ProgramName = "MIT";
                programList.Add(program);

                return programList;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Program> getProgramsOfATeacher(int teacherId)
        {
            List<Program> programList = new List<Program>();
            try
            {
                Program program = new Program();
                program.Id = 1;
                program.ProgramName = "BSSE";
                programList.Add(program);

                program = new Program();
                program.Id = 2;
                program.ProgramName = "MSSE";
                programList.Add(program);

                program = new Program();
                program.Id = 3;
                program.ProgramName = "PGDIT";
                programList.Add(program);

                return programList;

               
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<Semester> getSemestersOfAProgram(int programId)
        {
            List<Semester> semesterList = new List<Semester>();
            try
            {
                Semester semester = new Semester();
                semester.Id = 1;
                semester.SemesterNo = "1st ";
                semesterList.Add(semester);

                semester = new Semester();
                semester.Id = 2;
                semester.SemesterNo = "2nd";
                semesterList.Add(semester);


                semester = new Semester();
                semester.Id = 3;
                semester.SemesterNo = "3rd";
                semesterList.Add(semester);

                semester.Id = 4;
                semester.SemesterNo = "4th";
                semesterList.Add(semester);

                semester = new Semester();
                semester.Id = 5;
                semester.SemesterNo = "5th";
                semesterList.Add(semester);


                semester = new Semester();
                semester.Id = 6;
                semester.SemesterNo = "6th";
                semesterList.Add(semester);

                semester = new Semester();
                semester.Id = 7;
                semester.SemesterNo = "7th";
                semesterList.Add(semester);

                semester = new Semester();
                semester.Id = 8;
                semester.SemesterNo = "8th";
                semesterList.Add(semester);

                return semesterList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Semester> getSemestersOfATeacherOfAProgram(int teacherId, int programId)
        {            
            List<Semester> semesterList = new List<Semester>();
            try
            {
                Semester semester = new Semester();
                semester.Id = 1;
                semester.SemesterNo = "3rd";
                semesterList.Add(semester);

                semester = new Semester();
                semester.Id = 2;
                semester.SemesterNo = "6th";
                semesterList.Add(semester);


                semester = new Semester();
                semester.Id = 3;
                semester.SemesterNo = "8th";
                semesterList.Add(semester);

                return semesterList;
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

        
    }
}