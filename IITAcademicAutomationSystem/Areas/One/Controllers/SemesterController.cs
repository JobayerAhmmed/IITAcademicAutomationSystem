using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class SemesterController : Controller
    {
        private ApplicationUserManager _userManager;
        private IProgramService programService;
        private ISemesterService semesterService;
        private IBatchService batchService;
        private ICourseService courseService;
        private IStudentService studentService;
        private IUserService userService;
        private IRoleService roleService;
        private IMapper Mapper;

        public SemesterController()
        {
            semesterService = new SemesterService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            batchService = new BatchService(ModelState, new DAL.UnitOfWork());
            courseService = new CourseService(ModelState, new DAL.UnitOfWork());
            studentService = new StudentService(ModelState, new DAL.UnitOfWork());
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
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
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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

        // Semester Studet Details
        public ActionResult SemesterStudentDetails(int studentId, int batchId, int semesterId)
        {
            var student = studentService.ViewStudent(studentId);
            var user = UserManager.FindById(student.UserId);
            var program = programService.ViewProgram(student.ProgramId);
            var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
            var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);
            var studentSemester = semesterService.ViewSemester(student.SemesterId);
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);

            SemesterStudentDetailsViewModel model = new SemesterStudentDetailsViewModel();
            model.AdmissionSession = student.AdmissionSession;
            model.BatchNoCurrent = batchCurrent.BatchNo;
            model.BatchNoOriginal = batchOriginal.BatchNo;
            model.CurrentAddress = student.CurrentAddress;
            model.CurrentRoll = student.CurrentRoll;
            model.CurrentSession = student.CurrentSession;
            model.Designation = user.Designation;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.GuardianPhone = student.GuardianPhone;
            model.ImagePath = user.ImagePath;
            model.OriginalRoll = student.OriginalRoll;
            model.PermanentAddress = student.PermanentAddress;
            model.PhoneNumber = user.PhoneNumber;
            model.ProgramName = program.ProgramName;
            model.RegistrationNo = student.RegistrationNo;
            model.SemesterNo = semester.SemesterNo;
            model.Status = user.Status;

            model.ProgramId = semester.ProgramId;
            model.ProgramNameNav = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNoNav = semester.SemesterNo;

            return View(model);
        }

        // Semester Passed Students
        public ActionResult SemesterPassedStudents(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var batchCurrentSemester = semesterService.ViewSemester(batch.SemesterIdCurrent);
            var program = programService.ViewProgram(batch.ProgramId);
            IEnumerable<Student> students = semesterService.GetSemesterPassedStudents(batchId, semesterId);

            List<StudentResultViewModel> studentsToView = new List<StudentResultViewModel>();
            StudentResultViewModel studentIndexViewModel = new StudentResultViewModel();
            ApplicationUser user;
            double gpa;

            foreach (var item in students)
            {
                user = userService.ViewUser(item.UserId);
                gpa = semesterService.GetSemesterGpaOfStudent(batchId, semesterId, item.Id);

                studentIndexViewModel.Id = item.Id;
                studentIndexViewModel.UserId = item.UserId;
                studentIndexViewModel.Roll = item.CurrentRoll;
                studentIndexViewModel.FullName = user.FullName;
                studentIndexViewModel.Email = user.Email;
                studentIndexViewModel.PhoneNumber = user.PhoneNumber;
                studentIndexViewModel.ImagePath = user.ImagePath;
                studentIndexViewModel.GPA = gpa;

                studentsToView.Add(studentIndexViewModel);
                studentIndexViewModel = new StudentResultViewModel();
            }

            SemesterPassedStudentIndexViewModel model = new SemesterPassedStudentIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Students = studentsToView.OrderBy(s => s.Roll);

            return View(model);
        }

        // Semester failed Students
        public ActionResult SemesterFailedStudents(int batchId, int semesterId)
        {
            var semester = semesterService.ViewSemester(semesterId);
            var batch = batchService.ViewBatch(batchId);
            var program = programService.ViewProgram(batch.ProgramId);

            var students = semesterService.GetSemesterFailedStudents(batchId, semesterId);

            List<StudentResultViewModel> studentsToView = new List<StudentResultViewModel>();
            StudentResultViewModel studentIndexViewModel = new StudentResultViewModel();
            ApplicationUser user;
            double gpa;

            foreach (var item in students)
            {
                user = userService.ViewUser(item.UserId);
                gpa = semesterService.GetSemesterGpaOfStudent(batchId, semesterId, item.Id);

                studentIndexViewModel.Id = item.Id;
                studentIndexViewModel.UserId = item.UserId;
                studentIndexViewModel.Roll = item.CurrentRoll;
                studentIndexViewModel.FullName = user.FullName;
                studentIndexViewModel.Email = user.Email;
                studentIndexViewModel.PhoneNumber = user.PhoneNumber;
                studentIndexViewModel.ImagePath = user.ImagePath;
                studentIndexViewModel.GPA = gpa;

                studentsToView.Add(studentIndexViewModel);
                studentIndexViewModel = new StudentResultViewModel();
            }

            SemesterPassedStudentIndexViewModel model = new SemesterPassedStudentIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.Students = studentsToView.OrderBy(s => s.Roll);

            return View(model);
        }

        // Assign failed students to new batch
        public ActionResult AssignNewBatch(int batchId, int semesterId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);

            var students = semesterService.GetSemesterFailedStudents(batchId, semesterId);

            AddFailedStudentsNewBatchViewModel model = new AddFailedStudentsNewBatchViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            ApplicationUser user;
            foreach (var student in students)
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

            var batches = batchService.GetNextBatches(batchId);
            model.Batches = batches;

            var semesters = semesterService.GetSemestersOfProgram(program.Id);
            model.Semesters = semesters;

            return View(model);
        }

        // POST: Assign failed students to new batch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignNewBatch(AddFailedStudentsNewBatchViewModel model)
        {
            StudentSemester studentSemester;
            StudentSemester existedStudentSemester;
            Student studentToUpdate;
            bool result1, result2;
            foreach (var student in model.Students)
            {
                if (student.IsChecked)
                {
                    studentSemester = new StudentSemester()
                    {
                        SemesterId = model.SemesterIdAssigned,
                        BatchId = model.BatchIdAssigned,
                        StudentId = Convert.ToInt32(student.Value)
                    };
                    existedStudentSemester = semesterService.GetStudentSemester(
                        model.BatchIdAssigned, model.SemesterIdAssigned, Convert.ToInt32(student.Value));

                    result1 = true;
                    result2 = true;
                    if (existedStudentSemester == null)
                    {
                        studentToUpdate = studentService.ViewStudent(Convert.ToInt32(student.Value));
                        studentToUpdate.BatchIdCurrent = model.BatchIdAssigned;
                        studentToUpdate.SemesterId = model.SemesterIdAssigned;
                        result1 = semesterService.AddStudentToStudentSemester(studentSemester);
                        result2 = studentService.EditStudent(studentToUpdate);
                    }

                    if (result1 && result2)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("SemesterStudents", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId });
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
            Student studentToUpdate;
            bool result1, result2;

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

                    result1 = true;
                    result2 = true;

                    studentToUpdate = studentService.ViewStudent(Convert.ToInt32(student.Value));
                    studentToUpdate.BatchIdCurrent = model.BatchId;
                    studentToUpdate.SemesterId = model.SemesterId;

                    result1 = semesterService.AddStudentToStudentSemester(studentSemester);
                    result2 = studentService.EditStudent(studentToUpdate);

                    if (result1 && result2)
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

        // Course Teachers
        public ActionResult CourseTeachers(int batchId, int semesterId, int courseId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            IEnumerable<ApplicationUser> teachers = semesterService.GetCourseTeachers(batchId, semesterId, courseId);

            List<TeacherIndexViewModel> teachersToView = new List<TeacherIndexViewModel>();
            TeacherIndexViewModel teacherIndexViewModel = new TeacherIndexViewModel();

            foreach (var item in teachers)
            {
                teacherIndexViewModel.Id = item.Id;
                teacherIndexViewModel.FullName = item.FullName;
                teacherIndexViewModel.Designation = item.Designation;
                teacherIndexViewModel.Email = item.Email;
                teacherIndexViewModel.ImagePath = item.ImagePath;

                teachersToView.Add(teacherIndexViewModel);
                teacherIndexViewModel = new TeacherIndexViewModel();
            }

            CourseTeacherViewModel model = new CourseTeacherViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.CourseId = courseId;
            model.CourseCode = course.CourseCode;
            model.Teachers = teachersToView.OrderBy(s => s.FullName);

            return View(model);
        }

        // Add Teacher to Course
        public ActionResult AddTeacherToCourse(int batchId, int semesterId, int courseId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            var teacherRoleId = roleService.GetRoleByName("Teacher").Id;

            var teachers = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == teacherRoleId)
                && u.Status == "On Duty").OrderBy(o => o.FullName).ToList();

            var addedTeachers = semesterService.GetCourseTeachers(batchId, semesterId, courseId);
            List<ApplicationUser> remainTeachers = new List<ApplicationUser>();
            foreach (ApplicationUser item in teachers)
            {
                if (addedTeachers.Any(d => d.Id == item.Id))
                {
                    continue;
                }
                remainTeachers.Add(item);
            }

            AddCourseTeacherViewModel model = new AddCourseTeacherViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.CourseId = courseId;
            model.CourseCode = course.CourseCode;

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var teacher in remainTeachers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = teacher.Id,
                    Text = teacher.FullName,
                    IsChecked = false
                });
            }
            model.Teachers = checkBoxListItems;

            return View(model);
        }

        // POST: Add teacher to course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacherToCourse(AddCourseTeacherViewModel model)
        {
            CourseSemester courseSemester;
            bool result;
            foreach (var teacher in model.Teachers)
            {
                if (teacher.IsChecked)
                {
                    courseSemester = new CourseSemester()
                    {
                        BatchId = model.BatchId,
                        SemesterId = model.SemesterId,
                        CourseId = model.CourseId,
                        TeacherId = teacher.Value
                    };
                    result = semesterService.AddTeacherToCourseSemester(courseSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("CourseTeachers", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId, courseId = model.CourseId });
        }

        // Remove Teacher from Course
        public ActionResult RemoveTeacherFromCourse(int batchId, int semesterId, int courseId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            var addedTeachers = semesterService.GetCourseTeachers(batchId, semesterId, courseId);

            AddCourseTeacherViewModel model = new AddCourseTeacherViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;
            model.CourseId = courseId;
            model.CourseCode = course.CourseCode;

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var teacher in addedTeachers)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = teacher.Id,
                    Text = teacher.FullName,
                    IsChecked = false
                });
            }
            model.Teachers = checkBoxListItems;

            return View(model);
        }

        // POST: Remove Teacher from Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveTeacherFromCourse(AddCourseTeacherViewModel model)
        {
            CourseSemester courseSemester;
            bool result;
            foreach (var teacher in model.Teachers)
            {
                if (teacher.IsChecked)
                {
                    courseSemester = new CourseSemester()
                    {
                        BatchId = model.BatchId,
                        SemesterId = model.SemesterId,
                        CourseId = model.CourseId,
                        TeacherId = teacher.Value
                    };
                    result = semesterService.RemoveTeacherFromCourseSemester(courseSemester);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("CourseTeachers", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId, courseId = model.CourseId });
        }

        // Remove Student from Course
        public ActionResult RemoveStudentFromCourse(int batchId, int semesterId, int courseId)
        {
            var batch = batchService.ViewBatch(batchId);
            var semester = semesterService.ViewSemester(semesterId);
            var program = programService.ViewProgram(batch.ProgramId);
            var course = courseService.ViewCourse(courseId);

            var addedStudents = semesterService.GetCourseStudents(batchId, semesterId, courseId);

            SemesterAddStudentViewModel model = new SemesterAddStudentViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterId = semesterId;
            model.SemesterNo = semester.SemesterNo;

            var checkBoxListItems = new List<CheckBoxListItem>();
            ApplicationUser user;
            foreach (var student in addedStudents)
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

        // POST: Remove student from Course
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveStudentFromCourse(SemesterAddStudentViewModel model, int courseId)
        {
            StudentCourse studentCourse;
            bool result;
            foreach (var student in model.Students)
            {
                if (student.IsChecked)
                {
                    studentCourse = new StudentCourse()
                    {
                        BatchId = model.BatchId,
                        SemesterId = model.SemesterId,
                        CourseId = courseId,
                        StudentId = Convert.ToInt32(student.Value)
                    };
                    result = semesterService.RemoveStudentFromStudentCourse(studentCourse);

                    if (result)
                        continue;

                    return View(model);
                }
            }

            return RedirectToAction("CourseStudents", "Semester",
                new { area = "One", batchId = model.BatchId, semesterId = model.SemesterId, courseId = courseId });
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