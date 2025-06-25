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
    public class QuestionnaireDataSourceOptionsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnaireDataSourceOptionsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: QuestionnaireDataSourceOptions
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.QuestionnaireDataSourceOptions.Include(q => q.QuestionnaireDataSources);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: QuestionnaireDataSourceOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSourceOptions = await _context.QuestionnaireDataSourceOptions
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireDataSourceOptions == null)
            {
                return NotFound();
            }

            return View(questionnaireDataSourceOptions);
        }

        // GET: QuestionnaireDataSourceOptions/Create
        public IActionResult Create()
        {
            ViewData["IdQuestionnaireDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name");
            return View();
        }

        // POST: QuestionnaireDataSourceOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdQuestionnaireDataSource,Active,DisplayText")] QuestionnaireDataSourceOptions questionnaireDataSourceOptions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaireDataSourceOptions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdQuestionnaireDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireDataSourceOptions.IdQuestionnaireDataSource);
            return View(questionnaireDataSourceOptions);
        }

        // GET: QuestionnaireDataSourceOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSourceOptions = await _context.QuestionnaireDataSourceOptions.FindAsync(id);
            if (questionnaireDataSourceOptions == null)
            {
                return NotFound();
            }
            ViewData["IdQuestionnaireDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireDataSourceOptions.IdQuestionnaireDataSource);
            return View(questionnaireDataSourceOptions);
        }

        // POST: QuestionnaireDataSourceOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdQuestionnaireDataSource,Active,DisplayText")] QuestionnaireDataSourceOptions questionnaireDataSourceOptions)
        {
            if (id != questionnaireDataSourceOptions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireDataSourceOptions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireDataSourceOptionsExists(questionnaireDataSourceOptions.ID))
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
            ViewData["IdQuestionnaireDataSource"] = new SelectList(_context.QuestionnaireDataSources, "ID", "Name", questionnaireDataSourceOptions.IdQuestionnaireDataSource);
            return View(questionnaireDataSourceOptions);
        }

        // GET: QuestionnaireDataSourceOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSourceOptions = await _context.QuestionnaireDataSourceOptions
                .Include(q => q.QuestionnaireDataSources)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireDataSourceOptions == null)
            {
                return NotFound();
            }

            return View(questionnaireDataSourceOptions);
        }

        // POST: QuestionnaireDataSourceOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireDataSourceOptions = await _context.QuestionnaireDataSourceOptions.FindAsync(id);
            if (questionnaireDataSourceOptions != null)
            {
                _context.QuestionnaireDataSourceOptions.Remove(questionnaireDataSourceOptions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireDataSourceOptionsExists(int id)
        {
            return _context.QuestionnaireDataSourceOptions.Any(e => e.ID == id);
        }
    }
}
