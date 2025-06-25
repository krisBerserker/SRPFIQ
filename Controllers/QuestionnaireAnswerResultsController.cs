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
    public class QuestionnaireAnswerResultsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnaireAnswerResultsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: QuestionnaireAnswerResults
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.QuestionnaireAnswerResults.Include(q => q.QuestionnaireAnswers).Include(q => q.QuestionnaireQuestions);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: QuestionnaireAnswerResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswerResults = await _context.QuestionnaireAnswerResults
                .Include(q => q.QuestionnaireAnswers)
                .Include(q => q.QuestionnaireQuestions)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireAnswerResults == null)
            {
                return NotFound();
            }

            return View(questionnaireAnswerResults);
        }

        // GET: QuestionnaireAnswerResults/Create
        public IActionResult Create()
        {
            ViewData["IdQuestionnaireAnswer"] = new SelectList(_context.QuestionnaireAnswers, "ID", "ID");
            ViewData["IdQuestionnaireQuestion"] = new SelectList(_context.QuestionnaireQuestions, "ID", "Instructions");
            return View();
        }

        // POST: QuestionnaireAnswerResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdQuestionnaireAnswer,IdQuestionnaireQuestion,Value")] QuestionnaireAnswerResults questionnaireAnswerResults)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaireAnswerResults);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuestionnaireAnswer"] = new SelectList(_context.QuestionnaireAnswers, "ID", "ID", questionnaireAnswerResults.IdQuestionnaireAnswer);
            ViewData["IdQuestionnaireQuestion"] = new SelectList(_context.QuestionnaireQuestions, "ID", "Instructions", questionnaireAnswerResults.IdQuestionnaireQuestion);
            return View(questionnaireAnswerResults);
        }

        // GET: QuestionnaireAnswerResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswerResults = await _context.QuestionnaireAnswerResults.FindAsync(id);
            if (questionnaireAnswerResults == null)
            {
                return NotFound();
            }
            ViewData["IdQuestionnaireAnswer"] = new SelectList(_context.QuestionnaireAnswers, "ID", "ID", questionnaireAnswerResults.IdQuestionnaireAnswer);
            ViewData["IdQuestionnaireQuestion"] = new SelectList(_context.QuestionnaireQuestions, "ID", "Instructions", questionnaireAnswerResults.IdQuestionnaireQuestion);
            return View(questionnaireAnswerResults);
        }

        // POST: QuestionnaireAnswerResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaireAnswer,IdQuestionnaireQuestion,Value")] QuestionnaireAnswerResults questionnaireAnswerResults)
        {
            if (id != questionnaireAnswerResults.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireAnswerResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireAnswerResultsExists(questionnaireAnswerResults.ID))
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
            ViewData["IdQuestionnaireAnswer"] = new SelectList(_context.QuestionnaireAnswers, "ID", "ID", questionnaireAnswerResults.IdQuestionnaireAnswer);
            ViewData["IdQuestionnaireQuestion"] = new SelectList(_context.QuestionnaireQuestions, "ID", "Instructions", questionnaireAnswerResults.IdQuestionnaireQuestion);
            return View(questionnaireAnswerResults);
        }

        // GET: QuestionnaireAnswerResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswerResults = await _context.QuestionnaireAnswerResults
                .Include(q => q.QuestionnaireAnswers)
                .Include(q => q.QuestionnaireQuestions)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireAnswerResults == null)
            {
                return NotFound();
            }

            return View(questionnaireAnswerResults);
        }

        // POST: QuestionnaireAnswerResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireAnswerResults = await _context.QuestionnaireAnswerResults.FindAsync(id);
            if (questionnaireAnswerResults != null)
            {
                _context.QuestionnaireAnswerResults.Remove(questionnaireAnswerResults);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireAnswerResultsExists(int id)
        {
            return _context.QuestionnaireAnswerResults.Any(e => e.ID == id);
        }
    }
}
