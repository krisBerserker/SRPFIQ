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
            if (id == null)
            {
                return NotFound();
            }

            var questionnaires = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaires == null)
            {
                return NotFound();
            }

            return View(questionnaires);
        }

        // GET: Questionnaires/Create
        public IActionResult Create()
        {
            return View();

        }

        // POST: Questionnaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Active,CreatedDate")] Questionnaires questionnaires)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questionnaires);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionnaires);
        }

        // GET: Questionnaires/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaires = await _context.Questionnaires
             .Include(q => q.Questions)
             .FirstOrDefaultAsync(q => q.ID == id);


            if (questionnaires == null)
            {
                return NotFound();
            }
            return View(questionnaires);
        }


       

        // POST: Questionnaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active,CreatedDate")] Questionnaires questionnaires)
        {
            if (id != questionnaires.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            return View(questionnaires);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var questionnaire = await _context.Questionnaires.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            questionnaire.Active = !questionnaire.Active;
            _context.Update(questionnaire);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // GET: Questionnaires/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionnaires = await _context.Questionnaires
                .FirstOrDefaultAsync(m => m.ID == id);
            if (questionnaires == null)
            {
                return NotFound();
            }

            return View(questionnaires);
        }

        // POST: Questionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var questionnaires = await _context.Questionnaires.FindAsync(id);
            if (questionnaires != null)
            {
                _context.Questionnaires.Remove(questionnaires);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionnairesExists(int id)
        {
            return _context.Questionnaires.Any(e => e.ID == id);
        }
    }
}
