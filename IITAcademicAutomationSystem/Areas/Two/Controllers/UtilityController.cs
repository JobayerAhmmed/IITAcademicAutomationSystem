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
    public class UtilityController : Controller
    {

        private UtilitySerI utilityService=new UtilitySerImpl();
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


        [Authorize(Roles = "Program Officer Regular,Program Officer Evening,Teacher,Batch Coordinator,Admin,Student")]
        public ActionResult ShowPdf()
        {
            return View();
        }

        /*public ActionResult ShowPdf_an()
        {
            return View();
        }*/

        //.....................http service.............

        [HttpGet]
        [Authorize(Roles = "Program Officer Regular,Program Officer Evening,Teacher,Batch Coordinator,Admin,Student")]
        public JsonResult getAllPrograms()
        {
            try
            {
                var data = utilityService.getAllPrograms();
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Programs" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public JsonResult getProgramsOfATeacher()
        {
            try
            {
                var teacherId = User.Identity.GetUserId();
                var data= utilityService.getProgramsOfATeacher(teacherId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Programs"+e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Program Officer Regular,Program Officer Evening,Teacher,Batch Coordinator,Admin,Student")]
        public JsonResult getSemestersOfAProgram(int programId)
        {
            try
            {
                var data = utilityService.getSemestersOfAProgram(programId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Programs" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public JsonResult getSemestersOfATeacherOfAProgramr(int programId)
        {
            try
            {
                var teacherId = User.Identity.GetUserId();
                var data = utilityService.getSemestersOfATeacherOfAProgram(teacherId,programId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Semesters" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public JsonResult getCoursesOfATeacherOfASemesterOfAProgram(int programId, int semesterId)
        {
            try
            {
                var teacherId = User.Identity.GetUserId();
                var data = utilityService.getCoursesOfATeacherOfASemesterOfAProgram(teacherId, programId,semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Courses" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Program Officer Regular,Program Officer Evening,Teacher,Batch Coordinator,Admin,Student")]
        public JsonResult getBatchOfASemesterOfAProgram(int programId,int  semesterId)
        {            
            try
            {
                var teacherId = User.Identity.GetUserId();
                var data = utilityService.getBatch(programId,semesterId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Batch" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        public JsonResult getProgramSemesterBatchOfLoggedInStudent()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = utilityService.getProgramSemesterBatchOfLoggedInStudent(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Fetching Information" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Admin")]
        public JsonResult getStudentsOfACOurse(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var data = utilityService.getStudentsOfACOurse(programId, semesterId, batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Fetching Studets" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Teacher,Admin,Student")]
        public JsonResult getAllCoursesOfAStudent()
        {
            try
            {
                var userId = User.Identity.GetUserId();
                int studentId = utilityService.getStudentIdByUserId(userId);
                var data = utilityService.getCoursesOfAStudent(studentId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Fetching Studets" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Program Officer Regular,Program Officer Evening,Teacher,Batch Coordinator,Admin,Student")]
        public JsonResult getBatches(int programId)
        {
            try
            {
                var data = utilityService.getBatchesOfAProgram(programId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Fetching Studets" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Batch Coordinator,Admin")]
        public JsonResult getSemestersOfABatchCoOrdinator(int programId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var data = utilityService.getSemestersOfABatchCoordinator(userId, programId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Fetching Studets" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}