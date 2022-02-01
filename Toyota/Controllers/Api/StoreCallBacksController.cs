using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreCallBacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoreCallBacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CallBack>> PostCallBack(CallBack callBack)
        {
            _context.CallBacks.Add(callBack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCallBack", new { id = callBack.Id }, callBack);
        }
    }
}
