﻿using Microsoft.AspNetCore.Http;
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
    public class GetAdminDirectoriesController : ControllerBase
    {
        [HttpGet("{path}")]
        public async Task<ActionResult<String[]>> Get(String? path)
        {
            if (String.IsNullOrWhiteSpace(path))
                return null;

            var directories = Directory.GetDirectories(Media.StoragePath + "\\" + path.Replace("~", "\\"));
            
            for (int i = 0; i < directories.Length; i++)
                directories[i] = directories[i].Replace(Media.StoragePath + "\\", "");

            return directories;
        }

        [HttpGet]
        public async Task<ActionResult<String[]>> Get()
        {
            var directories = Directory.GetDirectories(Media.StoragePath);

            for (int i = 0; i < directories.Length; i++)
                directories[i] = directories[i].Replace(Media.StoragePath, "");

            return directories;
        }
    }
}
