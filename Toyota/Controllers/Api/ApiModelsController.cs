﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            return await _context.Models.ToListAsync();
        }

        // GET: api/ApiModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(Guid id)
        {
            var model = await _context.Models.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return model;
        }
    }
}
