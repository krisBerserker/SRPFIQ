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
    public class QuestionnaireDataSourcesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public QuestionnaireDataSourcesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: QuestionnaireDataSources
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionnaireDataSources.ToListAsync());
        }

        // GET: QuestionnaireDataSources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSources = await _context.QuestionnaireDataSources
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireDataSources == null)
            {
                return NotFound();
            }

            return View(questionnaireDataSources);
        }

        // GET: QuestionnaireDataSources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionnaireDataSources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Active,CreatedDate")] QuestionnaireDataSources questionnaireDataSources)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaireDataSources);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionnaireDataSources);
        }

        // GET: QuestionnaireDataSources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSources = await _context.QuestionnaireDataSources.FindAsync(id);
            if (questionnaireDataSources == null)
            {
                return NotFound();
            }
            return View(questionnaireDataSources);
        }

        // POST: QuestionnaireDataSources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active,CreatedDate")] QuestionnaireDataSources questionnaireDataSources)
        {
            if (id != questionnaireDataSources.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionnaireDataSources);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionnaireDataSourcesExists(questionnaireDataSources.ID))
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
            return View(questionnaireDataSources);
        }

        // GET: QuestionnaireDataSources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaireDataSources = await _context.QuestionnaireDataSources
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaireDataSources == null)
            {
                return NotFound();
            }

            return View(questionnaireDataSources);
        }

        // POST: QuestionnaireDataSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaireDataSources = await _context.QuestionnaireDataSources.FindAsync(id);
            if (questionnaireDataSources != null)
            {
                _context.QuestionnaireDataSources.Remove(questionnaireDataSources);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnaireDataSourcesExists(int id)
        {
            return _context.QuestionnaireDataSources.Any(e => e.ID == id);
        }
    }
}
