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
    public class ResourcesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public ResourcesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.Resources.Include(r => r.ResourceCity);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources
                .Include(r => r.ResourceCity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resources == null)
            {
                return NotFound();
            }

            return View(resources);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            ViewData["IdResourceCity"] = new SelectList(_context.ResourceCities, "ID", "Name");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PhoneNumber,IdResourceCity,Adresse,BusNearBy")] Resources resources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdResourceCity"] = new SelectList(_context.ResourceCities, "ID", "Name", resources.IdResourceCity);
            return View(resources);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources.FindAsync(id);
            if (resources == null)
            {
                return NotFound();
            }
            ViewData["IdResourceCity"] = new SelectList(_context.ResourceCities, "ID", "Name", resources.IdResourceCity);
            return View(resources);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,PhoneNumber,IdResourceCity,Adresse,BusNearBy")] Resources resources)
        {
            if (id != resources.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourcesExists(resources.ID))
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
            ViewData["IdResourceCity"] = new SelectList(_context.ResourceCities, "ID", "Name", resources.IdResourceCity);
            return View(resources);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resources = await _context.Resources
                .Include(r => r.ResourceCity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resources == null)
            {
                return NotFound();
            }

            return View(resources);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resources = await _context.Resources.FindAsync(id);
            if (resources != null)
            {
                _context.Resources.Remove(resources);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourcesExists(int id)
        {
            return _context.Resources.Any(e => e.ID == id);
        }
    }
}
