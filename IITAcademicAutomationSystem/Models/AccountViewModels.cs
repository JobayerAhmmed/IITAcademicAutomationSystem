using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter full name.")]
        [StringLength(100, ErrorMessage = "Full name must be at least 5 characters long.", MinimumLength = 5)]
        [RegularExpression(@"([a-zA-Z][a-zA-Z\.\'\-\s]+)", ErrorMessage = "Please enter valid name.")]
        [Display(Name = "Full name")]
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

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
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
