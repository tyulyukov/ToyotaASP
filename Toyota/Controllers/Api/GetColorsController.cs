using System;
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
    public class GetColorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GetColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        // GET: api/GetColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(Guid id)
        {
            var color = await _context.Colors.FindAsync(id);

            if (color == null)
                return NotFound();

            return color;
        }
    }
}
