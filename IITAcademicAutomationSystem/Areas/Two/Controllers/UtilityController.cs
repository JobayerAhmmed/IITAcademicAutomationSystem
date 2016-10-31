using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Areas.Two.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.Two.Controllers
{
    public class UtilityController : Controller
    {

        private UtilitySerI utilityService=new UtilitySerImpl();


        // GET: Two/Utility
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowPdf()
        {
            return View();
        }

        public ActionResult ShowPdf_an()
        {
            return View();
        }

        //.....................http service.............

        [HttpGet]
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
        public JsonResult getProgramsOfATeacher()
        {
            try
            {
                int teacherId = utilityService.getIdOfLoggedInTeacher();//this has to be changed
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
        public JsonResult getSemestersOfATeacherOfAProgramr(int programId)
        {
            try
            {
                int teacherId = utilityService.getIdOfLoggedInTeacher();//this has to be changed
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
        public JsonResult getCoursesOfATeacherOfASemesterOfAProgram(int programId, int semesterId)
        {
            try
            {
                int teacherId = utilityService.getIdOfLoggedInTeacher();//this has to be changed
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
        public JsonResult getBatchOfASemesterOfAProgram(int programId,int  semesterId)
        {            
            try
            {
                int teacherId = utilityService.getIdOfLoggedInTeacher();//this has to be changed
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
        public JsonResult getProgramSemesterBatchOfLoggedInStudent()
        {
            try
            {
                var data = utilityService.getProgramSemesterBatchOfLoggedInStudent();
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
        public JsonResult getAllCoursesOfAStudent()
        {
            try
            {
                var data = utilityService.getCoursesOfAStudent();
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