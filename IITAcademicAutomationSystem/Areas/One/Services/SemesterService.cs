using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface ISemesterService
    {
        Semester ViewSemester(int id);
        Semester GetFirstSemester(int programId);
        IEnumerable<Semester> GetSemestersOfProgram(int programId);
        IEnumerable<Student> GetEligibleStudentsForSemester(int batchId, int semesterId);
        bool AddStudentToStudentSemester(StudentSemester studentSemester);
        bool RemoveStudentFromStudentSemester(StudentSemester studentSemester);
        IEnumerable<Course> GetSemesterCourses(int batchId, int semesterId);
        IEnumerable<Student> GetSemesterStudents(int batchId, int semesterId);
        IEnumerable<ApplicationUser> GetSemesterTeachers(int batchId, int semesterId);
        bool AddCourseToSemester(CourseSemester courseSemester);
        bool RemoveCourseFromSemester(CourseSemester courseSemester);
        IEnumerable<Course> GetUnallocatedCoursesOfBatch(int batchId);
        void Dispose();
    }
    public class SemesterService : ISemesterService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        public SemesterService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // View Semester
        public Semester ViewSemester(int id)
        {
            return unitOfWork.SemesterRepository.GetSemesterById(id);
        }

        public Semester GetFirstSemester(int programId)
        {
            return unitOfWork.SemesterRepository.GetFirstSemester(programId);
        }

        public IEnumerable<Semester> GetSemestersOfProgram(int programId)
        {
            return unitOfWork.SemesterRepository.GetSemestersOfProgram(programId);
        }

        public IEnumerable<Student> GetEligibleStudentsForSemester(int batchId, int semesterId)
        {
            var batch = unitOfWork.BatchRepository.GetBatchById(batchId);
            var semester = unitOfWork.SemesterRepository.GetSemesterById(semesterId);
            var program = unitOfWork.ProgramRepository.GetProgramById(batch.ProgramId);

            var activeStudentsOfBatch = unitOfWork.StudentRepository.GetActiveStudentsOfBatch(batchId);

            var addedStudentSemestersInSemester =
                unitOfWork.StudentSemesterRepository.GetStudentSemestersForSemester(batchId, semesterId);

            List<Student> addedStudentsInSemester = new List<Student>();
            foreach (var studentSemester in addedStudentSemestersInSemester)
            {
                addedStudentsInSemester.Add(
                    unitOfWork.StudentRepository.GetStudentById(studentSemester.StudentId));
            }

            List<Student> remainStudents = new List<Student>();
            foreach (var student in activeStudentsOfBatch)
            {
                if (addedStudentsInSemester.Contains(student))
                {
                    continue;
                }
                remainStudents.Add(student);
            }

            // For first semester, return active students without checking the previous semester
            if (semester.SemesterNo == 1)
                return remainStudents;

            List<Student> eligibleStudents = new List<Student>();

            // Different criteria for different programs
            if (program.ProgramName == "BSSE")
            {
                Semester previousSemester =
                        unitOfWork.SemesterRepository.GetSemesterBySemesterNo(program.Id, semester.SemesterNo - 1);
                Batch previousBatch = null;
                if (batch.BatchNo > 1)
                {
                    previousBatch = unitOfWork.BatchRepository.GetBatchByBatchNo(program.Id, batch.BatchNo - 1);
                }

                foreach (var student in remainStudents)
                {
                    // check whether a student exists in previous semester
                    StudentSemester studentInPreviousSemester =
                        unitOfWork.StudentSemesterRepository.GetStudentSemester(
                            batchId, previousSemester.Id, student.Id);
                    if (studentInPreviousSemester != null)
                    {
                        // check whether the student has passed in previous semester
                        // if passed then add to eligible students
                        if (studentInPreviousSemester.GPA >= 2.00)
                            eligibleStudents.Add(student);
                    }
                    else if(previousBatch != null)
                    {
                        // if not found in previous semester then check whether - 
                        // he exists in previous semester of previous batch
                        var studentInPreviousSemesterOfPreviousBatch =
                            unitOfWork.StudentSemesterRepository.GetStudentSemester(
                                previousBatch.Id, previousSemester.Id, student.Id);

                        if (studentInPreviousSemesterOfPreviousBatch != null)
                        {
                            // check whether the student has passed in the previous semester
                            // if passed in all courses then add to eligible students
                            if (studentInPreviousSemesterOfPreviousBatch.GPA >= 2.00)
                                eligibleStudents.Add(student);
                        }
                    }
                }

                return eligibleStudents;
            }
            else if (program.ProgramName == "MSSE")
            {
                Semester previousSemester =
                        unitOfWork.SemesterRepository.GetSemesterBySemesterNo(program.Id, semester.SemesterNo - 1);

                foreach (var student in remainStudents)
                {
                    // check whether a student exists in previous semester
                    StudentSemester studentInPreviousSemester =
                        unitOfWork.StudentSemesterRepository.GetStudentSemester(
                            batchId, previousSemester.Id, student.Id);
                    if (studentInPreviousSemester != null)
                    {
                        // check whether the student has passed in previous semester
                        // if passed then add to eligible students
                        if (studentInPreviousSemester.GPA >= 2.00)
                            eligibleStudents.Add(student);
                    }
                }

                return eligibleStudents;
            }
            // PGDIT, MIT
            else
            {
                return remainStudents;
            }
        }

        public IEnumerable<Student> GetSemesterStudents(int batchId, int semesterId)
        {
            var studentSemesters = 
                unitOfWork.StudentSemesterRepository.GetStudentSemestersForSemester(batchId, semesterId);
            List<Student> students = new List<Student>();
            foreach (var item in studentSemesters)
            {
                students.Add(unitOfWork.StudentRepository.GetStudentById(item.StudentId));
            }
            return students;
        }
        public IEnumerable<ApplicationUser> GetSemesterTeachers(int batchId, int semesterId)
        {
            var courseSemesterEntities =
                unitOfWork.CourseSemesterRepository.GetSemesterTeachers(batchId, semesterId);

            List<ApplicationUser> teachers = new List<ApplicationUser>();

            foreach (var item in courseSemesterEntities)
            {
                teachers.Add(unitOfWork.UserRepository.GetUserById(item.TeacherId));
            }

            return teachers;
        }


        public bool AddStudentToStudentSemester(StudentSemester studentSemester)
        {
            try
            {
                unitOfWork.StudentSemesterRepository.AddStudentSemester(studentSemester);
                unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("", "Unable to save, please try again.");
                return false;
            }
        }

        public bool RemoveStudentFromStudentSemester(StudentSemester studentSemester)
        {
            StudentSemester studentSemesterToRemove =
                unitOfWork.StudentSemesterRepository.GetStudentSemester(
                    studentSemester.BatchId, studentSemester.SemesterId, studentSemester.StudentId);
            try
            {
                unitOfWork.StudentSemesterRepository.RemoveStudentSemester(studentSemesterToRemove);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public IEnumerable<Course> GetSemesterCourses(int batchId, int semesterId)
        {
            IEnumerable<CourseSemester> semesterCourses =
                unitOfWork.CourseSemesterRepository.GetSemesterCourses(batchId, semesterId);
            IList<Course> courses = new List<Course>();
            Course course;

            foreach (var item in semesterCourses)
            {
                course = unitOfWork.CourseRepository.GetCourseById(item.CourseId);
                courses.Add(course);
                //course = new Course();
            }

            return courses.ToList();
        }

        public bool AddCourseToSemester(CourseSemester courseSemester)
        {
            try
            {
                unitOfWork.CourseSemesterRepository.AddCourseToSemester(courseSemester);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public bool RemoveCourseFromSemester(CourseSemester courseSemester)
        {
            CourseSemester courseSemesterToRemove =
                unitOfWork.CourseSemesterRepository.GetCourseSemester(
                    courseSemester.BatchId, courseSemester.SemesterId, courseSemester.CourseId);
            try
            {
                unitOfWork.CourseSemesterRepository.RemoveCourseFromSemester(courseSemesterToRemove);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public IEnumerable<Course> GetUnallocatedCoursesOfBatch(int batchId)
        {
            var batch = unitOfWork.BatchRepository.GetBatchById(batchId);

            IEnumerable<CourseSemester> allocatedCourseSemesters =
                unitOfWork.CourseSemesterRepository.GetAllocatedCourseSemesterOfBatch(batchId);

            List<Course> allocatedCourses = new List<Course>();
            foreach (var courseSemester in allocatedCourseSemesters)
            {
                allocatedCourses.Add(unitOfWork.CourseRepository.GetCourseById(courseSemester.CourseId));
            }

            IEnumerable<Course> programCourses = unitOfWork.CourseRepository.GetCoursesOfProgram(batch.ProgramId);
            List<Course> unallocatedCourses = new List<Course>();

            foreach (var course in programCourses)
            {
                if (allocatedCourses.Contains(course))
                {
                    continue;
                }
                unallocatedCourses.Add(course);
            }

            return unallocatedCourses;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}