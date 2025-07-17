using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using WebApplication_SRPFIQ.ViewModel;

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
        public async Task<IActionResult> Index(int? SelectedCategorieId, int? SelectedCityId, string SelectedBus)
        {
            var categories = _context.ResourceCategories
           .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
           .ToList();

            var quartiers = _context.Resources
                .Select(r => r.ResourceCity)
                .Distinct()
                .Select(q => new SelectListItem { Value = q.Name, Text = q.Name })
                .ToList();

            var query = _context.Resources.Include(r => r.Resources_ResourceCategories).AsQueryable();

            if (SelectedCategorieId.HasValue)
                query = query.Where(r => r.ID == SelectedCategorieId);

            if (SelectedCityId.HasValue)
                query = query.Where(r => r.IdResourceCity == SelectedCityId);

            if (!string.IsNullOrEmpty(SelectedBus))
                query = query.Where(r => r.BusNearBy.Contains(SelectedBus));

            var model = new RessourceSearchViewModel
            {
                Categories = categories,
                Quartiers = quartiers,
                SelectedCategorieId = SelectedCategorieId,
                SelectedCityId = SelectedCityId,
                SelectedBus = SelectedBus,
                Resultats = query.ToList()
            };

            return View(model);
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
            ViewData["IdResourceCategorie"] = new SelectList(_context.ResourceCategories, "ID", "Name");
            ViewData["IdResourceBusinnesHours"] = new SelectList(_context.ResourceBusinessHours, "ID");

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
            ViewData["IdResourceCategorie"] = new SelectList(_context.ResourceCategories, "ID", "Name", resources.Resources_ResourceCategories);
            ViewData["IdResourceBusinnesHours"] = new SelectList(_context.ResourceBusinessHours, "ID","OpeningTime", resources.BusNearBy);

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
            ViewData["IdResourceCategorie"] = new SelectList(_context.ResourceCategories, "ID", "Name", resources.Resources_ResourceCategories);
            ViewData["IdResourceBusinnesHours"] = new SelectList(_context.ResourceBusinessHours, "ID", "OpeningTime", resources.BusNearBy);


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
