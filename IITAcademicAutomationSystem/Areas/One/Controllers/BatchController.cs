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
        private IBatchService batchService;
        private IUserService userService;
        private IMapper Mapper;
        public BatchController()
        {
            batchService = new BatchService(ModelState, new UnitOfWork());
            userService = new UserService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public BatchController(IBatchService batchService)
        {
            this.batchService = batchService;
            userService = new UserService(ModelState, new UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        // Index
        public ActionResult Index(int programId)
        {
            IEnumerable<Batch> batches = batchService.ViewActiveBatches(programId);
            Program program = batchService.GetProgramById(programId);

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
            Program program = batchService.GetProgramById(programId);

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
            Program program = batchService.GetProgramById(programId);
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
            Program program = batchService.GetProgramById(batch.ProgramId);
            //Semester semester =

            if (batch == null)
                return HttpNotFound();

            BatchDetailsViewModel batchDetailsVM = new BatchDetailsViewModel()
            {
                Id = batch.Id,
                ProgramId = batch.ProgramId,
                ProgramName = program.ProgramName,
                BatchNo = batch.BatchNo,
                CurrentStudent = batchService.ViewCurrentStudentsOfBatch(batch.Id).Count(),
                AdmittedStudent = 0,
                SemesterIdCurrent = batch.SemesterIdCurrent,
                CurrentSemesterNo = 1,
                Status = batch.BatchStatus
            };

            return View(batchDetailsVM);
        }

        // View current students of batch
        public ActionResult IndexCurrentStudents(int batchId)
        {
            Batch batch = batchService.ViewBatch(batchId);
            Program program = batchService.GetProgramById(batch.ProgramId);

            IEnumerable<Student> students = batchService.ViewCurrentStudentsOfBatch(batchId);
            IList<BatchCurrentStudent> currentStudents = new List<BatchCurrentStudent>();
            BatchCurrentStudent bcs;

            foreach (var student in students)
            {
                bcs = new BatchCurrentStudent()
                {
                    StudentId = student.Id,
                    UserId = student.UserId,
                    SemesterIdCurrent = student.SemesterId,
                    CurrentRoll = student.CurrentRoll,
                    FullName = userService.ViewUser(student.UserId).FullName
                };
                currentStudents.Add(bcs);
            }
            BatchCurrentStudentsViewModel currentStudentsVM = new BatchCurrentStudentsViewModel()
            {
                ProgramId = program.Id,
                ProgramName = program.ProgramName,
                BatchIdCurrent = batchId,
                BatchNo = batch.BatchNo,
                CurrentStudents = currentStudents
            };

            return View(currentStudentsVM);
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