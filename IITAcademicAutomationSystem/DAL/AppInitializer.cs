using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.DAL
{
    public class AppInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
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
                //new Semester { ProgramId= }
            };

            var batches = new List<Batch>
            {
                //new Batch { ProgramId }
            };
        }
    }
}