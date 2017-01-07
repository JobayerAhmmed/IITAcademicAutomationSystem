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
        Semester GetPreviousSemester(int semesterId);
        IEnumerable<Semester> GetSemestersOfProgram(int programId);
        IEnumerable<Student> GetEligibleStudentsForSemester(int batchId, int semesterId);
        IEnumerable<Student> GetEligibleStudentsForCourse(int batchId, int semesterId, int courseId);
        bool AddStudentToStudentSemester(StudentSemester studentSemester);
        bool RemoveStudentFromStudentSemester(StudentSemester studentSemester);
        bool AddStudentToStudentCourse(StudentCourse studentCourse);
        bool RemoveStudentFromStudentCourse(StudentCourse studentCourse);
        bool RemoveStudentsFromStudentCourse(StudentCourse studentCourse);
        bool RemoveCoursesFromStudentCourse(StudentCourse studentCourse);
        IEnumerable<Course> GetSemesterCourses(int batchId, int semesterId);
        IEnumerable<Student> GetSemesterStudents(int batchId, int semesterId);
        IEnumerable<Student> GetSemesterPassedStudents(int batchId, int semesterId);
        IEnumerable<Student> GetSemesterFailedStudents(int batchId, int semesterId);
        double GetSemesterGpaOfStudent(int batchId, int semesterId, int studentId);
        IEnumerable<Student> GetCourseStudents(int batchId, int semesterId, int courseId);
        int GetCourseStudentsNumber(int batchId, int semesterId, int courseId);
        IEnumerable<ApplicationUser> GetCourseTeachers(int batchId, int semesterId, int courseId);
        int GetCourseTeachersNumber(int batchId, int semesterId, int courseId);
        IEnumerable<ApplicationUser> GetSemesterTeachers(int batchId, int semesterId);
        bool AddCourseToSemester(CourseSemester courseSemester);
        bool RemoveCourseFromSemester(CourseSemester courseSemester);
        bool RemoveCoursesFromSemester(CourseSemester courseSemester);
        bool AddTeacherToCourseSemester(CourseSemester courseSemester);
        bool RemoveTeacherFromCourseSemester(CourseSemester courseSemester);
        IEnumerable<Course> GetUnallocatedCoursesOfBatch(int batchId);
        StudentSemester GetStudentSemester(int batchId, int semesterId, int studentId);
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

        public Semester GetPreviousSemester(int semesterId)
        {
            Semester currentSemester = unitOfWork.SemesterRepository.GetSemesterById(semesterId);
            if (currentSemester == null || currentSemester.SemesterNo <= 1)
            {
                return null;
            }

            return unitOfWork.SemesterRepository.GetSemesterBySemesterNo(currentSemester.ProgramId, currentSemester.SemesterNo - 1);
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

            List<Student> activeStudentsOfBatch = new List<Student>();
            List<StudentSemester> addedStudentSemestersInSemester = new List<StudentSemester>();
            List<Student> addedStudentsInSemester = new List<Student>();
            List<Student> remainStudents = new List<Student>();
            List<Student> eligibleStudents = new List<Student>();
            Semester previousSemester = new Semester();

            // for first semester, return active students of batch
            if (semester.SemesterNo == 1)
            {
                activeStudentsOfBatch = unitOfWork.StudentRepository.GetActiveStudentsOfBatch(batchId).ToList();

                addedStudentSemestersInSemester = 
                    unitOfWork.StudentSemesterRepository.
                        GetStudentSemestersForSemester(batchId, semesterId).ToList();

                foreach (var studentSemester in addedStudentSemestersInSemester)
                {
                    addedStudentsInSemester.Add(
                        unitOfWork.StudentRepository.GetStudentById(studentSemester.StudentId));
                }

                foreach (var student in activeStudentsOfBatch)
                {
                    if (addedStudentsInSemester.Contains(student))
                    {
                        continue;
                    }
                    remainStudents.Add(student);
                }

                return remainStudents;
            }

            else if (semester.SemesterNo > 1)
            {
                previousSemester = unitOfWork.SemesterRepository.
                    GetSemesterBySemesterNo(program.Id, semester.SemesterNo - 1);

                // student semesters of previous semester
                IEnumerable<StudentSemester> studentSemestersOfPreviousSemester =
                    unitOfWork.StudentSemesterRepository.
                        GetStudentSemestersForSemester(batchId, previousSemester.Id);

                // students of previous semester
                List<Student> studentsOfPreviousSemester = new List<Student>();
                foreach (var item in studentSemestersOfPreviousSemester)
                {
                    studentsOfPreviousSemester.Add(
                        unitOfWork.StudentRepository.GetStudentById(item.StudentId));
                }

                // active students of previous semester
                List<Student> activeStudentsOfPreviousSemester = new List<Student>();
                foreach (var item in studentsOfPreviousSemester)
                {
                    var user = unitOfWork.UserRepository.GetUserById(item.UserId);
                    if (user.Status == "Active")
                    {
                        activeStudentsOfPreviousSemester.Add(item);
                    }
                }

                // added student semesters of semester
                addedStudentSemestersInSemester =
                    unitOfWork.StudentSemesterRepository.
                        GetStudentSemestersForSemester(batchId, semesterId).ToList();

                // added students of semester
                foreach (var studentSemester in addedStudentSemestersInSemester)
                {
                    addedStudentsInSemester.Add(
                        unitOfWork.StudentRepository.GetStudentById(studentSemester.StudentId));
                }

                // remain students of previous semester
                foreach (var student in activeStudentsOfPreviousSemester)
                {
                    if (addedStudentsInSemester.Contains(student))
                    {
                        continue;
                    }
                    remainStudents.Add(student);
                }

                // for PGDIT and MIT, return remain students
                if (program.ProgramName == "PGDIT" || program.ProgramName == "MIT")
                {
                    return remainStudents;
                }

                // for BSSE and MSSE, check for pass / fail in previous semester
                else if (program.ProgramName == "BSSE" || program.ProgramName == "MSSE")
                {
                    // passed student semesters of previous semester
                    IEnumerable<StudentSemester> passedStudentSemestersOfPreviousSemester =
                        unitOfWork.StudentSemesterRepository.
                            GetSemesterPassedStudents(batchId, previousSemester.Id);

                    // passed students of previous semester
                    List<Student> passedStudentsOfPreviousSemester = new List<Student>();
                    foreach (var item in passedStudentSemestersOfPreviousSemester)
                    {
                        passedStudentsOfPreviousSemester.Add(
                            unitOfWork.StudentRepository.GetStudentById(item.StudentId));
                    }

                    // eligible students for semester
                    foreach (var item in remainStudents)
                    {
                        if (passedStudentsOfPreviousSemester.Contains(item))
                        {
                            eligibleStudents.Add(item);
                        }
                    }

                    return eligibleStudents;
                }
            }

            return eligibleStudents;

            /*
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
            }*/
        }

        public IEnumerable<Student> GetEligibleStudentsForCourse(int batchId, int semesterId, int courseId)
        {
            var course = unitOfWork.CourseRepository.GetCourseById(courseId);
            var semesterStudents = GetSemesterStudents(batchId, semesterId);

            var addedStudents = GetCourseStudents(batchId, semesterId, courseId);

            List<Student> remainStudents = new List<Student>();
            foreach (var student in semesterStudents)
            {
                if (addedStudents.Contains(student))
                {
                    continue;
                }
                remainStudents.Add(student);
            }

            // If there is no dependent course, then return the semester students
            if (course.DependentCourseId1 == 0 && course.DependentCourseId2 == 0)
            {
                return remainStudents;
            }

            // Check whether the students has passed the dependent courses
            // First check in current batch for dependent courses
            // If not found, then check in previous batch
            // If not found, then check in previous previous batch

            Batch batch = unitOfWork.BatchRepository.GetBatchById(batchId);
            Batch previousBatch = null;
            Batch previousPreviousBatch = null;

            if (batch.BatchNo > 1)
                previousBatch = unitOfWork.BatchRepository.GetBatchByBatchNo(batch.ProgramId, batch.BatchNo - 1);
            if(batch.BatchNo > 2)
                previousPreviousBatch = unitOfWork.BatchRepository.GetBatchByBatchNo(batch.ProgramId, batch.BatchNo - 2);

            List<Student> students = new List<Student>();
            Course dependentCourse1 = unitOfWork.CourseRepository.GetCourseById(course.DependentCourseId1);
            Course dependentCourse2 = null;

            if (course.DependentCourseId2 != 0)
                dependentCourse2 = unitOfWork.CourseRepository.GetCourseById(course.DependentCourseId2);

            bool flag;
            StudentCourse studentCourse1 = null;
            StudentCourse studentCourse2 = null;

            foreach (var student in remainStudents)
            {
                // check student course in current batch
                studentCourse1 =
                    unitOfWork.StudentCourseRepository.GetStudentCourse(batchId, student.Id, dependentCourse1.Id);

                // check student course in previous batch
                if (studentCourse1 == null && previousBatch != null)
                {
                    studentCourse1 =
                        unitOfWork.StudentCourseRepository.GetStudentCourse(previousBatch.Id, student.Id, dependentCourse1.Id);
                }

                // check student course in previous previous batch
                if (studentCourse1 == null && previousPreviousBatch != null)
                {
                    studentCourse1 =
                        unitOfWork.StudentCourseRepository.GetStudentCourse(previousPreviousBatch.Id, student.Id, dependentCourse1.Id);
                }

                if (dependentCourse2 != null)
                {
                    studentCourse2 =
                        unitOfWork.StudentCourseRepository.GetStudentCourse(batchId, student.Id, dependentCourse2.Id);
                    if (studentCourse2 == null && previousBatch != null)
                    {
                        studentCourse2 =
                            unitOfWork.StudentCourseRepository.GetStudentCourse(previousBatch.Id, student.Id, dependentCourse2.Id);
                    }
                    if (studentCourse2 == null && previousPreviousBatch != null)
                    {
                        studentCourse2 =
                            unitOfWork.StudentCourseRepository.GetStudentCourse(previousPreviousBatch.Id, student.Id, dependentCourse2.Id);
                    }
                }

                flag = false;

                if (studentCourse1 != null && studentCourse1.GradePoint >= 2.00)
                {
                    flag = true;
                    if (studentCourse2 != null && studentCourse2.GradePoint < 2.00)
                        flag = false;
                }

                if (flag)
                    students.Add(student);
            }

            return students;
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

        public IEnumerable<Student> GetSemesterPassedStudents(int batchId, int semesterId)
        {
            var studentSemesters =
                unitOfWork.StudentSemesterRepository.GetSemesterPassedStudents(batchId, semesterId);
            List<Student> students = new List<Student>();
            foreach (var item in studentSemesters)
            {
                students.Add(unitOfWork.StudentRepository.GetStudentById(item.StudentId));
            }
            return students;
        }

        public IEnumerable<Student> GetSemesterFailedStudents(int batchId, int semesterId)
        {
            var batch = unitOfWork.BatchRepository.GetBatchById(batchId);
            var semester = unitOfWork.SemesterRepository.GetSemesterById(semesterId);
            //var batchCurrentSemester = unitOfWork.SemesterRepository.GetSemesterById(batch.SemesterIdCurrent);

            List<Student> students = new List<Student>();
            List<StudentSemester> studentSemesters = new List<StudentSemester>();
            //if (semester.SemesterNo < batchCurrentSemester.SemesterNo)
            //{
                studentSemesters = 
                    unitOfWork.StudentSemesterRepository.GetSemesterFailedStudents(batchId, semesterId).ToList();

                foreach (var item in studentSemesters)
                {
                    students.Add(unitOfWork.StudentRepository.GetStudentById(item.StudentId));
                }
            //}
            
            return students;
        }

        public double GetSemesterGpaOfStudent(int batchId, int semesterId, int studentId)
        {
            StudentSemester data = unitOfWork.StudentSemesterRepository.GetStudentSemester(batchId, semesterId, studentId);
            return data.GPA;
        }

        public IEnumerable<Student> GetCourseStudents(int batchId, int semesterId, int courseId)
        {
            var courseStudents =
                unitOfWork.StudentCourseRepository.GetCourseStudents(batchId, semesterId, courseId);
            List<Student> students = new List<Student>();
            foreach (var item in courseStudents)
            {
                students.Add(unitOfWork.StudentRepository.GetStudentById(item.StudentId));
            }
            return students;
        }

        public int GetCourseStudentsNumber(int batchId, int semesterId, int courseId)
        {
            return unitOfWork.StudentCourseRepository.GetCourseStudents(batchId, semesterId, courseId).Count();
        }

        public IEnumerable<ApplicationUser> GetCourseTeachers(int batchId, int semesterId, int courseId)
        {
            var courseSemesterTeachers =
                unitOfWork.CourseSemesterRepository.GetCourseTeachers(batchId, semesterId, courseId);
            List<ApplicationUser> teachers = new List<ApplicationUser>();
            foreach (var item in courseSemesterTeachers)
            {
                teachers.Add(unitOfWork.UserRepository.GetUserById(item.TeacherId));
            }
            return teachers;
        }

        public int GetCourseTeachersNumber(int batchId, int semesterId, int courseId)
        {
            return unitOfWork.CourseSemesterRepository.GetCourseTeachers(batchId, semesterId, courseId).Count();
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

        public bool AddStudentToStudentCourse(StudentCourse studentCourse)
        {
            try
            {
                unitOfWork.StudentCourseRepository.AddStudentCourse(studentCourse);
                unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("", "Unable to save, please try again.");
                return false;
            }
        }

        public bool RemoveStudentFromStudentCourse(StudentCourse studentCourse)
        {
            StudentCourse studentCourseToRemove =
                unitOfWork.StudentCourseRepository.GetStudentCourse(
                    studentCourse.BatchId, studentCourse.SemesterId,
                    studentCourse.StudentId, studentCourse.CourseId);
            try
            {
                unitOfWork.StudentCourseRepository.RemoveStudentCourse(studentCourseToRemove);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public bool RemoveStudentsFromStudentCourse(StudentCourse studentCourse)
        {
            var studentCourses =
                unitOfWork.StudentCourseRepository.GetStudentCoursesOfStudent(studentCourse.BatchId,
                    studentCourse.SemesterId, studentCourse.StudentId);

            try
            {
                foreach (var item in studentCourses)
                {
                    unitOfWork.StudentCourseRepository.RemoveStudentCourse(item);
                }
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public bool RemoveCoursesFromStudentCourse(StudentCourse studentCourse)
        {
            var items =
                unitOfWork.StudentCourseRepository.GetCourseStudents(studentCourse.BatchId,
                        studentCourse.SemesterId, studentCourse.CourseId);

            try
            {
                foreach (var item in items)
                {
                    unitOfWork.StudentCourseRepository.RemoveStudentCourse(item);
                }
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }

        public bool AddTeacherToCourseSemester(CourseSemester courseSemester)
        {
            CourseSemester existed = unitOfWork.CourseSemesterRepository.GetCourseSemester(
                courseSemester.BatchId, courseSemester.SemesterId, courseSemester.CourseId);

            if (existed == null)
            {
                modelState.AddModelError("", "Unable to save, please try again.");
                return false;
            }

            // if teacher not set, set teacher
            if (existed.TeacherId == null)
            {
                existed.TeacherId = courseSemester.TeacherId;
                try
                {
                    unitOfWork.CourseSemesterRepository.EditCourseSemester(existed);
                    unitOfWork.Save();
                    return true;
                }
                catch (Exception)
                {
                    modelState.AddModelError("", "Unable to save, please try again.");
                    return false;
                }
            }
            // if teacher is set, add new entry in course semester
            else
            {
                try
                {
                    unitOfWork.CourseSemesterRepository.CreateCourseSemester(courseSemester);
                    unitOfWork.Save();
                    return true;
                }
                catch (Exception)
                {
                    modelState.AddModelError("", "Unable to save, please try again.");
                    return false;
                }
            }
        }

        public bool RemoveTeacherFromCourseSemester(CourseSemester courseSemester)
        {
            CourseSemester courseSemesterToRemove = unitOfWork.CourseSemesterRepository.GetCourseSemester(
                courseSemester.BatchId, courseSemester.SemesterId,
                courseSemester.CourseId, courseSemester.TeacherId);

            var courseSemesters = unitOfWork.CourseSemesterRepository.GetCourseSemestersOfCourse(
                courseSemester.BatchId, courseSemester.SemesterId, courseSemester.CourseId);

            // If number of entry is 1, make teacherid null
            if (courseSemesters.Count() == 1)
            {
                courseSemesterToRemove.TeacherId = null;
                try
                {
                    unitOfWork.CourseSemesterRepository.EditCourseSemester(courseSemesterToRemove);
                    unitOfWork.Save();
                    return true;
                }
                catch (DataException)
                {
                    modelState.AddModelError("", "Unable to save, try again.");
                    return false;
                }
            }
            else if (courseSemesters.Count() > 1)
            {
                try
                {
                    unitOfWork.CourseSemesterRepository.DeleteCourseSemester(courseSemesterToRemove);
                    unitOfWork.Save();
                    return true;
                }
                catch (DataException)
                {
                    modelState.AddModelError("", "Unable to save, try again.");
                    return false;
                }
            }
            return false;
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
            var courseSemesterToRemove =
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

        public bool RemoveCoursesFromSemester(CourseSemester courseSemester)
        {
            var courseSemestersToRemove =
                unitOfWork.CourseSemesterRepository.GetCourseSemestersOfCourse(
                    courseSemester.BatchId, courseSemester.SemesterId, courseSemester.CourseId);
            try
            {
                foreach (var item in courseSemestersToRemove)
                {
                    unitOfWork.CourseSemesterRepository.RemoveCourseFromSemester(item);
                }
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

        public StudentSemester GetStudentSemester(int batchId, int semesterId, int studentId)
        {
            return unitOfWork.StudentSemesterRepository.GetStudentSemester(batchId, semesterId, studentId);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}