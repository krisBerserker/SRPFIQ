using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Elfie.Serialization;
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

            var query = _context.Resources
                .Include(r => r.ResourceCity)
                .Include(r => r.ResourceBusinessHours)
                .Include(r => r.Resources_ResourceCategories)
                    .ThenInclude(rc => rc.ResourceCategory)
                .FirstOrDefaultAsync(r => r.ID == id);

           
            if (query == null)
            {
                return NotFound();
            }
            var model = new ResourceDetailsViewModel
            {
                Id = query.Result.ID,
                Name = query.Result.Name,
                PhoneNumber = query.Result.PhoneNumber,
                Adresse = query.Result.Adresse,
                Ville = query.Result.ResourceCity?.Name ?? "Non spécifié",
                Categories = query.Result.Resources_ResourceCategories.Select(rc => rc.ResourceCategory.Name).ToList(),
                BusNearBy = string.IsNullOrEmpty(query.Result.BusNearBy) ? new List<string>() : query.Result.BusNearBy.Split(',').ToList(),
                businessHours = query.Result.ResourceBusinessHours.ToList()
            };
            
            return View(model);
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
                // Recharger les listes
                model.Cities = _context.ResourceCities
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                model.Categories = _context.ResourceCategories
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                return View(model);
            }

            try
            {
                // Création de la ressource principale
                var ressource = new Resources
                {
                    Name = model.Nom,
                    PhoneNumber = model.PhoneNumber,
                    Adresse = model.Adresse,
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
                        Console.WriteLine($"Jour: {jour}, Ouverture: {horaire.Opening}, Fermeture: {horaire.Closing}");
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
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ressource créée avec succès.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log (si nécessaire) + message d'erreur utilisateur
                ModelState.AddModelError("", "Une erreur s’est produite lors de la création de la ressource : " + ex.Message);

                // Recharger les listes
                model.Cities = _context.ResourceCities
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                model.Categories = _context.ResourceCategories
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToList();

                return View(model);
            }
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var resource = await _context.Resources
         .Include(r => r.ResourceCity)
         .Include(r => r.Resources_ResourceCategories)
         .Include(r => r.ResourceBusinessHours)
         .FirstOrDefaultAsync(r => r.ID == id);

            if (resource == null) return NotFound();

            var model = new ResourceCreateViewModel
            {
                Nom = resource.Name,
                PhoneNumber = resource.PhoneNumber,
                Adresse = resource.Adresse,
                SelectedCityId = resource.IdResourceCity,
                SelectedCategoryIds = resource.Resources_ResourceCategories.Select(rc => rc.IdResourceCategory).ToList(),
                BusList = resource.BusNearBy?.Split(",").ToList() ?? new List<string>(),
                BusinessHours = resource.ResourceBusinessHours
                    .Select(bh => new BusinessHourInput
                    {
                        Opening = bh.OpeningTime,
                        Closing = bh.ClosingTime,
                        //Days = (DaysOfWeek) bh.DayOfWeek
                    })
                    .ToList(),

                Cities = await _context.ResourceCities
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToListAsync(),

                Categories = await _context.ResourceCategories
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToListAsync()
            };

            return View(model); 
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResourceCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Recharger les listes déroulantes
                model.Cities = await _context.ResourceCities
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToListAsync();

                model.Categories = await _context.ResourceCategories
                    .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
                    .ToListAsync();

                return View(model);
            }

            var resource = await _context.Resources
                .Include(r => r.Resources_ResourceCategories)
                .Include(r => r.ResourceBusinessHours)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (resource == null) return NotFound();

            // Mise à jour des champs
            resource.Name = model.Nom;
            resource.PhoneNumber = model.PhoneNumber;
            resource.Adresse = model.Adresse;
            resource.IdResourceCity = model.SelectedCityId;
            resource.BusNearBy = string.Join(",", model.BusList);

            // Mise à jour des catégories
            _context.Resources_ResourceCategories.RemoveRange(resource.Resources_ResourceCategories);
            foreach (var catId in model.SelectedCategoryIds)
            {
                _context.Resources_ResourceCategories.Add(new Resources_ResourceCategories
                {
                    IdResource = id,
                    IdResourceCategory = catId
                });
            }

            // Mise à jour des horaires
            _context.ResourceBusinessHours.RemoveRange(resource.ResourceBusinessHours);
            foreach (var bh in model.BusinessHours)
            {
                _context.ResourceBusinessHours.Add(new ResourceBusinessHours
                {
                    IdResource = id,
                    OpeningTime = bh.Opening,
                    ClosingTime = bh.Closing,
                    //DayOfWeek = bh.Days
                });
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ressource modifiée avec succès.";
            return RedirectToAction("Index");
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
