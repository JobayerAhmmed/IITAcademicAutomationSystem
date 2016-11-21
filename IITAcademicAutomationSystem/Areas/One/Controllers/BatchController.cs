using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class BatchController : Controller
    {
        private ApplicationUserManager _userManager;
        private IProgramService programService;
        private IBatchService batchService;
        private ISemesterService semesterService;
        private IUserService userService;
        private IRoleService roleService;
        private IStudentService studentService;
        private IMapper Mapper;
        public BatchController()
        {
            programService = new ProgramService(ModelState, new UnitOfWork());
            batchService = new BatchService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            roleService = new RoleService(ModelState, new UnitOfWork());
            studentService = new StudentService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public BatchController(IBatchService batchService)
        {
            this.batchService = batchService;
            programService = new ProgramService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            roleService = new RoleService(ModelState, new UnitOfWork());
            studentService = new StudentService(ModelState, new UnitOfWork());
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Teacher, Student")]
        public ActionResult Index(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewActiveBatches(programId);
            List<Batch> studentBatches = new List<Batch>();
            List<Batch> remainBatches = new List<Batch>();

            if (User.IsInRole("Student"))
            {
                var userId = User.Identity.GetUserId();
                var student = studentService.GetStudentByUserId(userId);
                studentBatches = batchService.GetStudentBatches(student.Id).ToList();

                foreach (var item in studentBatches)
                {
                    remainBatches.Add(item);
                }
            }
            else
            {
                foreach (var item in batches)
                {
                    remainBatches.Add(item);
                }
            }
            
            Program program = programService.ViewProgram(programId);
            IEnumerable<Semester> semesters = semesterService.GetSemestersOfProgram(programId);

            IList<BatchIndexViewModel> model = new List<BatchIndexViewModel>();
            BatchIndexViewModel batchIndexViewModel = new BatchIndexViewModel();
            Semester currentSemester;

            foreach (var item in remainBatches)
            {
                currentSemester = semesterService.ViewSemester(item.SemesterIdCurrent);
                batchIndexViewModel.Id = item.Id;
                batchIndexViewModel.BatchNo = item.BatchNo;
                batchIndexViewModel.SemesterNoCurrent = currentSemester.SemesterNo;
                batchIndexViewModel.Status = item.BatchStatus;
                batchIndexViewModel.Semesters = semesters;

                model.Add(batchIndexViewModel);
                batchIndexViewModel = new BatchIndexViewModel();
            }

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(model);
        }

        // IndexPassed
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Teacher")]
        public ActionResult IndexPassed(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewPassedBatches(programId);
            Program program = programService.ViewProgram(programId);

            if (batches == null)
                return HttpNotFound();

            IList<BatchIndexViewModel> batchesToView = new List<BatchIndexViewModel>();

            foreach (var item in batches)
            {
                batchesToView.Add(new BatchIndexViewModel()
                {
                    Id = item.Id,
                    BatchNo = item.BatchNo
                });
            }

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(batchesToView);
        }

        // Create
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult Create(int programId)
        {
            Program program = programService.ViewProgram(programId);
            Batch previousBatch = batchService.GetPreviousBatch(programId);
            int batchNo = 0;
            if (previousBatch != null)
                batchNo = previousBatch.BatchNo;

            BatchCreateViewModel batchCreateVM = new BatchCreateViewModel()
            {
                ProgramId = programId,
                ProgramName = program.ProgramName,
                BatchNo = batchNo + 1
            };

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(batchCreateVM);
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult Create([Bind(Include = "ProgramId,ProgramName,BatchNo")]BatchCreateViewModel batchCreateVM)
        {
            var firstSemester = semesterService.GetFirstSemester(batchCreateVM.ProgramId);
            Batch batchToCreate = new Batch()
            {
                ProgramId = batchCreateVM.ProgramId,
                BatchNo = batchCreateVM.BatchNo,
                SemesterIdCurrent = firstSemester.Id,
                BatchStatus = "Active"
            };

            if (batchService.CreateBatch(batchToCreate))
            {
                return View("CreateConfirmed", batchCreateVM);
            }

            return View();
        }

        // Details
        //[Authorize(Roles = "Program Officer Regular")]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult Details(int id)
        {
            Batch batch = batchService.ViewBatch(id);
            Program program = programService.ViewProgram(batch.ProgramId);
            Semester semester = semesterService.ViewSemester(batch.SemesterIdCurrent);

            if (batch == null)
                return HttpNotFound();

            BatchDetailsViewModel model = new BatchDetailsViewModel();
            model.Id = batch.Id;
            model.ProgramId = batch.ProgramId;
            model.ProgramName = program.ProgramName;
            model.BatchNo = batch.BatchNo;
            model.CurrentStudent = batchService.ViewCurrentStudentsOfBatch(batch.Id).Count();
            model.AdmittedStudent = batchService.ViewAdmittedStudentsOfBatch(batch.Id).Count();
            model.SemesterIdCurrent = batch.SemesterIdCurrent;
            model.CurrentSemesterNo = 0;
            if (semester != null)
            {
                model.CurrentSemesterNo = semester.SemesterNo;
            }
            model.Status = batch.BatchStatus;
            model.BatchCoordinator = "";

            ApplicationUser batchCoordinator = batchService.GetBatchCoordinator(id);
            if (batchCoordinator != null)
            {
                model.BatchCoordinator = batchCoordinator.FullName;
            }

            return View(model);
        }

        // View current students of batch
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult IndexCurrentStudents(int id)
        {
            Batch batch = batchService.ViewBatch(id);
            Program program = programService.ViewProgram(batch.ProgramId);

            IEnumerable<Student> students = batchService.ViewCurrentStudentsOfBatch(id);

            IList<StudentIndexViewModel> studentsToView = new List<StudentIndexViewModel>();
            StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel();
            ApplicationUser user;

            foreach (var item in students)
            {
                user = UserManager.FindById(item.UserId);

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

            ViewBag.ProgramId = program.Id;
            ViewBag.ProgramName = program.ProgramName;
            ViewBag.BatchId = batch.Id;
            ViewBag.BatchNo = batch.BatchNo;

            return View(studentsToView);
        }

        // View admitted students of batch
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult IndexAdmittedStudents(int id)
        {
            Batch batch = batchService.ViewBatch(id);
            Program program = programService.ViewProgram(batch.ProgramId);

            IEnumerable<Student> students = batchService.ViewAdmittedStudentsOfBatch(id);

            IList<StudentIndexViewModel> studentsToView = new List<StudentIndexViewModel>();
            StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel();
            ApplicationUser user;

            foreach (var item in students)
            {
                user = UserManager.FindById(item.UserId);

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

            ViewBag.ProgramId = program.Id;
            ViewBag.ProgramName = program.ProgramName;
            ViewBag.BatchId = batch.Id;
            ViewBag.BatchNo = batch.BatchNo;

            return View(studentsToView);
        }

        // Update current semester
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult UpdateCurrentSemester(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = programService.ViewProgram(batch.ProgramId);
            Semester currentSemester = semesterService.ViewSemester(batch.SemesterIdCurrent);

            UpdateCurrentSemesterViewModel model = new UpdateCurrentSemesterViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.SemesterIdCurrent = currentSemester.Id;
            model.CurrentSemesterNo = currentSemester.SemesterNo;

            List<Semester> semesters = semesterService.GetSemestersOfProgram(program.Id).ToList();
            //semesters.Add(new Semester()
            //{
            //    Id = 0,
            //    ProgramId = 0,
            //    SemesterNo = 100
            //});
            model.Semesters = semesters;

            return View(model);
        }

        // POST: update current sememster
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult UpdateCurrentSemester(UpdateCurrentSemesterViewModel model)
        {
            Batch batchToUpdate = batchService.ViewBatch(model.BatchId);

            if (batchToUpdate.SemesterIdCurrent == model.SemesterIdUpdated)
            {
                return RedirectToAction("Details", "Batch",
                    new { area = "One", id = model.BatchId });
            }

            batchToUpdate.SemesterIdCurrent = model.SemesterIdUpdated;
            batchService.EditBatch(batchToUpdate);

            //if (batchService.EditBatch(batchToUpdate))
            //{
            //    // Update students current semester
            //    var students = batchService.ViewCurrentStudentsOfBatch(model.BatchId);
            //    foreach (var student in students)
            //    {
            //        student.SemesterId = model.SemesterIdUpdated;
            //        if (!studentService.EditStudent(student))
            //        {
            //            break;
            //        }
            //    }
            //}

            return RedirectToAction("Details", "Batch",
                new { area = "One", id = model.BatchId });
        }

        // update batch status
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult UpdateBatchStatus(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = programService.ViewProgram(batch.ProgramId);
            Semester currentSemester = semesterService.ViewSemester(batch.SemesterIdCurrent);

            UpdateBatchStatusViewModel model = new UpdateBatchStatusViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;
            model.CurrentSemesterNo = currentSemester.SemesterNo;
            model.BatchStatus = batch.BatchStatus;

            List<string> values = new List<string>();
            values.Add("Active");
            values.Add("Passed");
            model.StatusValues = values;

            return View(model);
        }

        // POST: update batch status
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult UpdateBatchStatus(UpdateBatchStatusViewModel model)
        {
            Batch batchToUpdate = batchService.ViewBatch(model.BatchId);

            if (batchToUpdate.BatchStatus == model.BatchStatusNew)
            {
                return RedirectToAction("Details", "Batch",
                    new { area = "One", id = model.BatchId });
            }

            batchToUpdate.BatchStatus = model.BatchStatusNew;
            batchService.EditBatch(batchToUpdate);

            return RedirectToAction("Details", "Batch",
                new { area = "One", id = model.BatchId });
        }

        // Batch coordinators
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult BatchCoordinators(int id)
        {
            Batch batch = batchService.ViewBatch(id);
            Program program = programService.ViewProgram(batch.ProgramId);

            BatchCoordinatorIndexViewModel model = new BatchCoordinatorIndexViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = id;
            model.BatchNo = batch.BatchNo;

            var batchCoordinators = batchService.GetBatchCoordinatorOfBatch(id);
            List<BatchCoordinatorViewModel> coordinators = new List<BatchCoordinatorViewModel>();
            BatchCoordinatorViewModel bcModel = new BatchCoordinatorViewModel();
            Semester semester;
            ApplicationUser user;
            foreach (var item in batchCoordinators)
            {
                semester = semesterService.ViewSemester(item.SemesterId);
                user = userService.ViewUser(item.TeacherId);

                bcModel.SemesterNo = semester.SemesterNo;
                bcModel.Name = user.FullName;
                bcModel.ImagePath = user.ImagePath;
                bcModel.StartDate = item.StartDate.ToString("dd-MM-yyyy");
                bcModel.EndDate = item.EndDate.ToString("dd-MM-yyyy");
                //bcModel.StartDate = item.StartDate.ToString();
                //bcModel.EndDate = item.EndDate.ToString();

                coordinators.Add(bcModel);
                bcModel = new BatchCoordinatorViewModel();
            }

            model.Coordinators = coordinators;

            return View(model);
        }

        // Assign batch coordinators
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult AssignCoordinator(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = programService.ViewProgram(batch.ProgramId);

            AssignCoordinatorViewModel model = new AssignCoordinatorViewModel();
            model.ProgramId = program.Id;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;

            IdentityRole role = roleService.GetRoleByName("Teacher");
            var teachers = UserManager.Users.Where(u => 
                u.Status == "On Duty" &&
                u.Roles.Any(r => r.RoleId == role.Id)).ToList();

            model.Teachers = teachers;

            return View(model);
        }

        // POST: Assign batch coordinator
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult AssignCoordinator(AssignCoordinatorViewModel model)
        {
            if (model.CoordinatorId == "")
            {
                return RedirectToAction("BatchCoordinators", "Batch", new { area = "One", id = model.BatchId });
            }

            Batch batch = batchService.ViewBatch(model.BatchId);
            var roleCoordinator = roleService.GetRoleByName("Batch Coordinator");

            BatchCoordinator coordinator = new BatchCoordinator();
            coordinator.BatchId = model.BatchId;
            coordinator.SemesterId = batch.SemesterIdCurrent;
            coordinator.TeacherId = model.CoordinatorId;
            coordinator.StartDate = DateTime.Now;
            coordinator.EndDate = coordinator.StartDate;

            BatchCoordinator lastCoordinator = batchService.GetLastBatchCoordinator(model.BatchId);
            var result1 = true;
            if (lastCoordinator != null)
            {
                lastCoordinator.EndDate = coordinator.StartDate;
                result1 = batchService.EditBatchCoordinator(lastCoordinator);
            }

            IdentityRole role;
            IEnumerable<ApplicationUser> teachers;

            if (result1)
            {
                var result2 = batchService.AssignCoordinator(coordinator);
                IdentityResult result3 = UserManager.AddToRole(model.CoordinatorId, roleCoordinator.Name);
                if (result2 && result3.Succeeded)
                {
                    return RedirectToAction("BatchCoordinators", "Batch", new { area = "One", id = model.BatchId });
                }

                role = roleService.GetRoleByName("Teacher");
                teachers = UserManager.Users.Where(u =>
                    u.Status == "On Duty" &&
                    u.Roles.Any(r => r.RoleId == role.Id)).ToList();

                model.Teachers = teachers;

                return View(model);
            }

            role = roleService.GetRoleByName("Teacher");
            teachers = UserManager.Users.Where(u =>
                u.Status == "On Duty" &&
                u.Roles.Any(r => r.RoleId == role.Id)).ToList();

            model.Teachers = teachers;

            return View(model);
        }

        // Current Student Details
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult CurrentStudentDetails(int id)
        {
            var student = studentService.ViewStudent(id);
            var user = UserManager.FindById(student.UserId);
            var program = programService.ViewProgram(student.ProgramId);
            var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
            var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);
            var semester = semesterService.ViewSemester(student.SemesterId);

            StudentDetailsViewModel model = new StudentDetailsViewModel();
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

            ViewBag.ProgramId = program.Id;
            ViewBag.BatchId = batchCurrent.Id;
            return View(model);
        }

        // Admitted Student Details
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult AdmittedStudentDetails(int id)
        {
            var student = studentService.ViewStudent(id);
            var user = UserManager.FindById(student.UserId);
            var program = programService.ViewProgram(student.ProgramId);
            var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
            var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);
            var semester = semesterService.ViewSemester(student.SemesterId);

            StudentDetailsViewModel model = new StudentDetailsViewModel();
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

            ViewBag.ProgramId = program.Id;
            ViewBag.BatchId = batchOriginal.Id;
            return View(model);
        }

        // Batch Exist
        public JsonResult BatchExist(int BatchNo, int ProgramId)
        {
            if (!batchService.BatchExist(BatchNo, ProgramId))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}