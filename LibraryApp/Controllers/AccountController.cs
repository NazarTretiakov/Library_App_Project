using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Route("/register")]
        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("/register")]
        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO, string? returnUrl)
        {
            if (ModelState.IsValid == false)
            {
                return View(registerDTO);
            }

            User user = new User()
            {
                UserName = registerDTO.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());

                await _signInManager.SignInAsync(user, false);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }

                return View(registerDTO);
            }
        }

        [Route("/sign-in")]
        [HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("/sign-in")]
        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> SignIn(SignInDTO signInDTO, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(signInDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(signInDTO.UserName, signInDTO.Password, false, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError("Sign in", "Invalid username or password");
                return View(signInDTO);
            }
        }

        [Route("/logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
