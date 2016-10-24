using AutoMapper;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Controllers
{
    public class BatchController : Controller
    {
        private IProgramService programService;
        private IBatchService batchService;
        private ISemesterService semesterService;
        private IUserService userService;
        private IMapper Mapper;
        public BatchController()
        {
            programService = new ProgramService(ModelState, new UnitOfWork());
            batchService = new BatchService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public BatchController(IBatchService batchService)
        {
            this.batchService = batchService;
            programService = new ProgramService(ModelState, new UnitOfWork());
            semesterService = new SemesterService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        // Index
        public ActionResult Index(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewActiveBatches(programId);
            Program program = programService.ViewProgram(programId);

            if (batches == null)
                return HttpNotFound();

            IList<BatchIndexViewModel> batchesToView = new List<BatchIndexViewModel>();

            //IEnumerable<BatchIndexViewModel> batchesToView =
                //Mapper.Map<IEnumerable<Batch>, IEnumerable<BatchIndexViewModel>>(batches);

            foreach (var item in batches)
            {
                batchesToView.Add(new BatchIndexViewModel()
                {
                    Id = item.Id,
                    ProgramId = item.ProgramId,
                    ProgramName = program.ProgramName,
                    BatchNo = item.BatchNo
                });
            }

            ViewBag.ProgramId = programId;
            ViewBag.ProgramName = program.ProgramName;

            return View(batchesToView);
        }

        // IndexPassed
        public ActionResult IndexPassed(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewPassedBatches(programId);
            Program program = programService.ViewProgram(programId);

            if (batches == null)
                return HttpNotFound();

            IList<BatchIndexViewModel> batchesToView = new List<BatchIndexViewModel>();

            //IEnumerable<BatchIndexViewModel> batchesToView =
            //Mapper.Map<IEnumerable<Batch>, IEnumerable<BatchIndexViewModel>>(batches);

            foreach (var item in batches)
            {
                batchesToView.Add(new BatchIndexViewModel()
                {
                    Id = item.Id,
                    ProgramId = item.ProgramId,
                    ProgramName = program.ProgramName,
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
            int batchNo = previousBatch.BatchNo;

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
            Batch batchToCreate = new Batch()
            {
                ProgramId = batchCreateVM.ProgramId,
                BatchNo = batchCreateVM.BatchNo,
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
        public ActionResult IndexCurrentStudents(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = programService.ViewProgram(batch.ProgramId);

            IEnumerable<Student> students = batchService.ViewCurrentStudentsOfBatch(batchId);
            IList<BatchStudent> currentStudents = new List<BatchStudent>();
            BatchStudent bcs;

            foreach (var student in students)
            {
                bcs = new BatchStudent()
                {
                    StudentId = student.Id,
                    UserId = student.UserId,
                    SemesterIdCurrent = student.SemesterId,
                    Roll = student.CurrentRoll,
                    FullName = userService.ViewUser(student.UserId).FullName
                };
                currentStudents.Add(bcs);
            }
            BatchStudentsViewModel batchStudentsVM = new BatchStudentsViewModel()
            {
                ProgramId = program.Id,
                ProgramName = program.ProgramName,
                BatchId = batchId,
                BatchNo = batch.BatchNo,
                Students = currentStudents
            };

            return View(batchStudentsVM);
        }

        // View admitted students of batch
        public ActionResult IndexAdmittedStudents(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = programService.ViewProgram(batch.ProgramId);

            IEnumerable<Student> students = batchService.ViewAdmittedStudentsOfBatch(batchId);
            IList<BatchStudent> admittedStudents = new List<BatchStudent>();
            BatchStudent bcs;

            foreach (var student in students)
            {
                bcs = new BatchStudent()
                {
                    StudentId = student.Id,
                    UserId = student.UserId,
                    SemesterIdCurrent = student.SemesterId,
                    Roll = student.CurrentRoll,
                    FullName = userService.ViewUser(student.UserId).FullName
                };
                admittedStudents.Add(bcs);
            }
            BatchStudentsViewModel batchStudentsVM = new BatchStudentsViewModel()
            {
                ProgramId = program.Id,
                ProgramName = program.ProgramName,
                BatchId = batchId,
                BatchNo = batch.BatchNo,
                Students = admittedStudents
            };

            return View(batchStudentsVM);
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