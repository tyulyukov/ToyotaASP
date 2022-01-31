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
    public class StandardModificationColorsController : Controller
    {
        public const String ModificationColorsDirectoryName = "modification_colors_thumbs";

        private readonly ApplicationDbContext _context;

        public StandardModificationColorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StandardModificationColors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ModificationColors.Include(m => m.Color).Include(m => m.Modification);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StandardModificationColors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modificationColors = await _context.ModificationColors
                .Include(m => m.Color)
                .Include(m => m.Modification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modificationColors == null)
            {
                return NotFound();
            }

            return View(modificationColors);
        }

        // GET: StandardModificationColors/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["ModificationId"] = new SelectList(_context.Modifications, "Id", "Name");
            return View();
        }

        // POST: StandardModificationColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Slug,ModificationId,ColorId")] ModificationColors modificationColors, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                modificationColors.Id = Guid.NewGuid();
                modificationColors.ImgUrl = await Helpers.Media.UploadImage(file, ModificationColorsDirectoryName);

                _context.Add(modificationColors);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", modificationColors.ColorId);
            ViewData["ModificationId"] = new SelectList(_context.Modifications, "Id", "Name", modificationColors.ModificationId);

            return View(modificationColors);
        }

        // GET: StandardModificationColors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modificationColors = await _context.ModificationColors.FindAsync(id);
            if (modificationColors == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", modificationColors.ColorId);
            ViewData["ModificationId"] = new SelectList(_context.Modifications, "Id", "Name", modificationColors.ModificationId);
            return View(modificationColors);
        }

        // POST: StandardModificationColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Slug,ModificationId,ColorId,ImgUrl")] ModificationColors modificationColors)
        {
            if (id != modificationColors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modificationColors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModificationColorsExists(modificationColors.Id))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Name", modificationColors.ColorId);
            ViewData["ModificationId"] = new SelectList(_context.Modifications, "Id", "Name", modificationColors.ModificationId);
            return View(modificationColors);
        }

        // GET: StandardModificationColors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modificationColors = await _context.ModificationColors
                .Include(m => m.Color)
                .Include(m => m.Modification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modificationColors == null)
            {
                return NotFound();
            }

            return View(modificationColors);
        }

        // POST: StandardModificationColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var modificationColors = await _context.ModificationColors.FindAsync(id);
            _context.ModificationColors.Remove(modificationColors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModificationColorsExists(Guid id)
        {
            return _context.ModificationColors.Any(e => e.Id == id);
        }
    }
}
