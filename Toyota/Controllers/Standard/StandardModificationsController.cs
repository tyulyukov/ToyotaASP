using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Controllers.Standard
{
    public class StandardModificationsController : Controller
    {
        public const String ModificationDirectoryName = "modifications_thumbs";

        private readonly ApplicationDbContext _context;

        public StandardModificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StandardModifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Modifications.Include(m => m.Model);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StandardModifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modification = await _context.Modifications
                .Include(m => m.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modification == null)
            {
                return NotFound();
            }

            return View(modification);
        }

        // GET: StandardModifications/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Id");
            return View();
        }

        // POST: StandardModifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Slug,ModelId")] Modification modification, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                modification.Id = Guid.NewGuid();
                modification.ImgUrl = await Helpers.Media.UploadImage(file, ModificationDirectoryName);

                _context.Add(modification);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Id", modification.ModelId);

            return View(modification);
        }

        // GET: StandardModifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modification = await _context.Modifications.FindAsync(id);
            if (modification == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Id", modification.ModelId);
            return View(modification);
        }

        // POST: StandardModifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Slug,ImgUrl,ModelId")] Modification modification)
        {
            if (id != modification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModificationExists(modification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Id", modification.ModelId);
            return View(modification);
        }

        // GET: StandardModifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modification = await _context.Modifications
                .Include(m => m.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modification == null)
            {
                return NotFound();
            }

            return View(modification);
        }

        // POST: StandardModifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var modification = await _context.Modifications.FindAsync(id);
            _context.Modifications.Remove(modification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModificationExists(Guid id)
        {
            return _context.Modifications.Any(e => e.Id == id);
        }
    }
}
