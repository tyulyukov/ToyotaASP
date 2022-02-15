using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Toyota.Data;
using Toyota.Helpers.Database.Dump;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBackupsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetBackupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var files = Directory.GetFiles(Dump.Path);
            List<String> dumps = new();

            foreach (var file in files)
            {
                if (file.EndsWith(".json"))
                    dumps.Add(file.Replace(Dump.Path, ""));
            }

            return dumps;
        }
    }
}
