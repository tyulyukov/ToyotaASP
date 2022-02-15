using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toyota.Data;
using Toyota.Helpers.Database.Dump;
using Toyota.Helpers.Notification;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreBackupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DumpDatabase dumpDatabase;

        public StoreBackupsController(ApplicationDbContext context)
        {
            _context = context;
            dumpDatabase = new DumpDatabase(_context);
            dumpDatabase.OnBackupCreated += (String message) => Email.Send(message, "backup database", "makstyulyukov@gmail.com");
        }

        [HttpGet]
        public ActionResult<String> CreateBackup()
        {
            String backupPath = dumpDatabase.Create();
            backupPath = backupPath.Replace(Dump.Path, "");

            return new JsonResult(backupPath);
        }

        [HttpGet("{backupName}")]
        public ActionResult RestoreBackup(String backupName)
        {
            if (!dumpDatabase.Restore(Dump.Path + backupName))
                return BadRequest();

            return new JsonResult(backupName);
        }
    }
}
