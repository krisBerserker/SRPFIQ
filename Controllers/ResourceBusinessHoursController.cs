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
    public class ResourceBusinessHoursController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public ResourceBusinessHoursController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: ResourceBusinessHours
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.ResourceBusinessHours.Include(r => r.Resource);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: ResourceBusinessHours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceBusinessHours = await _context.ResourceBusinessHours
                .Include(r => r.Resource)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceBusinessHours == null)
            {
                return NotFound();
            }

            return View(resourceBusinessHours);
        }

        // GET: ResourceBusinessHours/Create
        public IActionResult Create()
        {
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name");
            return View();
        }

        // POST: ResourceBusinessHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdResource,DayOfWeek,OpeningTime,ClosingTime")] ResourceBusinessHours resourceBusinessHours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceBusinessHours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resourceBusinessHours.IdResource);
            return View(resourceBusinessHours);
        }

        // GET: ResourceBusinessHours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceBusinessHours = await _context.ResourceBusinessHours.FindAsync(id);
            if (resourceBusinessHours == null)
            {
                return NotFound();
            }
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resourceBusinessHours.IdResource);
            return View(resourceBusinessHours);
        }

        // POST: ResourceBusinessHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdResource,DayOfWeek,OpeningTime,ClosingTime")] ResourceBusinessHours resourceBusinessHours)
        {
            if (id != resourceBusinessHours.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceBusinessHours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceBusinessHoursExists(resourceBusinessHours.ID))
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
            ViewData["IdResource"] = new SelectList(_context.Resources, "ID", "Name", resourceBusinessHours.IdResource);
            return View(resourceBusinessHours);
        }

        // GET: ResourceBusinessHours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resourceBusinessHours = await _context.ResourceBusinessHours
                .Include(r => r.Resource)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (resourceBusinessHours == null)
            {
                return NotFound();
            }

            return View(resourceBusinessHours);
        }

        // POST: ResourceBusinessHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resourceBusinessHours = await _context.ResourceBusinessHours.FindAsync(id);
            if (resourceBusinessHours != null)
            {
                _context.ResourceBusinessHours.Remove(resourceBusinessHours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceBusinessHoursExists(int id)
        {
            return _context.ResourceBusinessHours.Any(e => e.ID == id);
        }
    }
}
