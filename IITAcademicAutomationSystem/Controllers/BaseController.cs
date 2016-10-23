using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Controllers
{
    public class BaseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: One/Base
        public ActionResult Index()
        {
            return View("IndexAdmin");
        }
        public ActionResult SideMenu()
        {
            var menu = new SideMenuViewModel();
            menu.Programs = unitOfWork.ProgramRepository.GetPrograms();
            return PartialView("_SideMenuPartial", menu);
        }
    }
}