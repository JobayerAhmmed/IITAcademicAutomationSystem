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
using static IITAcademicAutomationSystem.Areas.Two.RequestDto.AcademicCalendarReqDto;

namespace IITAcademicAutomationSystem.Areas.Two.Controllers
{
    public class AcademicCalendarManagementController : Controller
    {
        AcademicCalendarManagementSerI academicCalendarService = new AcademicCalendarManagementSerImpl();
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
        // GET: Two/AcademicCalendar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadAcademicCalendar()
        {
            return View();
        }

        public ActionResult ViewAcademicCalendar_tp()
        {
            return View();
        }

        public ActionResult ViewAcademicCalendar_s()
        {
            return View();
        }

        public ActionResult EditOrDeleteAcademicCalendar()
        {
            return View();
        }

        public ActionResult ViewAcademicCalendar(string filePath)
        {
            return View();
        }

        public FilePathResult DownloadAcademicCalendar(string fileName)
        {
            string filePath = "~/Areas/Two/AcademicFiles/AcademicCalendar/" + fileName;
            return File(filePath, "multipart/form-data", fileName);
        }

        //.................http service...........

        [HttpPost]
        public JsonResult AddAcademicCalendar(UploadAcademicCalendarReqDto uploadAcademicCalendarReqDto)
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

                    string roorPath = "~/Areas/Two/AcademicFiles/AcademicCalendar";
                    file.SaveAs(Path.Combine(Server.MapPath(roorPath), fileName));
                    
                   // string imagePath = roorPath+"/" + fileName;
                    string imagePath =  fileName;
                    uploadAcademicCalendarReqDto.path = imagePath;

                    string uploaderId= User.Identity.GetUserId();
                    academicCalendarService.uploadAcademicCalendar(uploadAcademicCalendarReqDto, uploaderId);

                }
                return new JsonResult { Data = new { Status = "OK", Message = "AcademicCalendar has been Uploaded Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured Uploading Saving AcademicCalendar" } };
            }
        }

        [HttpGet]
        public JsonResult getAcademicCalendars_tp(int programId, int semesterId)//tp means teacherProgramofficer
        {
            try
            {
                var data = academicCalendarService.getAcademicCalendars_teacherProgramOfficer(programId, semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Academic Calendar"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult checkIfAcademicCalendarUploaded(int programId,int semesterId)
        {
            try
            {
                var data = academicCalendarService.checkIfAcademicCalendarUploaded(programId, semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Checking If Academic Calendar is Uploaded"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getAcademicCalendars_s()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = academicCalendarService.getAcademicCalendars_student(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Academic Calendar"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult editAcademicCalendar(EditAcademicCalendarReqDto editAcademicCalendarReqDto)
        {
            try
            {
                academicCalendarService.editAcademicCalendar(editAcademicCalendarReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "AcademicCalendar has been Edited Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing AcademicCalendar"} };
            }
        }

        [HttpPost]
        public JsonResult deleteAcademicCalendar(int academicCalendarId)
        {
            try
            {
                academicCalendarService.deleteAcademicCalendar(academicCalendarId);
                return new JsonResult { Data = new { Status = "OK", Message = "Academic Calendar has been Deleted Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Deleting AcademicCalendar"} };
            }
        }
    }
}