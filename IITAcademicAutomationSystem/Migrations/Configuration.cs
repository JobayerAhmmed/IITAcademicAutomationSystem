namespace IITAcademicAutomationSystem.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IITAcademicAutomationSystem.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // Seed method runs every time update-database
        protected override void Seed(IITAcademicAutomationSystem.DAL.ApplicationDbContext context)
        {
            var programs = new List<Program>
            {
                new Program { ProgramName="BSSE" },
                new Program { ProgramName="MSSE" },
                new Program { ProgramName="PGDIT" },
                new Program { ProgramName="MIT" }
            };
            programs.ForEach(s => context.Programs.AddOrUpdate(p => p.ProgramName, s));
            context.SaveChanges();

            var semesters = new List<Semester>
            {
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 1 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 2 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 3 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 4 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 5 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 6 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 7 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id, SemesterNo = 8 },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = 1 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = 2 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id, SemesterNo = 3 },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = 1 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = 2 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id, SemesterNo = 3 },

                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = 1 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = 2 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = 3 },
                new Semester { ProgramId = programs.Single(s => s.ProgramName == "MIT").Id, SemesterNo = 4 }
            };
            semesters.ForEach(s => context.Semesters.AddOrUpdate(p => new { p.ProgramId, p.SemesterNo }, s));
            context.SaveChanges();

            var batches = new List<Batch>
            {
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 4,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "BSSE").Id,
                    BatchNo = 5,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MSSE").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "PGDIT").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 1,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 2,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                },
                new Batch {
                    ProgramId = programs.Single(s => s.ProgramName == "MIT").Id,
                    BatchNo = 3,
                    SemesterIdCurrent = semesters.Single(s =>
                        s.ProgramId == programs.Single(p => p.ProgramName == "MIT").Id &&
                        s.SemesterNo == 1).Id,
                    BatchStatus = "Active"
                }
            };
            batches.ForEach(b => context.Batches.AddOrUpdate(n => new { n.ProgramId, n.BatchNo }, b));
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
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "STAT103",
                    CourseTitle = "Probability and Statistics for Engineers-I",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "MATH104",
                    CourseTitle = "Calculus and Analytical Geometry",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "GE105",
                    CourseTitle = "Sociology",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "SE106",
                    CourseTitle = "Introduction to Software Engineering",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "CSE201",
                    CourseTitle = "Data Structure & Algorithm",
                    CourseCredit = 3,
                    CreditTheory = 1,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "EEE202",
                    CourseTitle = "Digital Systems Design",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "STAT203",
                    CourseTitle = "Probability and Statistics for Engineers-II",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "MATH204",
                    CourseTitle = "Ordinary Differential Equations",
                    CourseCredit = 3,
                    CreditTheory = 3,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "SE205",
                    CourseTitle = "Theory of Computing",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    CourseCode = "SE206",
                    CourseTitle = "Object Oriented Concepts I",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },

                // 4 MSSE courses
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MSSE").Id,
                    CourseCode = "MS1001",
                    CourseTitle = "Research Methodology",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MSSE").Id,
                    CourseCode = "MS1002",
                    CourseTitle = "Formal methods and Models in Software Engineering",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MSSE").Id,
                    CourseCode = "MS1003",
                    CourseTitle = "Secure Software Design and Programming",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MSSE").Id,
                    CourseCode = "MS1004",
                    CourseTitle = "Distributed Software Engineering",
                    CourseCredit = 3,
                    CreditTheory = 2,
                    CreditLab = 1
                },

                // 6 PGDIT courses
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD101",
                    CourseTitle = "Computer Fundamentals and Office Automation",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD104",
                    CourseTitle = "Structured Programming",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD106",
                    CourseTitle = "Operation System Concepts & UNIX OS",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD204",
                    CourseTitle = "DBMS & XML",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD201",
                    CourseTitle = "Data Structure & Algorithm",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD202",
                    CourseTitle = "Object Oriented Programming",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD105",
                    CourseTitle = "Introduction to Software Engineering",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="PGDIT").Id,
                    CourseCode = "PGD107",
                    CourseTitle = "Internet programming",
                    CourseCredit = 3,
                    CreditTheory = 0,
                    CreditLab = 0
                },

                // MIT courses
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM301",
                    CourseTitle = "Project Management and Business Info System",
                    CourseCredit = 2,
                    CreditTheory = 2,
                    CreditLab = 0
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM302",
                    CourseTitle = "Computer Programming",
                    CourseCredit = 4,
                    CreditTheory = 2,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM304",
                    CourseTitle = "Database Architecture and Administration",
                    CourseCredit = 4,
                    CreditTheory = 2,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM306",
                    CourseTitle = "Advanced Computer Networks & Internetworking",
                    CourseCredit = 4,
                    CreditTheory = 2,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM305",
                    CourseTitle = "Internet Computing",
                    CourseCredit = 4,
                    CreditTheory = 2,
                    CreditLab = 2
                },
                new Course {
                    ProgramId = programs.Single(p => p.ProgramName=="MIT").Id,
                    CourseCode = "MITM303",
                    CourseTitle = "Client Server Technology and System Programming",
                    CourseCredit = 4,
                    CreditTheory = 2,
                    CreditLab = 2
                }
            };
            courses.ForEach(c => context.Courses.AddOrUpdate(s => new { s.ProgramId, s.CourseCode, s.IsDelete}, c));
            context.SaveChanges();

            var users = new List<ApplicationUser>
            {
                // 10 students of BSSE 01
                new ApplicationUser
                {
                    Email = "iit1@iit.du.ac.bd",
                    UserName = "iit1@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Rayhanur Rahman",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit2@iit.du.ac.bd",
                    UserName = "iit2@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Mohayeminul Islam",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit3@iit.du.ac.bd",
                    UserName = "iit3@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Alim Ul Gias",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit4@iit.du.ac.bd",
                    UserName = "iit4@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Hasan Iqbal",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit5@iit.du.ac.bd",
                    UserName = "iit5@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Noman Bin Mannan",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit6@iit.du.ac.bd",
                    UserName = "iit6@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Ashraf Siddique",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit7@iit.du.ac.bd",
                    UserName = "iit7@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Tajkia Rahman Toma",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit8@iit.du.ac.bd",
                    UserName = "iit8@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Mostafa Kamal",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit9@iit.du.ac.bd",
                    UserName = "iit9@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Jobaer Islam Khan",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit10@iit.du.ac.bd",
                    UserName = "iit10@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Mohammad. Shafiqul Islam",
                    Status = "active"
                },

                // 10 students of BSSE 02
                new ApplicationUser
                {
                    Email = "iit11@iit.du.ac.bd",
                    UserName = "iit11@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Farhan Ishraque Khan",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit12@iit.du.ac.bd",
                    UserName = "iit12@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "M.T. Islam Chowdhury",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit13@iit.du.ac.bd",
                    UserName = "iit13@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Rayhanul Islam",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit14@iit.du.ac.bd",
                    UserName = "iit14@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Mahbub Ali Siddique",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit15@iit.du.ac.bd",
                    UserName = "iit15@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Saiful Islam Bhuiya",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit16@iit.du.ac.bd",
                    UserName = "iit16@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Rubaida Easmin",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit17@iit.du.ac.bd",
                    UserName = "iit17@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Mahedi Mahfuj",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit18@iit.du.ac.bd",
                    UserName = "iit18@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Hasan Ibrahim",
                    Status = "active",
                },
                new ApplicationUser
                {
                    Email = "iit19@iit.du.ac.bd",
                    UserName = "iit19@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Sharifur Rahaman",
                    Status = "active",
                },
                new ApplicationUser
                {
                    Email = "iit20@iit.du.ac.bd",
                    UserName = "iit20@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Jane Alam Sunny",
                    Status = "active"
                },

                // 10 teachers
                new ApplicationUser
                {
                    Email = "iit21@iit.du.ac.bd",
                    UserName = "iit21@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Zerina Begum",
                    Designation = "Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/16",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit22@iit.du.ac.bd",
                    UserName = "iit22@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Mohd. Zulfiquar Hafiz",
                    Designation = "Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/20",
                    ImagePath = "",
                    PhoneNumber = "01925647102",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit23@iit.du.ac.bd",
                    UserName = "iit23@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Md. Mahbubul Alam Joarder",
                    Designation = "Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/11",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit24@iit.du.ac.bd",
                    UserName = "iit24@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Kazi Muheymin-Us-Sakib",
                    Designation = "Associate Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/47",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit25@iit.du.ac.bd",
                    UserName = "iit25@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Md. Shariful Islam",
                    Designation = "Associate Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/19",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit26@iit.du.ac.bd",
                    UserName = "iit26@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Mohammad Shoyaib",
                    Designation = "Associate Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/48",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit27@iit.du.ac.bd",
                    UserName = "iit27@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Mohammed Shafiul Alam Khan",
                    Designation = "Assistant Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/63",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On study leave"
                },
                new ApplicationUser
                {
                    Email = "iit28@iit.du.ac.bd",
                    UserName = "iit28@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Mahmuda Rahman",
                    Designation = "Assistant Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/65",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On study leave"
                },
                new ApplicationUser
                {
                    Email = "iit29@iit.du.ac.bd",
                    UserName = "iit29@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Shah Mostafa Khaled",
                    Designation = "Assistant Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/50",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },
                new ApplicationUser
                {
                    Email = "iit30@iit.du.ac.bd",
                    UserName = "iit30@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Dr. Naushin Nower",
                    Designation = "Assistant Professor",
                    ProfileLink = "http://www.iit.du.ac.bd/about_iit/individual_teacher/69",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "On duty"
                },

                // Program officer
                new ApplicationUser
                {
                    Email = "iit31@iit.du.ac.bd",
                    UserName = "iit31@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Md. Abdul Bari",
                    Designation = "Program Officer",
                    ProfileLink = "",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "active"
                },
                new ApplicationUser
                {
                    Email = "iit32@iit.du.ac.bd",
                    UserName = "iit32@iit.du.ac.bd",
                    PasswordHash = new PasswordHasher().HashPassword("iit123"),
                    FullName = "Shampa Rani Kaur",
                    Designation = "Accountant",
                    ProfileLink = "",
                    ImagePath = "",
                    PhoneNumber = "01925647101",
                    Status = "active"
                }
            };
            users.ForEach(u => context.Users.AddOrUpdate(s => s.UserName, u));
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit1@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id && 
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0101",
                    CurrentRoll = "BSSE0101",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit2@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0102",
                    CurrentRoll  = "BSSE0102",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit3@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0103",
                    CurrentRoll  = "BSSE0103",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit4@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0104",
                    CurrentRoll  = "BSSE0104",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit5@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0105",
                    CurrentRoll  = "BSSE0105",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit6@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0106",
                    CurrentRoll  = "BSSE0106",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit7@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0107",
                    CurrentRoll  = "BSSE0107",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit8@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0108",
                    CurrentRoll  = "BSSE0108",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit9@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0109",
                    CurrentRoll  = "BSSE0109",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit10@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0110",
                    CurrentRoll  = "BSSE0110",
                    AdmissionSession = "2008-2009",
                    CurrentSession = "2008-2009"
                },

                // BSSE02
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit11@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0201",
                    CurrentRoll  = "BSSE0201",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit12@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0202",
                    CurrentRoll  = "BSSE0202",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit13@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0203",
                    CurrentRoll  = "BSSE0203",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit14@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0204",
                    CurrentRoll  = "BSSE0204",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit15@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0205",
                    CurrentRoll  = "BSSE0205",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit16@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0206",
                    CurrentRoll  = "BSSE0206",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit17@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0207",
                    CurrentRoll  = "BSSE0207",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit18@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0208",
                    CurrentRoll  = "BSSE0208",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit19@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0209",
                    CurrentRoll  = "BSSE0209",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                },
                new Student
                {
                    UserId = users.Single(u => u.UserName=="iit20@iit.du.ac.bd").Id,
                    ProgramId = programs.Single(p => p.ProgramName=="BSSE").Id,
                    BatchIdOriginal = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    BatchIdCurrent = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 2).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                    s.SemesterNo==1).Id,
                    OriginalRoll = "BSSE0210",
                    CurrentRoll  = "BSSE0210",
                    AdmissionSession = "2009-2010",
                    CurrentSession = "2009-2010"
                }
            };
            students.ForEach(s => context.Students.AddOrUpdate(p => p.UserId, s));
            context.SaveChanges();

            /*
            // CourseSemester
            var courseSemesters = new List<CourseSemester>
            {
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE101").Id,
                    TeacherId = users.Single(u => u.UserName == "iit24@iit.du.ac.bd").Id
                },
                
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE102").Id,
                    TeacherId = users.Single(u => u.UserName == "iit30@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "SE106").Id,
                    TeacherId = users.Single(u => u.UserName == "iit21@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE201").Id,
                    TeacherId = users.Single(u => u.UserName == "iit29@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "SE205").Id,
                    TeacherId = users.Single(u => u.UserName == "iit22@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="BSSE").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "SE206").Id,
                    TeacherId = users.Single(u => u.UserName == "iit28@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="MSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        c.CourseCode == "MS1001").Id,
                    TeacherId = users.Single(u => u.UserName == "iit24@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="MSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        c.CourseCode == "MS1002").Id,
                    TeacherId = users.Single(u => u.UserName == "iit23@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="MSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        c.CourseCode == "MS1003").Id,
                    TeacherId = users.Single(u => u.UserName == "iit25@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="MSSE").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "MSSE").Id &&
                        c.CourseCode == "MS1004").Id,
                    TeacherId = users.Single(u => u.UserName == "iit30@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD101").Id,
                    TeacherId = users.Single(u => u.UserName == "iit29@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD104").Id,
                    TeacherId = users.Single(u => u.UserName == "iit24@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD106").Id,
                    TeacherId = users.Single(u => u.UserName == "iit23@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==1).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD204").Id,
                    TeacherId = users.Single(u => u.UserName == "iit26@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD201").Id,
                    TeacherId = users.Single(u => u.UserName == "iit29@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD202").Id,
                    TeacherId = users.Single(u => u.UserName == "iit30@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD105").Id,
                    TeacherId = users.Single(u => u.UserName == "iit21@iit.du.ac.bd").Id
                },
                new CourseSemester
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId==programs.Single(p => p.ProgramName=="PGDIT").Id &&
                        s.SemesterNo==2).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "PGDIT").Id &&
                        c.CourseCode == "PGD107").Id,
                    TeacherId = users.Single(u => u.UserName == "iit25@iit.du.ac.bd").Id
                }
            };
            courseSemesters.ForEach(d => context.CourseSemesters.AddOrUpdate(s => 
                new { s.BatchId, s.SemesterId, s.CourseId, s.TeacherId }, d));
            context.SaveChanges();*/

            /*
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE101").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE102").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "STAT103").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 1).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "MATH104").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 2).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "CSE201").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 2).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "EEE202").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 2).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "STAT203").Id
                },
                new StudentCourse
                {
                    BatchId = batches.Single(b => b.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        b.BatchNo == 1).Id,
                    SemesterId = semesters.Single(s => s.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        s.SemesterNo == 2).Id,
                    StudentId = students.Single(s => s.UserId == users.Single(u => u.UserName == "iit1@iit.du.ac.bd").Id).Id,
                    CourseId = courses.Single(c => c.ProgramId == programs.Single(p => p.ProgramName == "BSSE").Id &&
                        c.CourseCode == "MATH204").Id
                }
            };
            studentCourses.ForEach(s => context.StudentCourses.AddOrUpdate(d => 
                new { d.BatchId, d.SemesterId, d.StudentId, d.CourseId }, s));
            context.SaveChanges();*/
        }
    }
}
