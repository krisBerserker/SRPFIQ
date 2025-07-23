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
    public class QuestionnaireQuestionsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnaireQuestionsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .Include(q => q.QuestionnaireDataSources);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var questionnaireQuestions = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireQuestions == null) return NotFound();

            return View(questionnaireQuestions);
        }

        public IActionResult Create()
        {
            ViewBag.MainDataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Champ texte" },
                new SelectListItem { Value = "2", Text = "Radio Bouton" },
                new SelectListItem { Value = "3", Text = "Case à cocher" },
                new SelectListItem { Value = "4", Text = "Liste déroulante" },
                new SelectListItem { Value = "5", Text = "Liste déroulante multiple" },
                new SelectListItem { Value = "6", Text = "Tableau composé" },
                new SelectListItem { Value = "7", Text = "Champ texte multiple" }
            };

            ViewBag.SubDataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "2", Text = "Radio Bouton" },
                new SelectListItem { Value = "3", Text = "Case à cocher" }
            };

            var dataSources = _context.QuestionnaireDataSources
                .Select(ds => new { ds.ID, ds.Name })
                .ToList();

            ViewBag.DataSources = new SelectList(dataSources, "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdQuestionnaire,Order,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            // Validation métier personnalisée
            if (questionnaireQuestions.IdMainDataType != 1 && questionnaireQuestions.IdMainDataType != 7)
            {
                if (questionnaireQuestions.IdMainDataSource == null)
                {
                    ModelState.AddModelError("IdMainDataSource", "Une source de données est requise pour ce type de contrôle HTML.");
                }
            }

            if (questionnaireQuestions.IdMainDataType == 6)
            {
                if (questionnaireQuestions.IdSubDataType == null)
                {
                    ModelState.AddModelError("IdSubDataType", "Un type de contrôle secondaire est requis pour un tableau composé.");
                }
                else if (questionnaireQuestions.IdSubDataType != 2 && questionnaireQuestions.IdSubDataType != 3)
                {
                    ModelState.AddModelError("IdSubDataType", "Le type de contrôle secondaire doit être Radio Bouton (2) ou Case à cocher (3).");
                }

                if (questionnaireQuestions.IdSubDataSource == null)
                {
                    ModelState.AddModelError("IdSubDataSource", "Une source de données secondaire est requise pour un tableau composé.");
                }
            }

            if (!ModelState.IsValid)
            {
                // Recharger les listes pour la vue
                ViewBag.MainDataTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "Champ texte" },
            new SelectListItem { Value = "2", Text = "Radio Bouton" },
            new SelectListItem { Value = "3", Text = "Case à cocher" },
            new SelectListItem { Value = "4", Text = "Liste déroulante" },
            new SelectListItem { Value = "5", Text = "Liste déroulante multiple" },
            new SelectListItem { Value = "6", Text = "Tableau composé" },
            new SelectListItem { Value = "7", Text = "Champ texte multiple" }
        };

                ViewBag.SubDataTypes = new List<SelectListItem>
        {
            new SelectListItem { Value = "2", Text = "Radio Bouton" },
            new SelectListItem { Value = "3", Text = "Case à cocher" }
        };

                var dataSources = _context.QuestionnaireDataSources
                    .Select(ds => new { ds.ID, ds.Name })
                    .ToList();
                ViewBag.DataSources = new SelectList(dataSources, "ID", "Name", questionnaireQuestions.IdMainDataSource);

                return View(questionnaireQuestions);
            }

            // Si tout est bon
            _context.Add(questionnaireQuestions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var questionnaireQuestions = await _context.QuestionnaireQuestions.FindAsync(id);
            if (questionnaireQuestions == null) return NotFound();

            ViewBag.MainDataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Champ texte" },
                new SelectListItem { Value = "2", Text = "Radio Bouton" },
                new SelectListItem { Value = "3", Text = "Case à cocher" },
                new SelectListItem { Value = "4", Text = "Liste déroulante" },
                new SelectListItem { Value = "5", Text = "Liste déroulante multiple" },
                new SelectListItem { Value = "6", Text = "Tableau composé" },
                new SelectListItem { Value = "7", Text = "Champ texte multiple" }
            };

            ViewBag.SubDataTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "2", Text = "Radio Bouton" },
                new SelectListItem { Value = "3", Text = "Case à cocher" }
            };

            var dataSources = _context.QuestionnaireDataSources
                .Select(ds => new { ds.ID, ds.Name })
                .ToList();

            ViewBag.DataSources = new SelectList(dataSources, "ID", "Name", questionnaireQuestions.IdMainDataSource);

            return View(questionnaireQuestions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaire,Order,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            if (id != questionnaireQuestions.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireQuestions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.QuestionnaireQuestions.Any(e => e.ID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(questionnaireQuestions);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var questionnaireQuestions = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireQuestions == null) return NotFound();

            return View(questionnaireQuestions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var QuestionnaireQuestions = await _context.QuestionnaireQuestions.FindAsync(id);
            if (QuestionnaireQuestions == null) return NotFound();

            QuestionnaireQuestions.Active = !QuestionnaireQuestions.Active;
            _context.Update(QuestionnaireQuestions);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireQuestions = await _context.QuestionnaireQuestions.FindAsync(id);
            if (questionnaireQuestions != null)
            {
                _context.QuestionnaireQuestions.Remove(questionnaireQuestions);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
