using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetModificationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GetModificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Modification>>> Get(Guid id)
        {
            return await _context.Modifications.Where(m=> m.ModelId == id).ToListAsync();
        }
    }
}
