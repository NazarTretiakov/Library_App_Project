using LibraryApp.Core.Domain.Entities;
using LibraryApp.Core.Domain.IdentityEntities;
using LibraryApp.Core.DTO;
using LibraryApp.Core.Enums;
using LibraryApp.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class MyAccountController : Controller
    {
        private readonly IOrdersGetterService _ordersGetterService;
        private readonly IChangeProfileInformationService _changeProfileInformationService;
        private readonly IChangeProfilePhotoService _changeProfilePhotoService;
        private readonly IUsersGetterService _usersGetterService;
        private readonly ISavesGetterService _savesGetterService;

        private readonly UserManager<User> _userManager;

        public MyAccountController(IOrdersGetterService ordersGetterService, IChangeProfileInformationService changeProfileInformationService, IChangeProfilePhotoService changeProfilePhotoService, IUsersGetterService usersGetterService, ISavesGetterService savesGetterService, UserManager<User> userManager)
        {
            _ordersGetterService = ordersGetterService;
            _changeProfileInformationService = changeProfileInformationService;
            _changeProfilePhotoService = changeProfilePhotoService;
            _usersGetterService = usersGetterService;
            _savesGetterService = savesGetterService;
            _userManager = userManager;
        }

        [Route("/my-account")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/my-account/orders")]
        public async Task<IActionResult> Orders()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            List<Order> orders = await _ordersGetterService.GetUserOrders(currentWorkingUser.Id.ToString());
            orders = orders.Where(o => o.Status != OrderStatusOptions.Returned.ToString() && o.Status != OrderStatusOptions.InRead.ToString()).ToList();

            return View(orders);
        }

        [Route("/my-account/orders-history")]
        public async Task<IActionResult> OrdersHistory()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            List<Order> orders = await _ordersGetterService.GetUserOrders(currentWorkingUser.Id.ToString());
            orders = orders.Where(o => o.Status == OrderStatusOptions.Returned.ToString()).ToList();

            return View(orders);
        }

        [Route("/my-account/notifications")]
        public IActionResult Notifications()
        {
            return View();
        }

        [Route("/my-account/settings")]
        public IActionResult Settings()
        {
            return RedirectToAction(nameof(MyAccountController.ProfileInformationSettings), "MyAccount");
        }

        [Route("/my-account/settings/profile")]
        [HttpGet]
        public async Task<IActionResult> ProfileInformationSettings()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            ChangeProfileInformationDTO changeProfileInformationDTO = new ChangeProfileInformationDTO()
            {
                Firstname = currentWorkingUser.Firstname,
                Lastname = currentWorkingUser.Lastname,
                Description = currentWorkingUser.Description
            };

            return View(changeProfileInformationDTO);
        }

        [Route("/my-account/settings/profile")]
        [HttpPost]
        public async Task<IActionResult> ProfileInformationSettings(ChangeProfileInformationDTO changeProfileInformationDTO)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (ModelState.IsValid == false || currentWorkingUser.Firstname == changeProfileInformationDTO.Firstname && currentWorkingUser.Lastname == changeProfileInformationDTO.Lastname && currentWorkingUser.Description == changeProfileInformationDTO.Description)
            {
                return View(changeProfileInformationDTO);
            }

            await _changeProfileInformationService.ChangeProfileInformation(changeProfileInformationDTO);

            ViewBag.IsInformationChanged = true;

            return View(changeProfileInformationDTO);
        }

        [Route("/my-account/settings/photo")]
        [HttpGet]
        public async Task<IActionResult> ProfilePhotoSettings()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/my-account/settings/photo")]
        [HttpPost]
        public async Task<IActionResult> ProfilePhotoSettings(ChangeProfilePhotoDTO changeProfilePhotoDTO)//  TODO: Fix the bug where the old user profile image is not removed when the user updates their profile image.The old image should be removed.
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (ModelState.IsValid == false)
            {
                return View();
            }

            await _changeProfilePhotoService.ChangeProfilePhoto(changeProfilePhotoDTO);

            return View();
        }

        [Route("/my-account/settings/change-password")]
        [HttpGet]
        public async Task<IActionResult> ChangePasswordSettings()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return View();
        }

        [Route("/my-account/settings/change-password")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordSettings(ChangePasswordDTO changePasswordDTO)
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            if (ModelState.IsValid == false)
            {
                return View();
            }
            else if (changePasswordDTO.OldPassword == changePasswordDTO.NewPassword)
            {
                ModelState.AddModelError("Change password", "New password can't be the same as old password.");
                return View();
            }

            var result = await _userManager.ChangePasswordAsync(currentWorkingUser, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);

            if (result.Succeeded)
            {
                ViewBag.IsPasswordChanged = true;
                return View();
            }
            else
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Change password", error.Description);
                }

                return View();
            }
        }

        [Route("/my-account/saved")]
        public async Task<IActionResult> Saved()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            List<Save> saves = await _savesGetterService.GetSavesByUserId(currentWorkingUser.Id.ToString());

            return View(saves);
        }

        [Route("/my-account/my-page")]
        public async Task<IActionResult> MyPage()
        {
            User currentWorkingUser = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CurrentWorkingUser = currentWorkingUser;

            return RedirectToAction(nameof(UserProfileController.Posts), "UserProfile", new { userId = currentWorkingUser.Id});
        }
    }
}
