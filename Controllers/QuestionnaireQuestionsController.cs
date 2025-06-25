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

        // GET: QuestionnaireQuestions
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.QuestionnaireQuestions.Include(q => q.Questionnaire).Include(q => q.QuestionnaireDataSources);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: QuestionnaireQuestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireQuestions = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireQuestions == null)
            {
                return NotFound();
            }

            return View(questionnaireQuestions);
        }

        // GET: QuestionnaireQuestions/Create
        public IActionResult Create()
        {
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID");
            ViewData["IdMainDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name");
            return View();
        }

        // POST: QuestionnaireQuestions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdQuestionnaire,Order,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaireQuestions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireQuestions.IdQuestionnaire);
            ViewData["IdMainDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireQuestions.IdMainDataSource);
            return View(questionnaireQuestions);
        }

        // GET: QuestionnaireQuestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireQuestions = await _context.QuestionnaireQuestions.FindAsync(id);
            if (questionnaireQuestions == null)
            {
                return NotFound();
            }
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireQuestions.IdQuestionnaire);
            ViewData["IdMainDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireQuestions.IdMainDataSource);
            return View(questionnaireQuestions);
        }

        // POST: QuestionnaireQuestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaire,Order,Name,Active,Title,ShortTitle,Instructions,IdMainDataType,IdMainDataSource,IdSubDataType,IdSubDataSource")] QuestionnaireQuestions questionnaireQuestions)
        {
            if (id != questionnaireQuestions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireQuestions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireQuestionsExists(questionnaireQuestions.ID))
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
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireQuestions.IdQuestionnaire);
            ViewData["IdMainDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireQuestions.IdMainDataSource);
            return View(questionnaireQuestions);
        }

        // GET: QuestionnaireQuestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireQuestions = await _context.QuestionnaireQuestions
                .Include(q => q.Questionnaire)
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireQuestions == null)
            {
                return NotFound();
            }

            return View(questionnaireQuestions);
        }

        // POST: QuestionnaireQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireQuestions = await _context.QuestionnaireQuestions.FindAsync(id);
            if (questionnaireQuestions != null)
            {
                _context.QuestionnaireQuestions.Remove(questionnaireQuestions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireQuestionsExists(int id)
        {
            return _context.QuestionnaireQuestions.Any(e => e.ID == id);
        }
    }
}
