using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersGetterService _usersGetterService;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(IUsersGetterService usersGetterService, UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            _usersGetterService = usersGetterService;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [Route("/register")]
        [HttpGet]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/register")]
        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO, string? returnUrl)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (ModelState.IsValid == false)
            {
                return View(registerDTO);
            }

            User user = new User()
            {
                UserName = registerDTO.UserName,
                DateOfRegistration = DateTime.Now,
                ProfilePhotoPath = "~/Images/Icons/User_Profile_Photo.svg"
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());

                await _signInManager.SignInAsync(user, false);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirectPermanent(returnUrl);
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
        public async Task<IActionResult> SignIn()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/sign-in")]
        [HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> SignIn(SignInDTO signInDTO, string? returnUrl)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(signInDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(signInDTO.UserName, signInDTO.Password, false, false);

            if (result.Succeeded)
            {
                User signedUser = await _usersGetterService.GetUserByUsername(signInDTO.UserName);

                if (await _userManager.IsInRoleAsync(signedUser, UserRoleOptions.Librarian.ToString()))
                {
                    return RedirectToAction("Index", "Librarian", new { area = "Librarian"});
                }
                else if (await _userManager.IsInRoleAsync(signedUser, UserRoleOptions.Admin.ToString()))
                {
                    return RedirectToAction("Index", "Admin", new { area = "Admin" });
                }

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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
