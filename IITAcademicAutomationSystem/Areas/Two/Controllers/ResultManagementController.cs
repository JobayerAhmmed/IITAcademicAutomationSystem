﻿using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Areas.Two.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.Two.Controllers
{
    public class ResultManagementController : Controller
    {

        private ResultManagementSerI resultManagementSerI=new ResultManagementSerImpl();

        // GET: Two/Result
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrViewHead()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrViewSubHead()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DistributeMarks()
        {
            return View();
        }

        public ActionResult ViewDistributedMarks_t()
        {
            return View();
        }

        public ActionResult ViewDistributedMarks_s()
        {
            return View();
        }

        public ActionResult EditDistributedMarks()
        {
            return View();
        }

        public ActionResult GiveMarks()
        {
            return View();
        }

        public ActionResult ViewGivenMarks_t()
        {
            return View();
        }

        public ActionResult ViewGivenMarks_s()
        {
            return View();
        }

        public ActionResult EditGivenMarks()
        {
            return View();
        }

        public ActionResult FinalSubmission()
        {
            return View();
        }

        public ActionResult ViewAllFinalSubmission()
        {
            return View();
        }


        //..........................For HTTP service


        [HttpGet]
        public JsonResult getAllHeads()
        {
            try
            {
                var data=resultManagementSerI.getAllHeads();
                Object response = new { Status = "OK", Data=data };
                return this.Json(response,JsonRequestBehavior.AllowGet) ;
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Heads"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult createHead(AddHeadRequestDto addHeadRequestDto)
        {
            try
            {                
                resultManagementSerI.createHead(addHeadRequestDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Head has been Saved Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Creating Head"} };
            }
        }

        [HttpGet]
        public JsonResult getSubHeads(int headId)
        {
            try
            {
                var data = resultManagementSerI.getSubHeads(headId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retrivig Subheads"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult createSubHead(AddSubHeadRequestDto addSubHeadRequestDto)
        {
            try
            {
                
                resultManagementSerI.createSubHead(addSubHeadRequestDto);

                return new JsonResult { Data = new { Status = "OK", Message = "Sub Head has been Saved Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Creating Sub Head"} };
            }
        }

        [HttpPost]
        public JsonResult distributeMarks(DistributeMarksFinalReqDto distributeMarksFinalReqDto)
        {
            try
            {
                resultManagementSerI.distributeMarks(distributeMarksFinalReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Marks Have benn Distributed Successfully" } };
            }
            catch(Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Distributing Marks" } };
            }
        }

        [HttpGet]
        public JsonResult checkIfMarksIsDistributedForACourse(int programId, int semesterId,int batchId, int courseId)
        {
            try
            {
                var data = resultManagementSerI.checkIfMarksIsDistributedForACourse(programId, semesterId,batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Checking Marks Distribution"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getDistributedMarks(int programId,int semesterId,int batchId,int courseId)
        {
            try
            {
                var data = resultManagementSerI.getDistributedMarks(programId, semesterId,batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Distributed Marks"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getDistributedMarksPartial(int programId, int semesterId,int batchId, int courseId)
        {
            try
            {
                var data = resultManagementSerI.getDistributedMarks(programId, semesterId,batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Distributed Marks"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult editDistributedMarks(EditedDistributeMarksReqDto editedDistributeMarksReqDto)
        {
            try
            {
                resultManagementSerI.editDistributedMarks(editedDistributeMarksReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Marks has been Edited Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing Marks"} };
            }
        }

        [HttpPost]
        public JsonResult giveMarks(GiveMarksReqDto giveMarksReqDto)
        {
            try
            {
                resultManagementSerI.saveGivenMarks(giveMarksReqDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Marks Have been Saved Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Saving Marks"} };
            }
        }

        [HttpGet]
        public JsonResult checkIfMarksIsGiven(int programId, int semesterId, int batchId, int courseId,int headId,int subHeadId)
        {
            try
            {
                var data = resultManagementSerI.checkIfMarksIsGiven(programId, semesterId, batchId, courseId, headId, subHeadId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Checking Marks Distribution" };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getGivenMarks_t(int programId, int semesterId,int batchId, int courseId)
        {
            try
            {
                var data = resultManagementSerI.getGivenMarks_t(programId, semesterId,batchId,courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving  Marks"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getGivenMarks_s(int courseId)
        {
            try
            {
                var data = resultManagementSerI.getGivenMarks_s(courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving  Marks" };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult getGivenMarksForEditing(int programId, int semesterId, int batchId, int courseId,int headId,int subHeadId)
        {
            try
            {
                var data = resultManagementSerI.getGivenMarks(programId, semesterId, batchId, courseId,headId,subHeadId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Marks"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult saveEditedMarks(SaveEditedMarksResDto saveEditedMarksResDto)
        {
            try
            {
                resultManagementSerI.saveEditedMarks(saveEditedMarksResDto);
                return new JsonResult { Data = new { Status = "OK", Message = "Marks Have been Edited Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Editing Marks"} };
            }
        }

        [HttpGet]
        public JsonResult getMarksGivingOfHeadsInfo(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var data = resultManagementSerI.getHeadsMarksGivingInfoOfACourse(programId, semesterId, batchId, courseId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Retriving Head's Marks Info"};
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        

        [HttpPost]
        public JsonResult submitFinally(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                resultManagementSerI.submitFinally(programId, semesterId, batchId, courseId);
                return new JsonResult { Data = new { Status = "OK", Message = "Marks Have been Finally Submitted Successfully" } };
            }
            catch (Exception e)
            {
                return new JsonResult { Data = new { Status = "ERROR", Message = "Error has Occured While Finally Submission" + e } };
            }
        }

        [HttpGet]
        public JsonResult getFinalSubmissionInfoOfAllCourses(int programId, int semesterId, int batchId)
        {
            try
            {
                var data = resultManagementSerI.getFinalSubmissionInfoOfAllCourses(programId, semesterId, batchId);
                Object response = new { Status = "OK", Data = data };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Object response = new { Status = "ERROR", Message = "Error has Occured While Reading Distributed Marks Info" + e };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}