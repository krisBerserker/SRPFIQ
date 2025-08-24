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
            var questions = _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .OrderBy(q => q.Order);

            return View(await questions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var question = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .FirstOrDefaultAsync(q => q.ID == id);

            if (question == null) return NotFound();

            return View(question);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdQuestionnaire,Order,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            // Validation métier
            if (questionnaireQuestions.IdMainDataType != 1 && questionnaireQuestions.IdMainDataType != 7)
            {
                if (questionnaireQuestions.IdMainDataSource == null)
                    ModelState.AddModelError("IdMainDataSource", "Une source de données est requise.");
            }

            if (questionnaireQuestions.IdMainDataType == 6)
            {
                if (questionnaireQuestions.IdSubDataType is null || (questionnaireQuestions.IdSubDataType != 2 && questionnaireQuestions.IdSubDataType != 3))
                    ModelState.AddModelError("IdSubDataType", "Le type secondaire doit être Radio (2) ou Checkbox (3).");

                if (questionnaireQuestions.IdSubDataSource == null)
                    ModelState.AddModelError("IdSubDataSource", "Une source secondaire est requise.");
            }

            // Gestion automatique de l’ordre
            questionnaireQuestions.Order = (_context.QuestionnaireQuestions
                .Where(q => q.IdQuestionnaire == questionnaireQuestions.IdQuestionnaire)
                .Max(q => (int?)q.Order) ?? 0) + 1;

            if (!ModelState.IsValid)
            {
                LoadViewBags();
                return View(questionnaireQuestions);
            }

            _context.Add(questionnaireQuestions);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var question = await _context.QuestionnaireQuestions.FindAsync(id);
            if (question == null) return NotFound();

            LoadViewBags(question.IdMainDataSource);

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaire,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            if (id != questionnaireQuestions.ID) return NotFound();

            if (ModelState.IsValid)
            {
                var existing = await _context.QuestionnaireQuestions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(q => q.ID == id);

                if (existing == null) return NotFound();

                questionnaireQuestions.Order = existing.Order; // garder l’ordre original

                _context.Update(questionnaireQuestions);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            LoadViewBags(questionnaireQuestions.IdMainDataSource);
            return View(questionnaireQuestions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var question = await _context.QuestionnaireQuestions.FindAsync(id);
            if (question == null) return NotFound();

            question.Active = !question.Active;
            _context.Update(question);
            await _context.SaveChangesAsync();

            var referer = Request.Headers["Referer"].ToString();
            return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MoveUp(int id, int idQuestionnaire)
        {
            var current = await _context.QuestionnaireQuestions.FindAsync(id);
            if (current == null) return NotFound();

            var above = await _context.QuestionnaireQuestions
                .Where(q => q.IdQuestionnaire == current.IdQuestionnaire && q.Order < current.Order)
                .OrderByDescending(q => q.Order)
                .FirstOrDefaultAsync();

            if (above != null)
            {
                (current.Order, above.Order) = (above.Order, current.Order);
                _context.UpdateRange(current, above);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", "Questionnaires", new { id = idQuestionnaire });
        }

        [HttpPost]
        public async Task<IActionResult> MoveDown(int id, int idQuestionnaire)
        {
            var current = await _context.QuestionnaireQuestions.FindAsync(id);
            if (current == null) return NotFound();

            var below = await _context.QuestionnaireQuestions
                .Where(q => q.IdQuestionnaire == current.IdQuestionnaire && q.Order > current.Order)
                .OrderBy(q => q.Order)
                .FirstOrDefaultAsync();

            if (below != null)
            {
                (current.Order, below.Order) = (below.Order, current.Order);
                _context.UpdateRange(current, below);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", "Questionnaires", new { id = idQuestionnaire });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var question = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .FirstOrDefaultAsync(q => q.ID == id);

            if (question == null) return NotFound();

            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.QuestionnaireQuestions.FindAsync(id);
            if (question != null)
            {
                _context.QuestionnaireQuestions.Remove(question);
                await _context.SaveChangesAsync();
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Ok();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var q = await _context.QuestionnaireQuestions.FindAsync(id);
            if (q == null) return NotFound();

            return Json(new
            {
                q.ID,
                q.Order,
                q.Name,
                q.Title,
                q.ShortTitle,
                q.Instructions,
                q.IdMainDataType,
                q.IdMainDataSource,
                q.IdSubDataType,
                q.IdSubDataSource
            });
        }

        private void LoadViewBags(int? selectedSourceId = null)
        {
            ViewBag.MainDataTypes = new List<SelectListItem>
            {
                new("1", "Champ texte"),
                new("2", "Radio Bouton"),
                new("3", "Case à cocher"),
                new("4", "Liste déroulante"),
                new("5", "Liste déroulante multiple"),
                new("6", "Tableau composé"),
                new("7", "Champ texte multiple")
            };

            ViewBag.SubDataTypes = new List<SelectListItem>
            {
                new("2", "Radio Bouton"),
                new("3", "Case à cocher")
            };

            var sources = _context.QuestionnaireDataSources
                .Select(s => new { s.ID, s.Name })
                .ToList();

            ViewBag.DataSources = new SelectList(sources, "ID", "Name", selectedSourceId);
        }




    }
}
