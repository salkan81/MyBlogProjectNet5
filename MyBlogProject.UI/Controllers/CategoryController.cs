﻿using Microsoft.AspNetCore.Mvc;

namespace MyBlogProject.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
