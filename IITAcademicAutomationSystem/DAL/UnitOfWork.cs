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
        private IStudentRepository studentRepository;
        private IProgramRepository programRepository;
        private IBatchRepository batchRepository;
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
        

        /*private GenericRepository<Program> programRepository;
        private GenericRepository<Semester> semesterRepository;
        private GenericRepository<Batch> batchRepository;
        private GenericRepository<CourseViewModel> courseRepository;
        private GenericRepository<CourseSemester> courseSemesterRepository;
        private GenericRepository<CourseContent> courseContentRepository;
        private GenericRepository<StudentCourse> studentCourseRepository;

        public GenericRepository<Program> ProgramRepository
        {
            get
            {
                if (this.programRepository == null)
                {
                    this.programRepository = new GenericRepository<Program>(context);
                }
                return programRepository;
            }
        }

        public GenericRepository<Semester> SemesterRepository
        {
            get
            {
                if (this.semesterRepository == null)
                {
                    this.semesterRepository = new GenericRepository<Semester>(context);
                }
                return semesterRepository;
            }
        }

        public GenericRepository<Batch> BatchRepository
        {
            get
            {
                if (this.batchRepository == null)
                {
                    this.batchRepository = new GenericRepository<Batch>(context);
                }
                return batchRepository;
            }
        }

        public GenericRepository<CourseViewModel> CourseRepository
        {
            get
            {
                if (this.courseRepository == null)
                {
                    this.courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }

        public GenericRepository<CourseSemester> CourseSemesterRepository
        {
            get
            {
                if (this.courseSemesterRepository == null)
                {
                    this.courseSemesterRepository = new GenericRepository<CourseSemester>(context);
                }
                return courseSemesterRepository;
            }
        }

        public GenericRepository<CourseContent> CourseContentRepository
        {
            get
            {
                if (this.courseContentRepository == null)
                {
                    this.courseContentRepository = new GenericRepository<CourseContent>(context);
                }
                return courseContentRepository;
            }
        }

        public GenericRepository<StudentCourse> StudentCourseRepository
        {
            get
            {
                if (this.studentCourseRepository == null)
                {
                    this.studentCourseRepository = new GenericRepository<StudentCourse>(context);
                }
                return studentCourseRepository;
            }
        }
        */
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