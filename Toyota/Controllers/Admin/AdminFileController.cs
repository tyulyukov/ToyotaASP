using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Toyota.Helpers;

namespace Toyota.Controllers.Admin
{
    public class AdminFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
