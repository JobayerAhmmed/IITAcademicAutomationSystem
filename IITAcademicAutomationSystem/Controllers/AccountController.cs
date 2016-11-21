using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.Areas.One.Services;
using AutoMapper;
using IITAcademicAutomationSystem.Areas.One;
using System.Collections.Generic;
using IITAcademicAutomationSystem.Areas.One.Models;
using System.IO;
using Excel;
using System.Data;
using System.Text.RegularExpressions;

namespace IITAcademicAutomationSystem.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IUserService userService;
        private IRoleService roleService;
        private IStudentService studentService;
        private IProgramService programService;
        private IBatchService batchService;
        private ISemesterService semesterService;
        private ICourseService courseService;
        private IMapper Mapper;

        public AccountController()
        {
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            studentService = new StudentService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            batchService = new BatchService(ModelState, new DAL.UnitOfWork());
            semesterService = new SemesterService(ModelState, new DAL.UnitOfWork());
            courseService = new CourseService(ModelState, new DAL.UnitOfWork());
            Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            userService = new UserService(ModelState, new DAL.UnitOfWork());
            roleService = new RoleService(ModelState, new DAL.UnitOfWork());
            studentService = new StudentService(ModelState, new DAL.UnitOfWork());
            programService = new ProgramService(ModelState, new DAL.UnitOfWork());
            batchService = new BatchService(ModelState, new DAL.UnitOfWork());
            semesterService = new SemesterService(ModelState, new DAL.UnitOfWork());
            courseService = new CourseService(ModelState, new DAL.UnitOfWork());
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

        // Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("Login", "Account", new { area = "", returnUrl = returnUrl });
            }
            ViewBag.ReturnUrl = null;
            return View();
        }

        // POST: Login
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

        // Warning: May give error
        //[Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
        public ActionResult LoadProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);

                var profile = new ProfileViewModel();
                profile.Id = user.Id;
                profile.FullName = user.FullName;
                profile.Designation = user.Designation;
                profile.ImagePath = user.ImagePath;

                return PartialView("_LoginPartial", profile);
            }

            //return PartialView("_LoginPartial");
            return null;
        }

        //[Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
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

        // LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        // Student Edit
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult StudentEdit(int studentId)
        {
            var student = studentService.ViewStudent(studentId);
            var user = UserManager.FindById(student.UserId);
            var model = new UserProfileEditViewModel();

            var program = programService.ViewProgram(student.ProgramId);
            var semester = semesterService.ViewSemester(student.SemesterId);
            var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
            var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);

            model.ProgramId = program.Id;
            model.BatchId = batchCurrent.Id;
            model.BatchNo = batchCurrent.BatchNo;

            model.Id = user.Id;
            model.StudentId = studentId;
            model.FullName = user.FullName;
            model.Designation = user.Designation;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ProfileLink = user.ProfileLink;
            model.ImagePath = user.ImagePath;
            model.Status = user.Status;

            model.AdmissionSession = student.AdmissionSession;
            model.BatchNoCurrent = batchCurrent.BatchNo;
            model.BatchNoOriginal = batchOriginal.BatchNo;
            model.CurrentAddress = student.CurrentAddress;
            model.CurrentRoll = student.CurrentRoll;
            model.CurrentSession = student.CurrentSession;
            model.GuardianPhone = student.GuardianPhone;
            model.OriginalRoll = student.OriginalRoll;
            model.PermanentAddress = student.PermanentAddress;
            model.ProgramName = program.ProgramName;
            model.RegistrationNo = student.RegistrationNo;
            model.SemesterNo = semester.SemesterNo;

            return View(model);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> StudentEdit(UserProfileEditViewModel model, HttpPostedFileBase file)
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
                user.PhoneNumber = model.PhoneNumber;
                user.ImagePath = fileName;
                user.Status = model.Status;

                Student student = studentService.GetStudentByUserId(model.Id);
                student.OriginalRoll = model.OriginalRoll;
                student.CurrentRoll = model.CurrentRoll;
                student.RegistrationNo = model.RegistrationNo;
                student.AdmissionSession = model.AdmissionSession;
                student.CurrentSession = model.CurrentSession;
                student.GuardianPhone = model.GuardianPhone;
                student.CurrentAddress = model.CurrentAddress;
                student.PermanentAddress = model.PermanentAddress;

                if (!studentService.EditStudent(student))
                {
                    ModelState.AddModelError("", "Unable to save student, please try again.");
                    return View(model);
                }

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("StudentEditConfirmed", model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
        public ActionResult UserProfile(string id)
        {
            var user = UserManager.FindById(id);
            var model = new UserProfileViewModel();

            model.Id = user.Id;
            model.FullName = user.FullName;
            model.Designation = user.Designation;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ProfileLink = user.ProfileLink;
            model.ImagePath = user.ImagePath;
            model.Status = user.Status;

            if (User.IsInRole("Student"))
            {
                var student = studentService.GetStudentByUserId(id);
                var program = programService.ViewProgram(student.ProgramId);
                var semester = semesterService.ViewSemester(student.SemesterId);
                var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
                var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);

                model.AdmissionSession = student.AdmissionSession;
                model.BatchNoCurrent = batchCurrent.BatchNo;
                model.BatchNoOriginal = batchOriginal.BatchNo;
                model.CurrentAddress = student.CurrentAddress;
                model.CurrentRoll = student.CurrentRoll;
                model.CurrentSession = student.CurrentSession;
                model.GuardianPhone = student.GuardianPhone;
                model.OriginalRoll = student.OriginalRoll;
                model.PermanentAddress = student.PermanentAddress;
                model.ProgramName = program.ProgramName;
                model.RegistrationNo = student.RegistrationNo;
                model.SemesterNo = semester.SemesterNo;
            }

            return View(model);
        }

        [Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
        public ActionResult UserProfileEdit(string id)
        {
            var user = UserManager.FindById(id);
            var model = new UserProfileEditViewModel();

            model.Id = user.Id;
            model.FullName = user.FullName;
            model.Designation = user.Designation;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.ProfileLink = user.ProfileLink;
            model.ImagePath = user.ImagePath;
            model.Status = user.Status;

            if (User.IsInRole("Student"))
            {
                var student = studentService.GetStudentByUserId(id);
                var program = programService.ViewProgram(student.ProgramId);
                var semester = semesterService.ViewSemester(student.SemesterId);
                var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
                var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);

                model.AdmissionSession = student.AdmissionSession;
                model.BatchNoCurrent = batchCurrent.BatchNo;
                model.BatchNoOriginal = batchOriginal.BatchNo;
                model.CurrentAddress = student.CurrentAddress;
                model.CurrentRoll = student.CurrentRoll;
                model.CurrentSession = student.CurrentSession;
                model.GuardianPhone = student.GuardianPhone;
                model.OriginalRoll = student.OriginalRoll;
                model.PermanentAddress = student.PermanentAddress;
                model.ProgramName = program.ProgramName;
                model.RegistrationNo = student.RegistrationNo;
                model.SemesterNo = semester.SemesterNo;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Batch Coordinator, Program Officer Evening, Program Officer Regular, Student, Teacher")]
        public async Task<ActionResult> UserProfileEdit(UserProfileEditViewModel model, HttpPostedFileBase file)
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

                user.Designation = model.Designation;
                user.PhoneNumber = model.PhoneNumber;
                user.ProfileLink = model.ProfileLink;
                user.ImagePath = fileName;

                if (User.IsInRole("Student"))
                {
                    Student student = studentService.GetStudentByUserId(model.Id);
                    student.GuardianPhone = model.GuardianPhone;
                    student.CurrentAddress = model.CurrentAddress;
                    student.PermanentAddress = model.PermanentAddress;

                    if(!studentService.EditStudent(student))
                    {
                        ModelState.AddModelError("", "Unable to save student, please try again.");
                        return View(model);
                    }
                }

                model.ImagePath = fileName;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("UserProfileEditConfirmed", model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        // Register
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> Register(RegisterViewModel model, HttpPostedFileBase file)
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
                    ImagePath = fileName,
                    Status = "On Duty"
                };
                var result = await UserManager.CreateAsync(user, "iit123");
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

                    return View("RegisterConfirmed");
                }
                AddErrors(result);
            }

            return View(model);
        }

        // Student Register
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult StudentRegister(int programId, int batchId)
        {
            var program = programService.ViewProgram(programId);
            var batch = batchService.ViewBatch(batchId);
            var model = new StudentRegisterViewModel()
            {
                ProgramId = programId,
                ProgramName = program.ProgramName,
                BatchId = batchId,
                BatchNo = batch.BatchNo,
                StudentFile = "Student-Info.xlsx"
            };
            return View(model);
        }

        // POST: Student Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> StudentRegister(StudentRegisterViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    Stream stream = file.InputStream;

                    IExcelDataReader reader = null;

                    if (file.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("StudentFile", "File format is not supported.");
                        return View(model);
                    }

                    DataSet result = null;
                    DataTable dataTable = null;

                    try
                    {
                        reader.IsFirstRowAsColumnNames = true;
                        result = reader.AsDataSet();

                        dataTable = result.Tables[0];
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("StudentFile", "File does not contain any student information row.");
                        return View(model);
                    }

                    if (dataTable.Columns.Count != 9)
                    {
                        ModelState.AddModelError("StudentFile", "Number of columns of the file should be 9.");
                        return View(model);
                    }

                    var columns = dataTable.Columns;
                    var semester = semesterService.GetFirstSemester(model.ProgramId);

                    var studentRegisterConfirmedModel = new StudentRegisterConfirmedViewModel();
                    studentRegisterConfirmedModel.ProgramId = model.ProgramId;
                    studentRegisterConfirmedModel.ProgramName = model.ProgramName;
                    studentRegisterConfirmedModel.BatchId = model.BatchId;
                    studentRegisterConfirmedModel.BatchNo = model.BatchNo;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        if (IsRowEmpty(row))
                        {
                            continue;
                        }
                        var user = new ApplicationUser();
                        var student = new Student();

                        user.FullName = row[columns[2].ColumnName].ToString();
                        user.Designation = "Student";
                        user.Email = row[columns[4].ColumnName].ToString();
                        user.UserName = user.Email;
                        user.PhoneNumber = row[columns[5].ColumnName].ToString();
                        user.Status = "Active";

                        student.ProgramId = model.ProgramId;
                        student.BatchIdOriginal = model.BatchId;
                        student.BatchIdCurrent = model.BatchId;
                        student.SemesterId = semester.Id;
                        student.OriginalRoll = row[columns[0].ColumnName].ToString();
                        student.CurrentRoll = row[columns[0].ColumnName].ToString();
                        student.RegistrationNo = row[columns[1].ColumnName].ToString();
                        student.AdmissionSession = row[columns[3].ColumnName].ToString();
                        student.CurrentSession = row[columns[3].ColumnName].ToString();
                        student.GuardianPhone = row[columns[6].ColumnName].ToString();
                        student.CurrentAddress = row[columns[7].ColumnName].ToString();
                        student.PermanentAddress = row[columns[8].ColumnName].ToString();

                        studentRegisterConfirmedModel.Names.Add(user.FullName);
                        studentRegisterConfirmedModel.Emails.Add(user.Email);
                        studentRegisterConfirmedModel.Rolls.Add(student.OriginalRoll);

                        string validationMessage = ValidateStudent(user, student);

                        if (userService.UserExist(user.Email))
                        {
                            studentRegisterConfirmedModel.Messages.Add("Student exists with this email.");
                        }
                        else if (validationMessage == "")
                        {
                            // Create user
                            var createResult = await UserManager.CreateAsync(user, "iit123");
                            if (createResult.Succeeded)
                            {
                                await UserManager.AddToRoleAsync(user.Id, "Student");

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

                                // Create student
                                student.UserId = user.Id;
                                var studentCreateResult = studentService.CreateStudent(student);
                                if (studentCreateResult == -1)
                                {
                                    studentRegisterConfirmedModel.Messages.Add("Unable to create student account");
                                }
                                else
                                {
                                    studentRegisterConfirmedModel.Messages.Add("Successful");
                                }
                            } // end create user
                            else
                            {
                                studentRegisterConfirmedModel.Messages.Add("Unable to create user account");
                            }
                        }
                        else
                        {
                            studentRegisterConfirmedModel.Messages.Add(validationMessage);
                        }
                    } // end of excel file

                    return View("StudentRegisterConfirmed", studentRegisterConfirmedModel);
                }
                else
                {
                    ModelState.AddModelError("StudentFile", "Please upload student information file.");
                }
            }

            return View(model);
        }
        protected bool IsRowEmpty(DataRow dr)
        {
            if (dr == null)
            {
                return true;
            }
            else
            {
                foreach (var value in dr.ItemArray)
                {
                    if (value.ToString() != "")
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        protected string ValidateStudent(ApplicationUser user, Student student)
        {
            string message = "";

            if (student.OriginalRoll.Trim().Length == 0)
            {
                message = message + "Roll is required  ";
            }
            if (student.RegistrationNo.Trim().Length == 0)
            {
                message += "Registration no. is required  ";
            }
            if (user.FullName.Trim().Length == 0)
            {
                message += "Name is required  ";
            }
            if (user.Email.Trim().Length == 0)
            {
                message += "Email is required  ";
            }
            if (!Regex.IsMatch(user.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                message += "Invalid email  ";
            }

            return message;
        }

        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public FileResult Download(string fileName)
        {
            return File("~/Areas/One/Content/Student/" + fileName,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // Student Index
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Teacher")]
        public ActionResult StudentIndex()
        {
            var students = studentService.GetAllStudents();

            IList<StudentIndexViewModel> studentsToView = new List<StudentIndexViewModel>();
            StudentIndexViewModel studentIndexViewModel = new StudentIndexViewModel();
            ApplicationUser user;

            foreach (var item in students)
            {
                user = UserManager.FindById(item.UserId);

                studentIndexViewModel.Id = item.Id;
                studentIndexViewModel.UserId = item.UserId;
                studentIndexViewModel.Roll = item.CurrentRoll;
                studentIndexViewModel.FullName = user.FullName;
                studentIndexViewModel.Email = user.Email;
                studentIndexViewModel.PhoneNumber = user.PhoneNumber;
                studentIndexViewModel.ImagePath = user.ImagePath;

                studentsToView.Add(studentIndexViewModel);
                studentIndexViewModel = new StudentIndexViewModel();
            }

            return View(studentsToView);
        }

        // Student Details
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Teacher")]
        public ActionResult StudentDetails(int id)
        {
            var student = studentService.ViewStudent(id);
            var user = UserManager.FindById(student.UserId);
            var program = programService.ViewProgram(student.ProgramId);
            var batchCurrent = batchService.ViewBatch(student.BatchIdCurrent);
            var batchOriginal = batchService.ViewBatch(student.BatchIdOriginal);
            var semester = semesterService.ViewSemester(student.SemesterId);

            StudentDetailsViewModel model = new StudentDetailsViewModel();
            model.AdmissionSession = student.AdmissionSession;
            model.BatchNoCurrent = batchCurrent.BatchNo;
            model.BatchNoOriginal = batchOriginal.BatchNo;
            model.CurrentAddress = student.CurrentAddress;
            model.CurrentRoll = student.CurrentRoll;
            model.CurrentSession = student.CurrentSession;
            model.Designation = user.Designation;
            model.Email = user.Email;
            model.FullName = user.FullName;
            model.GuardianPhone = student.GuardianPhone;
            model.ImagePath = user.ImagePath;
            model.OriginalRoll = student.OriginalRoll;
            model.PermanentAddress = student.PermanentAddress;
            model.PhoneNumber = user.PhoneNumber;
            model.ProgramName = program.ProgramName;
            model.RegistrationNo = student.RegistrationNo;
            model.SemesterNo = semester.SemesterNo;
            model.Status = user.Status;

            return View(model);
        }

        // Register Teacher
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult TeacherRegister()
        {
            return View();
        }

        // POST: Register Teacher
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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
                var result = await UserManager.CreateAsync(user, "iit123");
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult TeacherIndex()
        {
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

        // Teacher All Index
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
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

        // Teacher Edit
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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

        // Delete Teacher
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> TeacherDeletePost(TeacherDetailsViewModel model)
        {
            ApplicationUser user = UserManager.FindById(model.Id);
            user.IsDelete = true;
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ViewBag.UserId = user.Id;
                return View("TeacherDeleteConfirmed");
            }
            AddErrors(result);
            return View("TeacherDelete", model);
        }

        // Teacher Set Role
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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
                if (role.Name == "Student" || role.Name == "Batch Coordinator")
                {
                    continue;
                }
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

        // POST: Teacher Set Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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

        // Teacher courses
        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherCourses()
        {
            string teacherId = User.Identity.GetUserId();
            var courseSemesters = courseService.GetCourseSemestersOfTeacher(teacherId);
            Course course;
            Semester semester;
            Batch batch;
            Program program;

            List<TeacherCourseViewModel> model = new List<TeacherCourseViewModel>();
            TeacherCourseViewModel tc = new TeacherCourseViewModel();
            foreach (var item in courseSemesters)
            {
                course = courseService.ViewCourse(item.CourseId);
                semester = semesterService.ViewSemester(item.SemesterId);
                batch = batchService.ViewBatch(item.BatchId);
                program = programService.ViewProgram(batch.ProgramId);

                tc.TeacherId = teacherId;
                tc.CourseId = item.CourseId;
                tc.SemesterId = item.SemesterId;
                tc.BatchId = item.BatchId;
                tc.CourseCode = course.CourseCode;
                tc.CourseTitle = course.CourseTitle;
                tc.SemesterNo = semester.SemesterNo;
                tc.BatchNo = batch.BatchNo;
                tc.ProgramName = program.ProgramName;

                model.Add(tc);
                tc = new TeacherCourseViewModel();
            }

            return View(model);
        }

        // User Index
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
        public ActionResult OtherIndex()
        {
            var teacherRoleId = roleService.GetRoleByName("Teacher").Id;
            var studentRoleId = roleService.GetRoleByName("Student").Id;

            var users = UserManager.Users.Where(u =>
                u.IsDelete == false).OrderBy(o => o.FullName).ToList();

            List<ApplicationUser> persedUsers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (user.Roles.Any(r => r.RoleId == teacherRoleId || r.RoleId == studentRoleId))
                {
                    continue;
                }
                persedUsers.Add(user);
            }

            IList<OtherIndexViewModel> usersToView = new List<OtherIndexViewModel>();
            OtherIndexViewModel model = new OtherIndexViewModel();

            foreach (var item in persedUsers)
            {
                model.Id = item.Id;
                model.FullName = item.FullName;
                model.Designation = item.Designation;
                model.Email = item.Email;
                model.PhoneNumber = item.PhoneNumber;
                model.ImagePath = item.ImagePath;
                model.Status = item.Status;
                model.Roles = userService.GetUserRoles(item.Id);

                usersToView.Add(model);
                model = new OtherIndexViewModel();
            }

            return View(usersToView);
        }

        // User Delete
        [Authorize(Roles = "Admin")]
        public ActionResult UserDelete(string id)
        {
            var teacher = UserManager.FindById(id);
            var teacherDetails = new TeacherDetailsViewModel
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Designation = teacher.Designation,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                ImagePath = teacher.ImagePath,
                Status = teacher.Status
            };

            return View(teacherDetails);
        }

        // POST: Delete User
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UserDeletePost(TeacherDetailsViewModel model)
        {
            ApplicationUser user = UserManager.FindById(model.Id);
            user.IsDelete = true;
            IdentityResult result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                ViewBag.UserId = user.Id;
                return View("UserDeleteConfirmed");
            }
            AddErrors(result);
            return View("UserDelete", model);
        }

        // User Edit
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult UserEdit(string id)
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
                ImagePath = teacher.ImagePath
            };

            return View(teacherDetails);
        }

        // POST: User Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> UserEdit(TeacherEditViewModel model, HttpPostedFileBase file)
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
                user.ImagePath = fileName;

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return View("UserEditConfirmed", model);
                }
                AddErrors(result);
            }

            return View(model);
        }

        // User Set Role
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult OtherSetRole(string id)
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
                if (role.Name == "Student" || role.Name == "Batch Coordinator")
                {
                    continue;
                }
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

        // POST: User Set Role
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult OtherSetRole(SetRoleViewModel model)
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

            return View("OtherSetRoleConfirmed", model);
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> ResetEmail(ResetEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.UserName = model.Email;
                user.Email = model.Email;
                user.EmailConfirmed = false;

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

                    ViewBag.UserId = user.Id;
                    return View("ResetEmailConfirmed");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // Reset Email
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult ResetEmailUser(string id)
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> ResetEmailUser(ResetEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.UserName = model.Email;
                user.Email = model.Email;
                user.EmailConfirmed = false;

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

                    ViewBag.UserId = user.Id;
                    return View("ResetEmailConfirmedForUser");
                }
                AddErrors(result);
            }
            return View(model);
        }

        // Reset Email Student
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public ActionResult ResetEmailStudent(int programId, int batchId, string id)
        {
            Program program = programService.ViewProgram(programId);
            Batch batch = batchService.ViewBatch(batchId);

            ResetEmailViewModel model = new ResetEmailViewModel();
            model.Id = id;

            model.ProgramId = programId;
            model.ProgramName = program.ProgramName;
            model.BatchId = batchId;
            model.BatchNo = batch.BatchNo;

            return View(model);
        }

        // POST: Reset Email Student
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular")]
        public async Task<ActionResult> ResetEmailStudent(ResetEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.UserName = model.Email;
                user.Email = model.Email;
                user.EmailConfirmed = false;

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

                    ViewBag.UserId = user.Id;
                    ViewBag.ProgramId = model.ProgramId;
                    ViewBag.ProgramName = model.ProgramName;
                    ViewBag.BatchId = model.BatchId;
                    ViewBag.BatchNo = model.BatchNo;

                    return View("ResetEmailConfirmedForStudent");
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
        [Authorize(Roles = "Admin, Program Officer Evening, Program Officer Regular, Batch Coordinator, Student, Teacher")]
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