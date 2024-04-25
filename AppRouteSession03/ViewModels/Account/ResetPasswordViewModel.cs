using System.ComponentModel.DataAnnotations;

namespace AppRouteSession03.PL.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "<Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password Dosn`t math with Password")]
        public string ConfirmPassword { get; set; }


    }
}
