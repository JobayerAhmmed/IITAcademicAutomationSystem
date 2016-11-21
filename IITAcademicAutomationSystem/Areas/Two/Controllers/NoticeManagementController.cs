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
    public class NoticeManagementController : Controller
    {
        NoticeManagementSerI noticeService = new NoticeManagementSerImpl();
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
        // GET: Two/Notice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadNotice()
        {
            return View();
        }

        public ActionResult ViewNotice_tp(string path)
        {
            return View();
        }

        public ActionResult ViewNotice_s(string path)
        {
            return View();
        }

        public ActionResult EditOrDeleteNotice()
        {
            return View();
        }
        

        public FilePathResult DownloadNotice(string fileName)
        {
            string filePath = "~/Areas/Two/AcademicFiles/Notice/"+ fileName;
            return File(filePath, "multipart/form-data", fileName);
        }

        public ActionResult ViewNotice(string filePath)
        {
            return View();
        }

        //.................http service...........

        [HttpPost]
        public JsonResult AddNotice(UploadNoticeReqDto uploadNoticeReqDto)
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

                    string rootPath = "~/Areas/Two/AcademicFiles/Notice";

                    file.SaveAs(Path.Combine(Server.MapPath(rootPath), fileName));

                    //string imagePath = rootPath+"/" + fileName;
                    string imagePath =  fileName;

                    uploadNoticeReqDto.path = imagePath;
                    string uploaderId = User.Identity.GetUserId();
                    noticeService.uploadNotice(uploadNoticeReqDto, uploaderId);

                }
                return new JsonResult { Data = new { Status = "OK", Message = "Notice has been Uploaded Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Uploading Notice"} };
            }
        }

        [HttpGet]
        public JsonResult getNotices_tp(int programId, int semesterId)
        {
            try
            {
                var data = noticeService.getNotices_teacherProgramOfficer(programId, semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Notices"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getNotices_s()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = noticeService.getNotices_student(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Notices"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult editNotice(EditNoticeReqDto editNoticeReqDto)
        {
            try
            {
                noticeService.editNotice(editNoticeReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Notice has been Edited Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing Notice"} };
            }
        }

        [HttpPost]
        public JsonResult deleteNotice(int noticeId)
        {
            try
            {
                noticeService.deleteNotice(noticeId);
                return new JsonResult { Data = new { Status = "OK", Message = "Notice has been Deleted Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Deleting Notice"} };
            }
        }

    }
}