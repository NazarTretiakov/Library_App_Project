﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [Route("/admin-panel")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin-panel/manage-books")]
        public IActionResult ManageBooks()
        {
            return View();
        }

        [Route("/admin-panel/manage-books/add-book")]
        public IActionResult AddBook()
        {
            return View();
        }

        [Route("/admin-panel/manage-books/manage-book")]
        public IActionResult ManageBook()
        {
            return View();
        }
    }
}
