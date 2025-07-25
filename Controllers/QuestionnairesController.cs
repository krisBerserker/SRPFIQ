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
    public class QuestionnairesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnairesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Questionnaires
        public async Task<IActionResult> Index()
        {

            return View(await _context.Questionnaires.ToListAsync());
        }

        // GET: Questionnaires/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var questionnaires = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.ID == id);

            if (questionnaires == null) return NotFound();

            return View(questionnaires);
        }

        // GET: Questionnaires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questionnaires/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Active,CreatedDate")] Questionnaires questionnaires)
        {
            if (ModelState.IsValid)
            {
                questionnaires.CreatedDate = DateTime.Now;
                _context.Add(questionnaires);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionnaires);
        }

        // GET: Questionnaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var questionnaires = await _context.Questionnaires
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.ID == id);

            if (questionnaires == null) return NotFound();

            // Injecter les ViewBag nécessaires pour le modal
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

            return View(questionnaires);
        }

        // POST: Questionnaires/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Active,CreatedDate")] Questionnaires questionnaires)
        {
            if (id != questionnaires.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    questionnaires.CreatedDate = DateTime.Now;
                    _context.Update(questionnaires);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnairesExists(questionnaires.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id = questionnaires.ID });
            }

            return RedirectToAction("Edit", new { id = questionnaires.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire == null) return NotFound();

            questionnaire.Active = !questionnaire.Active;
            _context.Update(questionnaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Questionnaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var questionnaires = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.ID == id);

            if (questionnaires == null) return NotFound();

            return View(questionnaires);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            _context.Questionnaires.Remove(questionnaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnairesExists(int id)
        {
            return _context.Questionnaires.Any(e => e.ID == id);
        }



    }
}
