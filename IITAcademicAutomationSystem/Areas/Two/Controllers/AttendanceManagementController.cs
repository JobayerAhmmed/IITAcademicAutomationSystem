using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Areas.Two.ServiceImpl;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace IITAcademicAutomationSystem.Areas.Two.Controllers
{
    public class AttendanceManagementController : Controller
    {
        AttendanceManagementSerI attendanceManagementService = new AttendanceManagementSerImpl();
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
        // GET: Two/Attendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GiveAttendance()
        {
            return View();
        }

        public ActionResult EditAttendance()
        {
            return View();
        }

        public ActionResult ViewAttendanceCourseWise()
        {
            return View();
        }

        public ActionResult ViewAttendanceIndividual()
        {
            return View();
        }

        //............................http services...................

        [HttpGet]
        public JsonResult getLastClassNumber(int programId,int semesterId,int batchId,int courseId)
        {
            try
            {
                var data = attendanceManagementService.getLastClassNumber(programId, semesterId, batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Last Class No."};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult saveAttendance(GiveAttendanceResDto giveAttendanceResDto)
        {
            try
            {
                string teacherId = User.Identity.GetUserId();
                attendanceManagementService.saveAttendance(giveAttendanceResDto, teacherId);
                return new JsonResult { Data = new { Status = "OK", Message = "Attendance has been Saved Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Saving Attendance"+e } };
            }
        }

        [HttpGet]
        public JsonResult getClassesNumbersAndDates(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var data = attendanceManagementService.getClassesNumbersAndDates(programId, semesterId, batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Last Class Numbers and Dates" };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getAttendancesForEditing(int programId, int semesterId, int batchId, int courseId,int classNo)
        {
            try
            {
                var data = attendanceManagementService.getAttendances(programId, semesterId, batchId, courseId, classNo);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Attendance History"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

         [HttpPost]
         public JsonResult saveEditedAttendance(EditAttendanceReqDto editAttendanceReqDto)
         {
             try
             {
                 attendanceManagementService.saveEditedAttendance(editAttendanceReqDto);
                 return new JsonResult { Data = new { Status = "OK", Message = "Attendance has been Edited Successfully" } };
             }
             catch (Exception e)
             {
                 return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing Attendance" } };
             }
         }

        [HttpGet]
        public JsonResult getAttendancesCourseWise(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var data = attendanceManagementService.getAttendancesCourseWise(programId, semesterId, batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Attendance Course Wisr"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getAttendancesIndividual()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = attendanceManagementService.getAttendanceOfAStudentOfAllCourses(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Attendance"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}