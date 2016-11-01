using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Repositories;
using IITAcademicAutomationSystem.Models;
//using IITAcademicAutomationSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.DAL
{
    // Make sure that when you use multiple repositories, they share a single database context.
    // Each repository property returns a repository instance that has been instantiated 
    // using the same database context instance as the other repository instances.
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IStudentRepository studentRepository;
        private IProgramRepository programRepository;
        private IBatchRepository batchRepository;
        private ISemesterRepository semesterRepository;
        private ICourseRepository courseRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(context);
                }
                return userRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(context);
                }
                return roleRepository;
            }
        }
        public IStudentRepository StudentRepository
        {
            get
            {
                if (studentRepository == null)
                {
                    studentRepository = new StudentRepository(context);
                }
                return studentRepository;
            }
        }
        public IProgramRepository ProgramRepository
        {
            get
            {
                if (programRepository == null)
                {
                    programRepository = new ProgramRepository(context);
                }
                return programRepository;
            }
        }
        public IBatchRepository BatchRepository
        {
            get
            {
                if (batchRepository == null)
                {
                    batchRepository = new BatchRepository(context);
                }
                return batchRepository;
            }
        }
        public ISemesterRepository SemesterRepository
        {
            get
            {
                if (semesterRepository == null)
                {
                    semesterRepository = new SemesterRepository(context);
                }
                return semesterRepository;
            }
        }
        public ICourseRepository CourseRepository
        {
            get
            {
                if (courseRepository == null)
                {
                    courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }
        
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}