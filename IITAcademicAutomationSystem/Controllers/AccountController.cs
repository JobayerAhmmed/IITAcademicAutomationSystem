using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMapper;
using IITAcademicAutomationSystem.Areas.One;
using System.Collections.Generic;
using IITAcademicAutomationSystem.Areas.One.Models;
using System.IO;

namespace IITAcademicAutomationSystem.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService userService;
        private IRoleService roleService;
        private IProgramService programService;
        private IMapper Mapper;

        public AccountController()
        {
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("Password", "Invalid email or password, try again.");
                return View(model);
            }
            if (!UserManager.IsEmailConfirmed(user.Id))
            {
                return View("EmailNotConfirmed");
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return returnUrl == null ? RedirectToAction("Index", "Home", new { area = "" }) : RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("Password", "Invalid email or password, try again.");
                    return View(model);
            }
        }

        public ActionResult LoadProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);
                var profile = new ProfileViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Designation = user.Designation,
                    ImagePath = user.ImagePath
                };
                return PartialView("_LoginPartial", profile);
            }
            
            return PartialView("_LoginPartial");
        }

        public ActionResult SideMenu()
        {
            if (User.Identity.IsAuthenticated)
            {
                var menu = new SideMenuViewModel();
                menu.Programs = programService.GetPrograms();
                return PartialView("_SideMenuPartial", menu);
            }
            return null;   
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Designation = model.Designation,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail", "Account",
                        new { userId = user.Id, code = code },
                        protocol: Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id,
                        "Confirm your account",
                        "<h1>IIT Academic Automation System</h1><hr>" +
                        "<h3>Hello Mr. " + user.FullName + "</h3>" +
                        "<p>An account has been created for you using the email \"" + user.Email +
                        "\" in the IIT Academic Automation System.<p>" +
                        "Please <a href=\"" + callbackUrl + "\">confirm your account.</a>");

                    return View("DisplayEmail");
                    //return RedirectToAction("Index", "Base", new { area = "" });
                }
                AddErrors(result);
            }

            return View(model);
        }

        // Register Teacher
        [AllowAnonymous]
        public ActionResult TeacherRegister()
        {
            return View();
        }

        // POST: Register Teacher
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TeacherRegister(TeacherRegisterViewModel model, HttpPostedFileBase file)
        {
            string fileName = "";

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.Contains("image"))
                {
                    fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
                    file.SaveAs(HttpContext.Server.MapPath("~/Areas/One/Content/UserImage/")
                        + fileName);
                }
                else
                    ModelState.AddModelError("ImagePath", "Unsupported image format");
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    Designation = model.Designation,
                    PhoneNumber = model.PhoneNumber,
                    ProfileLink = model.ProfileLink,
                    ImagePath = fileName,
                    Status = "On Duty"
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    // Add to Teacher role
                    await UserManager.AddToRoleAsync(user.Id, "Teacher");

                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail", "Account",
                        new { userId = user.Id, code = code },
                        protocol: Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id,
                        "Confirm your account",
                        "<h1>IIT Academic Automation System</h1><hr>" +
                        "<h3>Hello Mr. " + user.FullName + "</h3>" +
                        "<p>An account has been created for you using the email \"" + user.Email +
                        "\" in the IIT Academic Automation System.<p>" +
                        "Please <a href=\"" + callbackUrl + "\">confirm your account.</a>");

                    return View("TeacherRegisterConfirmed");
                    //return RedirectToAction("Index", "Base", new { area = "" });
                }
                AddErrors(result);
            }

            return View(model);
        }

        // Teacher Index
        public ActionResult TeacherIndex()
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var teacherRoleId = roleService.GetRoleByName("Teacher").Id;

            var teachers = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == teacherRoleId)
                && u.IsDelete == false).OrderBy(o => o.FullName).ToList();

            IList<TeacherIndexViewModel> teachersToView = new List<TeacherIndexViewModel>();
            TeacherIndexViewModel teacherIndexViewModel = new TeacherIndexViewModel();
 
            foreach (var item in teachers)
            {
                teacherIndexViewModel.Id = item.Id;
                teacherIndexViewModel.FullName = item.FullName;
                teacherIndexViewModel.Designation = item.Designation;
                teacherIndexViewModel.Email = item.Email;
                teacherIndexViewModel.PhoneNumber = item.PhoneNumber;
                teacherIndexViewModel.ProfileLink = item.ProfileLink;
                teacherIndexViewModel.ImagePath = item.ImagePath;
                teacherIndexViewModel.Status = item.Status;
                teacherIndexViewModel.Roles = userService.GetUserRoles(item.Id);

                teachersToView.Add(teacherIndexViewModel);
                teacherIndexViewModel = new TeacherIndexViewModel();
            }

            return View(teachersToView);
        }

        // Teacher Leave Index
        public ActionResult TeacherAllIndex()
        {
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var teacherRoleId = roleService.GetRoleByName("Teacher").Id;

            var teachers = UserManager.Users.Where(u => u.Roles.Any(r => r.RoleId == teacherRoleId)).OrderBy(o => o.FullName).ToList();

            IList<TeacherIndexViewModel> teachersToView = new List<TeacherIndexViewModel>();
            TeacherIndexViewModel teacherIndexViewModel = new TeacherIndexViewModel();

            foreach (var item in teachers)
            {
                teacherIndexViewModel.Id = item.Id;
                teacherIndexViewModel.FullName = item.FullName;
                teacherIndexViewModel.Designation = item.Designation;
                teacherIndexViewModel.Email = item.Email;
                teacherIndexViewModel.PhoneNumber = item.PhoneNumber;
                teacherIndexViewModel.ProfileLink = item.ProfileLink;
                teacherIndexViewModel.ImagePath = item.ImagePath;
                teacherIndexViewModel.Status = item.Status;
                teacherIndexViewModel.Roles = userService.GetUserRoles(item.Id);

                teachersToView.Add(teacherIndexViewModel);
                teacherIndexViewModel = new TeacherIndexViewModel();
            }

            return View("TeacherAllIndex", teachersToView);
        }

        // Teacher Details
        public ActionResult TeacherDetails(string id)
        {
            var teacher = UserManager.FindById(id);
            var teacherDetails = new TeacherDetailsViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Designation = teacher.Designation,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                ProfileLink = teacher.ProfileLink,
                ImagePath = teacher.ImagePath,
                Status = teacher.Status
            };

            return View(teacherDetails);
        }

        // Delete Teacher
        public ActionResult TeacherDelete(string id)
        {
            var teacher = UserManager.FindById(id);
            var teacherDetails = new TeacherDetailsViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Designation = teacher.Designation,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                ProfileLink = teacher.ProfileLink,
                ImagePath = teacher.ImagePath,
                Status = teacher.Status
            };

            return View(teacherDetails);
        }

        // POST: Delete Teacher
        [HttpPost, ActionName("TeacherDelete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TeacherDeletePost(TeacherDetailsViewModel model)
        {
            ApplicationUser user = UserManager.FindById(model.Id);
            user.IsDelete = true;
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return View("TeacherDeleteConfirmed");
            }
            AddErrors(result);
            return View("TeacherDelete", model);
        }

        // Teacher Edit
        public ActionResult TeacherEdit(string id)
        {
            var teacher = UserManager.FindById(id);
            var teacherDetails = new TeacherEditViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Designation = teacher.Designation,
                Status = teacher.Status,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                ProfileLink = teacher.ProfileLink,
                ImagePath = teacher.ImagePath
            };

            return View(teacherDetails);
        }

        // POST: Teacher Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TeacherEdit(TeacherEditViewModel model, HttpPostedFileBase file)
        {
            string fileName = model.ImagePath;

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.Contains("image"))
                {
                    fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
                    file.SaveAs(HttpContext.Server.MapPath("~/Areas/One/Content/UserImage/")
                        + fileName);
                }
                else
                    ModelState.AddModelError("ImagePath", "Unsupported image format");
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(model.Id);

                user.FullName = model.FullName;
                user.Designation = model.Designation;
                user.Status = model.Status;
                user.PhoneNumber = model.PhoneNumber;
                user.ProfileLink = model.ProfileLink;
                user.ImagePath = fileName;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("TeacherEditConfirmed", model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        // Set Role
        public ActionResult TeacherSetRole(string id)
        {
            var user = UserManager.FindById(id);
            var userRoles = userService.GetUserRoles(id);
            var allRoles = roleService.ViewRoles();

            SetRoleViewModel model = new SetRoleViewModel();
            model.Id = id;
            model.FullName = user.FullName;
            model.Designation = user.Designation;

            var checkBoxListItems = new List<CheckBoxListItem>();
            foreach (var role in allRoles)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Value = role.Name,
                    Text = role.Name,
                    IsChecked = userRoles.Where(x => x.Name == role.Name).Any()
                });
            }
            model.Roles = checkBoxListItems;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherSetRole(SetRoleViewModel model)
        {
            var teacher = UserManager.FindById(model.Id);
            var teacherRoles = UserManager.GetRoles(teacher.Id);
            foreach (var role in teacherRoles)
            {
                UserManager.RemoveFromRole(teacher.Id, role);
            }

            IdentityResult result;
            foreach (var role in model.Roles)
            {
                if (role.IsChecked)
                {
                    result = UserManager.AddToRole(teacher.Id, role.Value);

                    if (result.Succeeded)
                        continue;

                    AddErrors(result);
                    return View(model);
                }
            }

            return View("TeacherSetRoleConfirmed", model);
        }

        // ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordFailed");
                }

                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code = code, email = user.Email }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id,
                    "Reset password",
                        "<h1>IIT Academic Automation System</h1><hr>" +
                        "<h3>Hello Mr. " + user.FullName + "</h3>" +
                        "<p>A request has been made to reset password.</p>" +
                        "Please <a href=\"" + callbackUrl + "\">click here to reset your password.</a>");

                return RedirectToAction("ForgotPasswordConfirmation", "Account", new { area = "" });
                //return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        // ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // Reset Email
        public ActionResult ResetEmail(string id)
        {
            ResetEmailViewModel model = new ResetEmailViewModel()
            {
                Id = id
            };
            return View(model);
        }

        // POST: Reset Email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetEmail(ResetEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.UserName = model.Email;
                user.Email = model.Email;

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(
                        "ConfirmEmail", "Account",
                        new { userId = user.Id, code = code },
                        protocol: Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id,
                        "Confirm your email",
                        "<h1>IIT Academic Automation System</h1><hr>" +
                        "<h3>Hello Mr. " + user.FullName + "</h3>" +
                        "<p>The email address for your account in the IIT Academic Automation System has been changed to \"" + user.Email +
                        "\".<p>" +
                        "Please <a href=\"" + callbackUrl + "\">confirm your email.</a>");

                    return View("ResetEmailConfirmed");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code, string email)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                Code = code,
                Email = email
            };

            return code == null ? View("Error") : View(model);
        }

        // ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordFailed", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // User exist
        public JsonResult UserExist(string Email)
        {
            if (!userService.UserExist(Email))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}