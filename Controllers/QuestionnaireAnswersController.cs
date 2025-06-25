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
    public class QuestionnaireAnswersController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnaireAnswersController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: QuestionnaireAnswers
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.QuestionnaireAnswers.Include(q => q.Questionnaires).Include(q => q.Requests).Include(q => q.Users);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: QuestionnaireAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswers = await _context.QuestionnaireAnswers
                .Include(q => q.Questionnaires)
                .Include(q => q.Requests)
                .Include(q => q.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireAnswers == null)
            {
                return NotFound();
            }

            return View(questionnaireAnswers);
        }

        // GET: QuestionnaireAnswers/Create
        public IActionResult Create()
        {
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID");
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: QuestionnaireAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdQuestionnaire,IdRequest,IdUser,IdStatuts,CreatedDate,LastModifiedDate")] QuestionnaireAnswers questionnaireAnswers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaireAnswers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireAnswers.IdQuestionnaire);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", questionnaireAnswers.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", questionnaireAnswers.IdUser);
            return View(questionnaireAnswers);
        }

        // GET: QuestionnaireAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswers = await _context.QuestionnaireAnswers.FindAsync(id);
            if (questionnaireAnswers == null)
            {
                return NotFound();
            }
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireAnswers.IdQuestionnaire);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", questionnaireAnswers.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", questionnaireAnswers.IdUser);
            return View(questionnaireAnswers);
        }

        // POST: QuestionnaireAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaire,IdRequest,IdUser,IdStatuts,CreatedDate,LastModifiedDate")] QuestionnaireAnswers questionnaireAnswers)
        {
            if (id != questionnaireAnswers.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireAnswers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireAnswersExists(questionnaireAnswers.ID))
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
            ViewData["IdQuestionnaire"] = new SelectList(_context.Questionnaires, "ID", "ID", questionnaireAnswers.IdQuestionnaire);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", questionnaireAnswers.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", questionnaireAnswers.IdUser);
            return View(questionnaireAnswers);
        }

        // GET: QuestionnaireAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireAnswers = await _context.QuestionnaireAnswers
                .Include(q => q.Questionnaires)
                .Include(q => q.Requests)
                .Include(q => q.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireAnswers == null)
            {
                return NotFound();
            }

            return View(questionnaireAnswers);
        }

        // POST: QuestionnaireAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireAnswers = await _context.QuestionnaireAnswers.FindAsync(id);
            if (questionnaireAnswers != null)
            {
                _context.QuestionnaireAnswers.Remove(questionnaireAnswers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireAnswersExists(int id)
        {
            return _context.QuestionnaireAnswers.Any(e => e.ID == id);
        }
    }
}
