using System.ComponentModel.DataAnnotations;

namespace AppRouteSession03.PL.ViewModels.User
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "User Name Is Required")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "First Name Is Required")]
		[Display(Name ="Fisrt Name")]
		public string FName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]
		[Display(Name = "Last Name")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "<Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

		[Required(ErrorMessage = "Password is Required")]		
		[DataType(DataType.Password)]
		[Compare(nameof(Password) , ErrorMessage ="Confirm Password Dosn`t math with Password")]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
