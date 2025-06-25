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
    public class ResourceCitiesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public ResourceCitiesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: ResourceCities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResourceCities.ToListAsync());
        }

        // GET: ResourceCities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCities = await _context.ResourceCities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceCities == null)
            {
                return NotFound();
            }

            return View(resourceCities);
        }

        // GET: ResourceCities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResourceCities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Active,CreatedDate")] ResourceCities resourceCities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceCities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourceCities);
        }

        // GET: ResourceCities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCities = await _context.ResourceCities.FindAsync(id);
            if (resourceCities == null)
            {
                return NotFound();
            }
            return View(resourceCities);
        }

        // POST: ResourceCities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active,CreatedDate")] ResourceCities resourceCities)
        {
            if (id != resourceCities.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceCities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceCitiesExists(resourceCities.ID))
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
            return View(resourceCities);
        }

        // GET: ResourceCities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceCities = await _context.ResourceCities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceCities == null)
            {
                return NotFound();
            }

            return View(resourceCities);
        }

        // POST: ResourceCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resourceCities = await _context.ResourceCities.FindAsync(id);
            if (resourceCities != null)
            {
                _context.ResourceCities.Remove(resourceCities);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceCitiesExists(int id)
        {
            return _context.ResourceCities.Any(e => e.ID == id);
        }
    }
}
