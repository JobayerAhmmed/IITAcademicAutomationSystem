using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Areas.Two.ServiceImpl;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IITAcademicAutomationSystem.Areas.Two.Controllers
{
    public class RoutineManagementController : Controller
    {
        RoutineManagementSerI routineService = new RoutineManagementSerImpl();
        private UtilitySerI utilityService = new UtilitySerImpl();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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
        // GET: Two/Routine
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadRoutine()
        {
            return View();
        }

        public ActionResult ViewRoutine_tp()
        {
            return View();
        }

        public ActionResult ViewRoutine_s()
        {
            return View();
        }

        public ActionResult EditOrDeleteRoutine()
        {
            return View();
        }

        public ActionResult ViewRoutine(string filePath)
        {
            return View();
        }

        public FilePathResult DownloadRoutine(string fileName)
        {
            string filePath = "~/Areas/Two/AcademicFiles/Routine/" + fileName;
            return File(filePath, "multipart/form-data", fileName);
        }

        //.................http service...........

        [HttpPost]
        public JsonResult AddRoutine(UploadRoutineReqDto uploadRoutineReqDto)
        {
            try
            {
                string fileName, actualFileName;
                fileName = actualFileName = string.Empty;
                if (Request.Files != null)
                {
                    var file = Request.Files[0];
                    actualFileName = file.FileName;
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    int size = file.ContentLength;

                    string roorPath = "~/Areas/Two/AcademicFiles/Routine";
                    file.SaveAs(Path.Combine(Server.MapPath(roorPath), fileName));

                    // string imagePath = roorPath+"/" + fileName;
                    string imagePath = fileName;
                    uploadRoutineReqDto.path = imagePath;
                    string uploaderId = User.Identity.GetUserId();
                    routineService.uploadRoutine(uploadRoutineReqDto, uploaderId);

                }
                return new JsonResult { Data = new { Status = "OK", Message = "Routine has been Uploaded Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Uploading Routine"} };
            }
        }

        [HttpGet]
        public JsonResult getRoutines_tp(int programId, int semesterId)
        {
            try
            {
                var data = routineService.getRoutines_teacherProgramOfficer(programId, semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Routine"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult checkIfRoutineUploaded(int programId, int semesterId)
        {
            try
            {
                var data = routineService.checkIfRoutineUploaded(programId, semesterId);
                Object response = new { Status = "OK", Data = data };

                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Checking Routine Status"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getRoutines_s()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = routineService.getRoutines_student(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Routine"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult editRoutine(EditRoutineReqDto editRoutineReqDto)
        {
            try
            {
                routineService.editRoutine(editRoutineReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Routine has been Edited Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing Routine"} };
            }
        }

        [HttpPost]
        public JsonResult deleteRoutine(int routineId)
        {
            try
            {
                routineService.deleteRoutine(routineId);
                return new JsonResult { Data = new { Status = "OK", Message = "Routine has been Deleted Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Deleting Routine"} };
            }
        }
    }
}