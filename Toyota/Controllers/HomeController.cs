using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Toyota.Data;
using Toyota.Helpers.Database.Dump;
using Toyota.Models;

namespace Toyota.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // var dumpColors = new DumpColors(_context);
            // dumpColors.Restore(@"C:\Users\User\source\repos\C# ASP.NET\Toyota\Toyota\wwwroot\storage\dumps\f22dd7c4-854c-4be9-b9ad-c32bf13fb08b.xml");

            // var dumpModels = new DumpModels(_context);
            // var models = dumpModels.Restore(dumpModels.Create());

            // var dumpModifications = new DumpModifications(_context);
            // var modifications = dumpModifications.Restore(dumpModifications.Create());

            // var dumpModificationColors = new DumpModificationColors(_context);
            // var modificationColor = dumpModificationColors.Restore(dumpModificationColors.Create());

            // var dumpDatabase = new DumpDatabase(_context);
            // var db = dumpDatabase.Restore(dumpDatabase.Create());

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cars()
        {
            return View();
        }

        public IActionResult Jwt()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
