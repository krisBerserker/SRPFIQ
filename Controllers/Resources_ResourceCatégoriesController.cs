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
    public class Resources_ResourceCatégoriesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public Resources_ResourceCatégoriesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Resources_ResourceCatégories
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.Resources_ResourceCatégories.Include(r => r.Resource).Include(r => r.ResourceCategory);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: Resources_ResourceCatégories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources_ResourceCatégories = await _context.Resources_ResourceCatégories
                .Include(r => r.Resource)
                .Include(r => r.ResourceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resources_ResourceCatégories == null)
            {
                return NotFound();
            }

            return View(resources_ResourceCatégories);
        }

        // GET: Resources_ResourceCatégories/Create
        public IActionResult Create()
        {
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name");
            ViewData["IdResourceCategory"] = new SelectList(_context.ResourceCategories, "ID", "Name");
            return View();
        }

        // POST: Resources_ResourceCatégories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdResourceCategory,IdResource")] Resources_ResourceCatégories resources_ResourceCatégories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resources_ResourceCatégories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resources_ResourceCatégories.IdResource);
            ViewData["IdResourceCategory"] = new SelectList(_context.ResourceCategories, "ID", "Name", resources_ResourceCatégories.IdResourceCategory);
            return View(resources_ResourceCatégories);
        }

        // GET: Resources_ResourceCatégories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources_ResourceCatégories = await _context.Resources_ResourceCatégories.FindAsync(id);
            if (resources_ResourceCatégories == null)
            {
                return NotFound();
            }
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resources_ResourceCatégories.IdResource);
            ViewData["IdResourceCategory"] = new SelectList(_context.ResourceCategories, "ID", "Name", resources_ResourceCatégories.IdResourceCategory);
            return View(resources_ResourceCatégories);
        }

        // POST: Resources_ResourceCatégories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdResourceCategory,IdResource")] Resources_ResourceCatégories resources_ResourceCatégories)
        {
            if (id != resources_ResourceCatégories.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resources_ResourceCatégories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Resources_ResourceCatégoriesExists(resources_ResourceCatégories.ID))
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
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resources_ResourceCatégories.IdResource);
            ViewData["IdResourceCategory"] = new SelectList(_context.ResourceCategories, "ID", "Name", resources_ResourceCatégories.IdResourceCategory);
            return View(resources_ResourceCatégories);
        }

        // GET: Resources_ResourceCatégories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources_ResourceCatégories = await _context.Resources_ResourceCatégories
                .Include(r => r.Resource)
                .Include(r => r.ResourceCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resources_ResourceCatégories == null)
            {
                return NotFound();
            }

            return View(resources_ResourceCatégories);
        }

        // POST: Resources_ResourceCatégories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resources_ResourceCatégories = await _context.Resources_ResourceCatégories.FindAsync(id);
            if (resources_ResourceCatégories != null)
            {
                _context.Resources_ResourceCatégories.Remove(resources_ResourceCatégories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Resources_ResourceCatégoriesExists(int id)
        {
            return _context.Resources_ResourceCatégories.Any(e => e.ID == id);
        }
    }
}
