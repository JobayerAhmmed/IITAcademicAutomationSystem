using IITAcademicAutomationSystem.Areas.One.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Models
{
    public class StudentRegisterViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public string StudentFile { get; set; }
    }

    public class StudentRegisterConfirmedViewModel
    {
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public List<string> Names { get; set; }
        public List<string> Rolls { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Messages { get; set; }

        public StudentRegisterConfirmedViewModel()
        {
            Names = new List<string>();
            Rolls = new List<string>();
            Emails = new List<string>();
            Messages = new List<string>();
        }
    }

    public class StudentIndexViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Roll { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string ProgramName { get; set; }
        public int BatchNo { get; set; }

    }

    public class StudentResultViewModel : StudentIndexViewModel
    {
        public double GPA { get; set; }
    }

    public class StudentDetailsViewModel
    {
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public string ProgramName { get; set; }
        public int BatchNoOriginal { get; set; }
        public int BatchNoCurrent { get; set; }
        public int SemesterNo { get; set; }
        public string OriginalRoll { get; set; }
        public string CurrentRoll { get; set; }
        public string RegistrationNo { get; set; }
        public string AdmissionSession { get; set; }
        public string CurrentSession { get; set; }
        public string GuardianPhone { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(100, ErrorMessage = "Name must be at least 5 characters long.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z][a-zA-Z\.\'\-\s]+)", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email-address")]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("UserExist", "Account", ErrorMessage = "User already exists with this email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [StringLength(100, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(50, ErrorMessage = "Designation must be at list 6 characters long.", MinimumLength = 6)]
        [RegularExpression(@"([a-zA-Z0-9 ,\.\&\'\-]+)", ErrorMessage = "Designation seems to incorrect.")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [StringLength(14, ErrorMessage = "Please enter valid phone number.", MinimumLength = 3)]
        [RegularExpression(@"(\+?[0-9]+)", ErrorMessage = "Phone number can only contains digits and a plus symbol.")]
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
    }

    public class OtherIndexViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }

    public class TeacherRegisterViewModel
    {
        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(100, ErrorMessage = "Name must be at least 5 characters long.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z][a-zA-Z\.\'\-\s]+)", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Designation must be at list 6 characters long.", MinimumLength = 6)]
        [RegularExpression(@"([a-zA-Z0-9 ,\.\&\'\-]+)", ErrorMessage = "Designation seems to incorrect.")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email-address")]
        [Display(Name = "Email")]
        [System.Web.Mvc.Remote("UserExist", "Account", ErrorMessage = "Teacher already exists with this email.")]
        public string Email { get; set; }

        [StringLength(14, ErrorMessage = "Please enter valid phone number.", MinimumLength = 3)]
        [RegularExpression(@"(\+?[0-9]+)", ErrorMessage = "Phone number can only contains digits and a plus symbol.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [StringLength(100, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string ProfileLink { get; set; }

        public string ImagePath { get; set; }
    }

    public class TeacherEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(100, ErrorMessage = "Name must be at least 5 characters long.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z][a-zA-Z\.\'\-\s]+)", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Designation must be at list 6 characters long.", MinimumLength = 6)]
        [RegularExpression(@"([a-zA-Z0-9 ,\.\&\'\-]+)", ErrorMessage = "Designation seems to incorrect.")]
        public string Designation { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }

        [StringLength(14, ErrorMessage = "Please enter valid phone number.", MinimumLength = 3)]
        [RegularExpression(@"(\+?[0-9]+)", ErrorMessage = "Phone number can only contains digits and a plus symbol.")]
        public string PhoneNumber { get; set; }

        public string ProfileLink { get; set; }

        public string ImagePath { get; set; }
    }

    public class TeacherIndexViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileLink { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }

    public class TeacherDetailsViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileLink { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
    }

    public class SetRoleViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public List<CheckBoxListItem> Roles { get; set; }

        public SetRoleViewModel()
        {
            Roles = new List<CheckBoxListItem>();
        }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class UserProfileViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileLink { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public string ProgramName { get; set; }
        public int BatchNoOriginal { get; set; }
        public int BatchNoCurrent { get; set; }
        public int SemesterNo { get; set; }
        public string OriginalRoll { get; set; }
        public string CurrentRoll { get; set; }
        public string RegistrationNo { get; set; }
        public string AdmissionSession { get; set; }
        public string CurrentSession { get; set; }
        public string GuardianPhone { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
    }

    public class UserProfileEditViewModel
    {
        public string Id { get; set; }
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter name.")]
        [StringLength(100, ErrorMessage = "Name must be at least 5 characters long.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z][a-zA-Z\.\'\-\s]+)", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Name")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Designation must be at list 6 characters long.", MinimumLength = 6)]
        [RegularExpression(@"([a-zA-Z0-9 ,\.\&\'\-]+)", ErrorMessage = "Designation seems to incorrect.")]
        public string Designation { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }

        [StringLength(14, ErrorMessage = "Please enter valid phone number.", MinimumLength = 3)]
        [RegularExpression(@"(\+?[0-9]+)", ErrorMessage = "Phone number can only contains digits and a plus symbol.")]
        public string PhoneNumber { get; set; }

        [StringLength(300, ErrorMessage = "URL should not exceed 300 characters")]
        public string ProfileLink { get; set; }

        public string ImagePath { get; set; }

        public string ProgramName { get; set; }
        public int BatchNoOriginal { get; set; }
        public int BatchNoCurrent { get; set; }
        public int SemesterNo { get; set; }
        public string OriginalRoll { get; set; }
        public string CurrentRoll { get; set; }
        public string RegistrationNo { get; set; }
        public string AdmissionSession { get; set; }
        public string CurrentSession { get; set; }

        [StringLength(14, ErrorMessage = "Please enter valid phone number.", MinimumLength = 3)]
        [RegularExpression(@"(\+?[0-9]+)", ErrorMessage = "Phone number can only contains digits and a plus symbol.")]
        public string GuardianPhone { get; set; }

        [StringLength(300, ErrorMessage = "Address should not exceed 300 characters.")]
        public string CurrentAddress { get; set; }

        [StringLength(300, ErrorMessage = "Address should not exceed 300 characters.")]
        public string PermanentAddress { get; set; }

        public int ProgramId { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ResetEmailViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Remote("UserExist", "Account", ErrorMessage = "User exists with this email.")]
        public string Email { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Confirm Email")]
        [System.ComponentModel.DataAnnotations.Compare("Email", ErrorMessage = "The email and confirm email do not match.")]
        public string ConfirmEmail { get; set; }
        public int ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}
