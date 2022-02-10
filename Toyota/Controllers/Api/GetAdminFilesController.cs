using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Toyota.Helpers;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAdminFilesController : ControllerBase
    {
        [HttpGet("{path}")]
        public async Task<ActionResult<String[]>> Get(String? path)
        {
            if (String.IsNullOrWhiteSpace(path))
                return null;

            var files = Directory.GetFiles(Media.StoragePath + "\\" + path.Replace("~", "\\"));

            for (int i = 0; i < files.Length; i++)
                files[i] = files[i].Replace(Media.StoragePath + "\\", "");

            return files;
        }

        [HttpGet]
        public async Task<ActionResult<String[]>> Get()
        {
            var files = Directory.GetFiles(Media.StoragePath);

            for (int i = 0; i < files.Length; i++)
                files[i] = files[i].Replace(Media.StoragePath, "");

            return files;
        }
    }
}
