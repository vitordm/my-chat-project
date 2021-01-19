using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyChat.Application.Dto.Requests;
using MyChat.Application.Service.Contracts;
using MyChat.Domain.Auth;
using System.Threading.Tasks;

namespace MyChat.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly IAuthService authService;

        public AuthController(
            IAuthService authService,
            SignInManager<AppUser> signInManager
        )
        {
            this.authService = authService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserRequest model)
        {
            if (ModelState.IsValid)
            {
                var registerResult = await authService.RegisterNewUserAsync(model);

                if (registerResult.Succeeded)
                {
                    var user = await authService.GetUserByUserNameAsync(model.UserName);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Wrong UserName or Password");
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");


        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


    }
}
