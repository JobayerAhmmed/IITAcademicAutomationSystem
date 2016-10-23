using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.DAL
{
    public class AppInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //var users = new List<User> { };
            //var students = new List<Student> { };

            var programs = new List<Program>
            {
                new Program { ProgramName="BSSE" },
                new Program { ProgramName="MSSE" },
                new Program { ProgramName="PGDIT" },
                new Program { ProgramName="MIT" }
            };
            programs.ForEach(s => context.Programs.Add(s));
            context.SaveChanges();

            var semesters = new List<Semester>
            {
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 1" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 2" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 3" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 4" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 5" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 6" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 7" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = "Semester 8" },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = "Semester 1" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = "Semester 2" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = "Semester 3" },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = "Semester 1" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = "Semester 2" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = "Semester 3" },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = "Semester 1" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = "Semester 2" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = "Semester 3" },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = "Semester 4" }
            };
            semesters.ForEach(s => context.Semesters.Add(s));
            context.SaveChanges();

            var batches = new List<Batch>
            {
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s => 
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id && 
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 4,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 5,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == "Semester 1").Id,
                    BatchStatus = "Active"
                }
            };
            batches.ForEach(b => context.Batches.Add(b));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "CSE101",
                    CourseTitle = "Structured Programming",
                    CourseCredit = 3,
                    CreditTheory = 1,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "CSE102",
                    CourseTitle = "Discrete Mathematics",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                }
            };
            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            //var courseContents = new List<CourseContent>
            //{
            //    new CourseContent {
            //        CourseId = courses.Single(c => c.CourseCode=="CSE101").Id,
            //        TeacherId = users.Single(u => u.).Id,
            //    }
            //};
        }
    }
}