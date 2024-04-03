using AppRouteSession03.DAL.Models;
using AppRouteSession03.PL.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppRouteSession03.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager ) 
        {
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

        #endregion

    }
}
