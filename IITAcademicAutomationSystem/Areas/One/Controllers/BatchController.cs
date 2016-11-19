using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using IITAcademicAutomationSystem.DAL;
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
    public class BatchController : Controller
    {
        private ApplicationUserManager _userManager;
        private IProgramService programService;
        private IBatchService batchService;
        private ISemesterService semesterService;
        private IUserService userService;
        private IStudentService studentService;
        private IMapper Mapper;
        public BatchController()
        {
            programService = new ProgramService(ModelState, new UnitOfWork());
            batchService = new BatchService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            studentService = new StudentService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public BatchController(IBatchService batchService)
        {
            this.batchService = batchService;
            programService = new ProgramService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
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
        public ActionResult Index(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewActiveBatches(programId);
            Program program = programService.ViewProgram(programId);
            IEnumerable<Semester> semesters = semesterService.GetSemestersOfProgram(programId);

            IList<BatchIndexViewModel> model = new List<BatchIndexViewModel>();
            BatchIndexViewModel batchIndexViewModel = new BatchIndexViewModel();
            Semester currentSemester;

            foreach (var item in batches)
            {
                currentSemester = semesterService.ViewSemester(item.SemesterIdCurrent);
                batchIndexViewModel.Id = item.Id;
                batchIndexViewModel.BatchNo = item.BatchNo;
                batchIndexViewModel.SemesterNoCurrent = currentSemester.SemesterNo;
                batchIndexViewModel.Semesters = semesters;

                model.Add(batchIndexViewModel);
                batchIndexViewModel = new BatchIndexViewModel();
            }

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(model);
        }

        // IndexPassed
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
        public ActionResult Details(int id)
        {
            Batch batch = batchService.ViewBatch(id);
            Program program = programService.ViewProgram(batch.ProgramId);
            Semester semester = semesterService.ViewSemester(batch.SemesterIdCurrent);

            if (batch == null)
                return HttpNotFound();

            BatchDetailsViewModel batchDetailsVM = new BatchDetailsViewModel()
            {
                Id = batch.Id,
                ProgramId = batch.ProgramId,
                ProgramName = program.ProgramName,
                BatchNo = batch.BatchNo,
                CurrentStudent = batchService.ViewCurrentStudentsOfBatch(batch.Id).Count(),
                AdmittedStudent = batchService.ViewAdmittedStudentsOfBatch(batch.Id).Count(),
                SemesterIdCurrent = batch.SemesterIdCurrent,
                CurrentSemesterNo = semester.SemesterNo,
                Status = batch.BatchStatus
            };

            return View(batchDetailsVM);
        }

        // View current students of batch
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

            var semesters = semesterService.GetSemestersOfProgram(program.Id);
            model.Semesters = semesters;

            return View(model);
        }

        // POST: update current sememster
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCurrentSemester(UpdateCurrentSemesterViewModel model)
        {
            Batch batchToUpdate = batchService.ViewBatch(model.BatchId);

            if (batchToUpdate.SemesterIdCurrent == model.SemesterIdUpdated)
            {
                return RedirectToAction("Details", "Batch",
                    new { area = "One", id = model.BatchId });
            }

            batchToUpdate.SemesterIdCurrent = model.SemesterIdUpdated;
            if (batchService.EditBatch(batchToUpdate))
            {
                // Update students current semester
                var students = batchService.ViewCurrentStudentsOfBatch(model.BatchId);
                foreach (var student in students)
                {
                    student.SemesterId = model.SemesterIdUpdated;
                    if (!studentService.EditStudent(student))
                    {
                        break;
                    }
                }
            }

            return RedirectToAction("Details", "Batch",
                new { area = "One", id = model.BatchId });
        }

        // Current Student Details
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