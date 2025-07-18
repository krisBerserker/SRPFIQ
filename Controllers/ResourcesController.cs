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

            var villes = _context.ResourceCities
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.Name })
                .ToList();

            var query = _context.Resources
                .Include(r => r.ResourceCity)
                .Include(r => r.ResourceBusinessHours)
                .Include(r => r.Resources_ResourceCategories)
                    .ThenInclude(rc => rc.ResourceCategory)
                .AsQueryable();

            if (SelectedCategorieId.HasValue)
            {
                query = query.Where(r => r.Resources_ResourceCategories
                    .Any(rc => rc.IdResourceCategory == SelectedCategorieId));
            }

            if (SelectedCityId.HasValue)
                query = query.Where(r => r.IdResourceCity == SelectedCityId);

            if (!string.IsNullOrEmpty(SelectedBus))
                query = query.Where(r => r.BusNearBy.Contains(SelectedBus));

            var model = new ResourceSearchViewModel
            {
                Categories = categories,
                Cities = villes,
                SelectedCategorieId = SelectedCategorieId,
                SelectedCityId = SelectedCityId,
                SelectedBus = SelectedBus,
                Resultats = query.ToList()
            };

            try
            {
                model.Resultats = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

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
            var model = new ResourceCreateViewModel
            {
                Cities = _context.ResourceCities.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList(),
                Categories = _context.ResourceCategories.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList()
            };

            return View(model);
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Recharger les listes en cas d'erreur
                model.Cities = _context.ResourceCities
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                model.Categories = _context.ResourceCategories
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                return View(model);
            }

            // Création de la ressource principale
            var ressource = new Resources
            {
                Name = model.Nom,
                IdResourceCity = model.SelectedCityId,
                BusNearBy = string.Join(",", model.BusList),
                Resources_ResourceCategories = new List<Resources_ResourceCategories>(),
                ResourceBusinessHours = new List<ResourceBusinessHours>()
            };

            // Lien vers les catégories sélectionnées
            foreach (var catId in model.SelectedCategoryIds.Distinct())
            {
                ressource.Resources_ResourceCategories.Add(new Resources_ResourceCategories
                {
                    IdResourceCategory = catId
                });
            }

            // Horaire : chaque ligne peut avoir plusieurs jours → une ligne par jour
            foreach (var horaire in model.BusinessHours)
            {
                foreach (var jour in horaire.Days)
                {
                    ressource.ResourceBusinessHours.Add(new ResourceBusinessHours
                    {
                        DayOfWeek = (DaysOfWeek)jour,
                        OpeningTime = horaire.Opening,
                        ClosingTime = horaire.Closing
                    });
                }
            }

            // Sauvegarde
            _context.Resources.Add(ressource);
            _context.SaveChanges();

            return RedirectToAction("Index");
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
