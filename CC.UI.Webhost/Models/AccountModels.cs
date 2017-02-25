using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DataAnnotationsExtensions;

namespace CC.UI.Webhost.Models
{
    public class UserDisplayProfileModel
    {
        public string DisplayFirstName { get; set; }
        public string DisplayLastName { get; set; }
        public string Avatar { get; set; }
        public bool IsLoggedIn { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "new password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "first name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "e-mail address")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "@twitter")]
        [StringLength(15, ErrorMessage = "{0} can be at most 16 characters long.")]
        public string Twitter { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "avatar")]
        public HttpPostedFileBase Avatar { get; set; }

        [Required]
        [Display(Name = "city, state")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "t-shirt size")]
        public int TShirtSizeId { get; set; }

        public string ImageUrl { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required]
        [Email]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "e-mail address")]
        public string Email { get; set; }
    }
}
