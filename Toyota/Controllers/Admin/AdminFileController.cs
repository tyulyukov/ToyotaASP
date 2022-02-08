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
            String currentDirectory = "/";

            var directories = Directory.GetDirectories(Media.StoragePath);

            for (int i = 0; i < directories.Length; i++)
                directories[i] = directories[i].Replace(Media.StoragePath, "");

            ViewBag.Directories = directories;
            ViewBag.DirectoryFiles = Directory.GetFiles(Media.StoragePath);

            return View();
        }
    }
}
