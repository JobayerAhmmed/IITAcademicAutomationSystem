using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class SemesterController : Controller
    {
        private IProgramService programService;
        private ISemesterService semesterService;
        private IBatchService batchService;
        private ICourseService courseService;
        private IStudentService studentService;
        private IUserService userService;
        private IMapper Mapper;

        public SemesterController()
        {
            semesterService = new SemesterService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            batchService = new BatchService(ModelState, new DAL.UnitOfWork());
            courseService = new CourseService(ModelState, new DAL.UnitOfWork());
            studentService = new StudentService(ModelState, new DAL.UnitOfWork());
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public SemesterController(ISemesterService semesterService)
        {
            this.semesterService = semesterService;
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            batchService = new BatchService(ModelState, new DAL.UnitOfWork());
            courseService = new CourseService(ModelState, new DAL.UnitOfWork());
            studentService = new StudentService(ModelState, new DAL.UnitOfWork());
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        // Index
        public ActionResult Index(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);
            IEnumerable<Course> courses = semesterService.GetSemesterCourses(batchId, semesterId);
            IEnumerable<Student> students = semesterService.GetSemesterStudents(batchId, semesterId);
            IEnumerable<ApplicationUser> teachers = semesterService.GetSemesterTeachers(batchId, semesterId);

            SemesterIndexViewModel model = new SemesterIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.NumberOfOfferedCourses = courses.Count();
            model.NumberOfEnrolledStudents = students.Count();
            model.NumberOfCourseTeachers = teachers.Count();

            return View(model);
        }

        // Semester Students
        public ActionResult SemesterStudents(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);
            IEnumerable<Student> students = semesterService.GetSemesterStudents(batchId, semesterId);

            List<StudentIndexViewModel> studentsToView = new List<StudentIndexViewModel>();
            StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel();
            ApplicationUser user;

            foreach (var item in students)
            {
                user = userService.ViewUser(item.UserId);

                studentIndexViewModel.Id = item.Id;
                studentIndexViewModel.UserId = item.UserId;
                studentIndexViewModel.Roll = item.CurrentRoll;
                studentIndexViewModel.FullName = user.FullName;
                studentIndexViewModel.Email = user.Email;
                studentIndexViewModel.PhoneNumber = user.PhoneNumber;
                studentIndexViewModel.ImagePath = user.ImagePath;

                studentsToView.Add(studentIndexViewModel);
                studentIndexViewModel = new StudentIndexViewModel();
            }

            SemesterIndexViewModel model = new SemesterIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Students = studentsToView.OrderBy(s => s.Roll);

            return View(model);
        }

        // Course Students
        public ActionResult CourseStudents(int batchId, int semesterId, int courseId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            IEnumerable<Student> students = semesterService.GetCourseStudents(batchId, semesterId, courseId);

            List<StudentIndexViewModel> studentsToView = new List<StudentIndexViewModel>();
            StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel();
            ApplicationUser user;

            foreach (var item in students)
            {
                user = userService.ViewUser(item.UserId);

                studentIndexViewModel.Id = item.Id;
                studentIndexViewModel.UserId = item.UserId;
                studentIndexViewModel.Roll = item.CurrentRoll;
                studentIndexViewModel.FullName = user.FullName;
                studentIndexViewModel.Email = user.Email;
                studentIndexViewModel.PhoneNumber = user.PhoneNumber;
                studentIndexViewModel.ImagePath = user.ImagePath;

                studentsToView.Add(studentIndexViewModel);
                studentIndexViewModel = new StudentIndexViewModel();
            }

            SemesterIndexViewModel model = new SemesterIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Students = studentsToView.OrderBy(s => s.Roll);

            ViewBag.CourseId = course.Id;
            ViewBag.CourseCode = course.CourseCode;

            return View(model);
        }

        // Add Student to Course
        public ActionResult AddStudentToCourse(int batchId, int semesterId, int courseId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            var eligibleStudents = semesterService.GetEligibleStudentsForCourse(batchId, semesterId, courseId);

            SemesterAddStudentViewModel model = new SemesterAddStudentViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            ApplicationUser user;
            foreach (var student in eligibleStudents)
            {
                user = userService.ViewUser(student.UserId);
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = (student.Id).ToString(),
                    Text = student.CurrentRoll + " - " + user.FullName,
                    IsChecked = false
                });
            }
            model.Students = checkBoxListItems;

            ViewBag.CourseId = courseId;
            ViewBag.CourseCode = course.CourseCode;

            return View(model);
        }

        // POST: Add student to course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentToCourse(SemesterAddStudentViewModel model, int courseId)
        {
            StudentCourse studentCourse;
            bool result;
            foreach (var student in model.Students)
            {
                if (student.IsChecked)
                {
                    studentCourse = new StudentCourse()
                    {
                        SemesterId = model.SemesterId,
                        CourseId = courseId,
                        BatchId = model.BatchId,
                        StudentId = Convert.ToInt32(student.Value)
                    };
                    result = semesterService.AddStudentToStudentCourse(studentCourse);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("CourseStudents", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId, courseId = courseId });
        }

        // Semester Courses
        public ActionResult SemesterCourses(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);
            IEnumerable<Course> courses = semesterService.GetSemesterCourses(batchId, semesterId);
            IEnumerable<CourseIndexViewModel> coursesToView =
                Mapper.Map<IEnumerable<Course>, IEnumerable<CourseIndexViewModel>>(courses);

            SemesterIndexViewModel model = new SemesterIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Courses = coursesToView;

            return View(model);
        }
        
        // Semester Teachers
        public ActionResult SemesterTeachers(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);
            IEnumerable<ApplicationUser> teachers = semesterService.GetSemesterTeachers(batchId, semesterId);

            IList<TeacherIndexViewModel> teachersToView = new List<TeacherIndexViewModel>();
            TeacherIndexViewModel teacherIndexViewModel = new TeacherIndexViewModel();

            foreach (var item in teachers)
            {
                teacherIndexViewModel.Id = item.Id;
                teacherIndexViewModel.FullName = item.FullName;
                teacherIndexViewModel.Designation = item.Designation;
                teacherIndexViewModel.Email = item.Email;
                //teacherIndexViewModel.PhoneNumber = item.PhoneNumber;
                //teacherIndexViewModel.ProfileLink = item.ProfileLink;
                teacherIndexViewModel.ImagePath = item.ImagePath;
                //teacherIndexViewModel.Status = item.Status;
                //teacherIndexViewModel.Roles = userService.GetUserRoles(item.Id);

                teachersToView.Add(teacherIndexViewModel);
                teacherIndexViewModel = new TeacherIndexViewModel();
            }

            SemesterIndexViewModel model = new SemesterIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Teachers = teachersToView;

            return View(model);
        }

        // Add student to Semester
        public ActionResult AddStudentToSemester(int batchId, int semesterId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);

            var eligibleStudents = semesterService.GetEligibleStudentsForSemester(batchId, semesterId);

            SemesterAddStudentViewModel model = new SemesterAddStudentViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            ApplicationUser user;
            foreach (var student in eligibleStudents)
            {
                user = userService.ViewUser(student.UserId);
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = (student.Id).ToString(),
                    Text = student.CurrentRoll +  " - " + user.FullName,
                    IsChecked = false
                });
            }
            model.Students = checkBoxListItems;

            return View(model);
        }

        // POST: Add student to Semester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudentToSemester(SemesterAddStudentViewModel model)
        {
            StudentSemester studentSemester;
            bool result;
            foreach (var student in model.Students)
            {
                if (student.IsChecked)
                {
                    studentSemester = new StudentSemester()
                    {
                        SemesterId = model.SemesterId,
                        BatchId = model.BatchId,
                        StudentId = Convert.ToInt32(student.Value)
                    };
                    result = semesterService.AddStudentToStudentSemester(studentSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId });
        }

        // Remove Student from semester
        public ActionResult RemoveStudentFromSemester(int batchId, int semesterId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);

            var addedStudentsInSemester = semesterService.GetSemesterStudents(batchId, semesterId);

            SemesterAddStudentViewModel model = new SemesterAddStudentViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            ApplicationUser user;
            foreach (var student in addedStudentsInSemester)
            {
                user = userService.ViewUser(student.UserId);
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = (student.Id).ToString(),
                    Text = student.CurrentRoll + " - " + user.FullName,
                    IsChecked = false
                });
            }
            model.Students = checkBoxListItems;

            return View(model);
        }

        // POST: Remove student from Semester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveStudentFromSemester(SemesterAddStudentViewModel model)
        {
            StudentSemester studentSemester;
            bool result;
            foreach (var student in model.Students)
            {
                if (student.IsChecked)
                {
                    studentSemester = new StudentSemester()
                    {
                        SemesterId = model.SemesterId,
                        BatchId = model.BatchId,
                        StudentId = Convert.ToInt32(student.Value)
                    };
                    result = semesterService.RemoveStudentFromStudentSemester(studentSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId });
        }

        // Allocate Courses to Semester
        public ActionResult AllocateCoursesToSemester(int batchId, int semesterId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var unallocatedCourses = semesterService.GetUnallocatedCoursesOfBatch(batchId);

            SemesterCourseAllocationViewModel model = new SemesterCourseAllocationViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var course in unallocatedCourses)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = (course.Id).ToString(),
                    Text = course.CourseCode + " - " + course.CourseTitle,
                    IsChecked = false
                });
            }
            model.Courses = checkBoxListItems;

            return View(model);
        }

        // POST: Allocate Courses to Semester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AllocateCoursesToSemester(SemesterCourseAllocationViewModel model)
        {
            CourseSemester courseSemester;
            bool result;
            foreach (var course in model.Courses)
            {
                if (course.IsChecked)
                {
                    courseSemester = new CourseSemester()
                    {
                        SemesterId = model.SemesterId,
                        BatchId = model.BatchId,
                        CourseId = Convert.ToInt32(course.Value)
                    };
                    result = semesterService.AddCourseToSemester(courseSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId });
        }

        // Remove Course from Semester
        public ActionResult RemoveCourseFromSemester(int batchId, int semesterId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);

            var allocatedCoursesOfSemester = semesterService.GetSemesterCourses(batchId, semesterId);

            SemesterCourseAllocationViewModel model = new SemesterCourseAllocationViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var course in allocatedCoursesOfSemester)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = (course.Id).ToString(),
                    Text = course.CourseCode + " - " + course.CourseTitle,
                    IsChecked = false
                });
            }
            model.Courses = checkBoxListItems;

            return View(model);
        }

        // POST: Remove Course from Semester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveCourseFromSemester(SemesterCourseAllocationViewModel model)
        {
            CourseSemester courseSemester;
            bool result;
            foreach (var course in model.Courses)
            {
                if (course.IsChecked)
                {
                    courseSemester = new CourseSemester()
                    {
                        SemesterId = model.SemesterId,
                        BatchId = model.BatchId,
                        CourseId = Convert.ToInt32(course.Value)
                    };
                    result = semesterService.RemoveCourseFromSemester(courseSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("Index", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId });
        }

        protected override void Dispose(bool disposing)
        {
            programService.Dispose();
            batchService.Dispose();
            semesterService.Dispose();
            courseService.Dispose();

            base.Dispose(disposing);
        }
    }
}