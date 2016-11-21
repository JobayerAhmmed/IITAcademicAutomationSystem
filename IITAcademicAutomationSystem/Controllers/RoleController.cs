using AutoMapper;
using IITAcademicAutomationSystem.Areas.One;
using IITAcademicAutomationSystem.Areas.One.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Controllers
{
    public class RoleController : Controller
    {
        private IRoleService roleService;
        private IMapper Mapper;

        public RoleController()
        {
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        // Index
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult Index()
        {
            IEnumerable<IdentityRole> roles = roleService.ViewRoles();
            if (roles == null)
                return HttpNotFound();

            IEnumerable<RoleIndexViewModel> rolesToView =
                Mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleIndexViewModel>>(roles);

            return View(rolesToView);
        }

        // Details
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult Details(string id)
        {
            IdentityRole role = roleService.GetRoleById(id);
            if (role == null)
                return HttpNotFound();
            RoleDetailsViewModel roleDetailsViewModel = new RoleDetailsViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleDetailsViewModel);
        }

        // Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RoleCreateViewModel roleViewModel)
        {
            IdentityRole roleToCreate = new IdentityRole
            {
                Name = roleViewModel.Name
            };

            string roleId = roleService.CreateRole(roleToCreate);

            if (roleId != null)
            {
                IdentityRole role = roleService.GetRoleById(roleId);
                RoleDetailsViewModel roleToView = new RoleDetailsViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View("CreateConfirmed", roleToView);
            }

            return View();
        }

        // Edit
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            IdentityRole role = roleService.GetRoleById(id);
            RoleEditViewModel roleEditVM = new RoleEditViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(roleEditVM);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(RoleEditViewModel model)
        {
            IdentityRole role = new IdentityRole
            {
                Id = model.Id,
                Name = model.Name
            };

            if (roleService.EditRole(role))
            {
                var roleDetails = new RoleDetailsViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View("EditConfirmed", roleDetails);
            }

            return View(model);
        }

        // RoleExist
        [Authorize(Roles = "Admin")]
        public JsonResult RoleExist(string Name)
        {
            if (!roleService.RoleExist(Name))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // Role Exist except Current
        [Authorize(Roles = "Admin")]
        public JsonResult RoleExistExcept(string Name, string Id)
        {
            if (!roleService.RoleExistExcept(Name, Id))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}