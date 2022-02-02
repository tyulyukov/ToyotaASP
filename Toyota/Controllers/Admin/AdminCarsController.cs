using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Toyota.Data;
using Toyota.Data.Entities;

namespace Toyota.Controllers.Admin
{
    public class AdminCarsController : Controller
    {
        public const String ModificationDirectoryName = "modifications_thumbs";
        public const String ModelsDirectoryName = "models_thumbs";
        public const String ModificationColorsDirectoryName = "modification_colors_thumbs";
        public const String ColorsDirectoryName = "colors_thumbs";

        private readonly ApplicationDbContext _context;

        public AdminCarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Models.ToListAsync());
        }

        public async Task<IActionResult> ModelModifications(Guid? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.OpenModel = _context.Models.First(model => model.Id == id);

            return View(await _context.Modifications
                .Include(modification => modification.ModificationColors)
                .Where(mod => mod.ModelId == id).ToListAsync()
            );
        }

        public async Task<IActionResult> ModificationColors(Guid? id)
        {
            if (id == null)
                return NotFound();

            ViewBag.OpenModification = _context.Modifications.First(modification => modification.Id == id);

            return View(await _context.ModificationColors
                .Include(modificationColor => modificationColor.Color)
                .Where(modificationColor => modificationColor.ModificationId == id).ToListAsync()
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModification([Bind("Id,Name,Slug,ModelId")] Modification modification, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                modification.Id = Guid.NewGuid();
                modification.ImgUrl = await Helpers.Media.UploadImage(file, ModificationDirectoryName);

                _context.Add(modification);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ModelModifications), new { Id = modification.ModelId });
            }

            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", modification.ModelId);

            return View(modification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModel([Bind("Id,Name,Slug")] Model model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                model.ImgUrl = await Helpers.Media.UploadImage(file, ModelsDirectoryName);

                _context.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateModificationColor([Bind("Id,Slug,ModificationId,ColorId")] ModificationColors modificationColors, IFormFile file)
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
    }
}
