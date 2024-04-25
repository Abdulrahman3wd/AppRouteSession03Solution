using AppRouteSession03.Controllers;
using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.Services.EmailSender;
using AppRouteSession03.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AppRouteSession03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly IEmailSender _emailSender;
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
            IEmailSender emailSender,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager ,
            SignInManager<ApplicationUser> signInManager ) 
        {
		    _emailSender = emailSender;
			_configuration = configuration;
			_userManager = userManager;
			_signInManager = signInManager;
		}
        #region Sign Up 


        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user is null)
                {
					user = new ApplicationUser()
					{
						FName = model.FName,
						LName = model.LName,
						UserName = model.Username,
						Email = model.Email,
						IsAgree = model.IsAgree,
					};
                    var Result =    await  _userManager.CreateAsync(user , model.Password);

                    if (Result.Succeeded)                   
                        return RedirectToAction(nameof(SignIn));

                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        
                    }

                }
                ModelState.AddModelError(string.Empty , "This user Name is Already in Use for Another Account ");


            }
            return View(model);
        }
        #endregion

        #region SignIn
        public  IActionResult SignIn()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SignIn (SignInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account Is Loked");
						if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index) , "Home");
                        if (result.IsNotAllowed)
                            ModelState.AddModelError(string.Empty, "Your Account Is Not Comfirmed Yet !!");


					}
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
            }
            return View(model);
        }

        #endregion

        #region Sign Out

        public async new Task< IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var ResetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, token = ResetPasswordToken });

                    await _emailSender.SendAsync(
                        from: _configuration["EmailSettings:SenderEmail"],
                        recipients: model.Email,
                        subject: "Reset Your Password",
                        body: resetPasswordUrl);
                    return RedirectToAction(nameof(CheckYourInput));
                }
                ModelState.AddModelError(string.Empty, "There is No Account With This Email!!");

            }
            return View(model);
        }
        public IActionResult CheckYourInput()
        {
            return View();
        }
        #endregion
        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string email , string token) 
        {
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();  
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    await _userManager.ResetPasswordAsync(user , token , model.NewPassword);
                    return RedirectToAction(nameof(SignIn));
                }
                ModelState.AddModelError(string.Empty, "Url Is Not Valid");

            }

            return View(model);
        }
		#endregion






	}
}
