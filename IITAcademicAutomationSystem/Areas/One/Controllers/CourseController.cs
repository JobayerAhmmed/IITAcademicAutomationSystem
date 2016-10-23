using System.Collections.Generic;
using System.Web.Mvc;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using AutoMapper;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService courseService;
        private IMapper Mapper;
        public CourseController()
        {
            courseService = new CourseService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        //GET: One/Course
        public ActionResult Index(int programId)
        {
            IEnumerable<Course> courses = courseService.ViewCoursesOfProgram(programId);
            Program program = courseService.GetProgramById(programId);

            if (courses == null)
                return HttpNotFound();

            IEnumerable<CourseIndexViewModel> coursesToView =
                Mapper.Map<IEnumerable<Course>, IEnumerable<CourseIndexViewModel>>(courses);

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(coursesToView);
        }

        // GET: One/Course/Create
        public ActionResult Create(int programId)
        {
            Program program = courseService.GetProgramById(programId);

            CourseCreateViewModel courseCreateViewModel = new CourseCreateViewModel()
            {
                ProgramId = programId,
                Courses = courseService.ViewCoursesOfProgram(programId)
            };

            ViewBag.ProgramName = program.ProgramName;
            return View(courseCreateViewModel);
        }

        // POST: One/Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgramId,CourseCode,CourseTitle,CourseCredit,CreditTheory,CreditLab,DependentCourseId1,DependentCourseId2")]
            CourseCreateViewModel courseCreateViewModel)
        {
            Course courseToCreate = Mapper.Map<CourseCreateViewModel, Course>(courseCreateViewModel);
            Program program = courseService.GetProgramById(courseCreateViewModel.ProgramId);

            int courseId = courseService.CreateCourse(courseToCreate);

            if (courseId != -1)
            {
                CourseDetailsViewModel courseDetails = Mapper.Map<Course, CourseDetailsViewModel>(courseToCreate);

                Course dependentCourse1 = courseService.ViewCourse(courseCreateViewModel.DependentCourseId1);
                Course dependentCourse2 = courseService.ViewCourse(courseCreateViewModel.DependentCourseId2);

                if (dependentCourse1 != null)
                    courseDetails.DependentCourse1 = dependentCourse1.CourseCode;

                if (dependentCourse2 != null)
                    courseDetails.DependentCourse2 = dependentCourse2.CourseCode;

                ViewBag.ProgramName = program.ProgramName;
                return View("CreateConfirmed", courseDetails);
            }

            courseCreateViewModel.Courses = courseService.ViewCoursesOfProgram(courseCreateViewModel.ProgramId);
            ViewBag.ProgramName = program.ProgramName;
            return View(courseCreateViewModel);
        }

        // GET: One/Course/Edit/5
        public ActionResult Edit(int id)
        {
            Course course = courseService.ViewCourse(id);
            Program program = courseService.GetProgramById(course.ProgramId);

            if (course == null)
                return HttpNotFound();

            CourseEditViewModel courseEditViewModel = Mapper.Map<Course, CourseEditViewModel>(course);

            IEnumerable<Course> allCourses = courseService.ViewCoursesOfProgram(course.ProgramId);
            IList<Course> dependentCoursesToSelect = new List<Course>();
            foreach (Course item in allCourses)
            {
                if (item.Id != course.Id)
                    dependentCoursesToSelect.Add(item);
            }
            courseEditViewModel.Courses = dependentCoursesToSelect;

            ViewBag.ProgramName = program.ProgramName;
            return View(courseEditViewModel);
        }

        // POST: One/Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProgramId,CourseCode,CourseTitle,CourseCredit,CreditTheory,CreditLab,DependentCourseId1,DependentCourseId2,Courses")]
            CourseEditViewModel courseEditViewModel)
        {
            Course courseToEdit = Mapper.Map<CourseEditViewModel, Course>(courseEditViewModel);
            Program program = courseService.GetProgramById(courseEditViewModel.ProgramId);

            if (courseService.EditCourse(courseToEdit))
            {
                CourseDetailsViewModel courseDetails = Mapper.Map<Course, CourseDetailsViewModel>(courseToEdit);

                Course dependentCourse1 = courseService.ViewCourse(courseEditViewModel.DependentCourseId1);
                Course dependentCourse2 = courseService.ViewCourse(courseEditViewModel.DependentCourseId2);

                if (dependentCourse1 != null)
                    courseDetails.DependentCourse1 = dependentCourse1.CourseCode;

                if (dependentCourse2 != null)
                    courseDetails.DependentCourse2 = dependentCourse2.CourseCode;

                ViewBag.ProgramName = program.ProgramName;
                return View("EditConfirmed", courseDetails);
            }

            IEnumerable<Course> allCourses = courseService.ViewCoursesOfProgram(courseEditViewModel.ProgramId);
            IList<Course> dependentCoursesToSelect = new List<Course>();
            foreach (Course item in allCourses)
            {
                if (item.Id != courseEditViewModel.Id)
                    dependentCoursesToSelect.Add(item);
            }
            courseEditViewModel.Courses = dependentCoursesToSelect;

            ViewBag.ProgramName = program.ProgramName;
            return View(courseEditViewModel);
        }

        //GET: One/Course/Details/5
        public ActionResult Details(int id)
        {
            Course course = courseService.ViewCourse(id);
            Program program = courseService.GetProgramById(course.ProgramId);

            if (course == null)
                return HttpNotFound();

            CourseDetailsViewModel courseDetails = Mapper.Map<Course, CourseDetailsViewModel>(course);

            Course dependentCourse1 = courseService.ViewCourse(course.DependentCourseId1);
            Course dependentCourse2 = courseService.ViewCourse(course.DependentCourseId2);

            if (dependentCourse1 != null)
                courseDetails.DependentCourse1 = dependentCourse1.CourseCode;

            if (dependentCourse2 != null)
                courseDetails.DependentCourse2 = dependentCourse2.CourseCode;

            ViewBag.ProgramName = program.ProgramName;
            return View(courseDetails);
        }


        // GET: One/Course/Delete/5
        public ActionResult Delete(int id)
        {
            Course course = courseService.ViewCourse(id);
            Program program = courseService.GetProgramById(course.ProgramId);

            if (course == null)
                return HttpNotFound();

            CourseDetailsViewModel courseDetails = Mapper.Map<Course, CourseDetailsViewModel>(course);

            Course dependentCourse1 = courseService.ViewCourse(course.DependentCourseId1);
            Course dependentCourse2 = courseService.ViewCourse(course.DependentCourseId2);

            if (dependentCourse1 != null)
                courseDetails.DependentCourse1 = dependentCourse1.CourseCode;

            if (dependentCourse2 != null)
                courseDetails.DependentCourse2 = dependentCourse2.CourseCode;

            ViewBag.ProgramName = program.ProgramName;
            return View(courseDetails);
        }

        // POST: One/Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CourseDetailsViewModel course)
        {
            Program program = courseService.GetProgramById(course.ProgramId);
            if(courseService.DeleteCourse(course.Id))
            {
                ViewBag.ProgramId = course.ProgramId;
                ViewBag.ProgramName = program.ProgramName;
                return View("DeleteConfirmed");
            }
            ViewBag.ProgramName = program.ProgramName;
            return View(course);
        }

        // CheckExistingCourse
        public JsonResult CourseExist(string CourseCode, int ProgramId)
        {
            if (!courseService.CourseExist(CourseCode, ProgramId))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // CheckExistingCourse except current one
        public JsonResult CourseExistExcept(string CourseCode, int Id, int ProgramId)
        {
            if (!courseService.CourseExistExcept(CourseCode, Id, ProgramId))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            courseService.Dispose();
            base.Dispose(disposing);
        }
    }
}
