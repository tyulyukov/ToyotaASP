using Microsoft.AspNetCore.Mvc;
using System;
using Toyota.Data;
using Toyota.Helpers.Database.Dump;
using Toyota.Helpers.Notification;

namespace Toyota.Controllers.Admin
{
    public class AdminBackupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DumpDatabase dumpDatabase;

        public AdminBackupsController(ApplicationDbContext context)
        {
            _context = context;
            dumpDatabase = new DumpDatabase(_context);
            dumpDatabase.OnBackupCreated += (String message) => Email.Send(message, "backup database", "makstyulyukov@gmail.com");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
