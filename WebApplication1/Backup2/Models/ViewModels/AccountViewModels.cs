extern alias EF;
extern alias SCD;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
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
        [SCD::System.ComponentModel.DataAnnotations.Required]
        public string Provider { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        public string Email { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Password)]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Password")]
        public string Password { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
        public string Email { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Password)]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Password")]
        public string Password { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Password)]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Confirm password")]
        [SCD::System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
        public string Email { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Password)]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Password")]
        public string Password { get; set; }

        [SCD::System.ComponentModel.DataAnnotations.DataType(SCD::System.ComponentModel.DataAnnotations.DataType.Password)]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Confirm password")]
        [SCD::System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [SCD::System.ComponentModel.DataAnnotations.Required]
        [SCD::System.ComponentModel.DataAnnotations.EmailAddress]
        [SCD::System.ComponentModel.DataAnnotations.Display(Name = "Email")]
        public string Email { get; set; }
    }
}