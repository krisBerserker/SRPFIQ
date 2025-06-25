using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Controllers
{
    public class ResourceCategoriesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public ResourceCategoriesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: ResourceCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResourceCategories.ToListAsync());
        }

        // GET: ResourceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCategories = await _context.ResourceCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceCategories == null)
            {
                return NotFound();
            }

            return View(resourceCategories);
        }

        // GET: ResourceCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResourceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Active,CreatedDate")] ResourceCategories resourceCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourceCategories);
        }

        // GET: ResourceCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCategories = await _context.ResourceCategories.FindAsync(id);
            if (resourceCategories == null)
            {
                return NotFound();
            }
            return View(resourceCategories);
        }

        // POST: ResourceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active,CreatedDate")] ResourceCategories resourceCategories)
        {
            if (id != resourceCategories.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceCategoriesExists(resourceCategories.ID))
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
            return View(resourceCategories);
        }

        // GET: ResourceCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCategories = await _context.ResourceCategories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceCategories == null)
            {
                return NotFound();
            }

            return View(resourceCategories);
        }

        // POST: ResourceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resourceCategories = await _context.ResourceCategories.FindAsync(id);
            if (resourceCategories != null)
            {
                _context.ResourceCategories.Remove(resourceCategories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceCategoriesExists(int id)
        {
            return _context.ResourceCategories.Any(e => e.ID == id);
        }
    }
}
