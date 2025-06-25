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
    public class MaternalExperiencesThemesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public MaternalExperiencesThemesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: MaternalExperiencesThemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaternalExperiencesThemes.ToListAsync());
        }

        // GET: MaternalExperiencesThemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiencesThemes = await _context.MaternalExperiencesThemes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (maternalExperiencesThemes == null)
            {
                return NotFound();
            }

            return View(maternalExperiencesThemes);
        }

        // GET: MaternalExperiencesThemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaternalExperiencesThemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name,IsPrenatal")] MaternalExperiencesThemes maternalExperiencesThemes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maternalExperiencesThemes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maternalExperiencesThemes);
        }

        // GET: MaternalExperiencesThemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiencesThemes = await _context.MaternalExperiencesThemes.FindAsync(id);
            if (maternalExperiencesThemes == null)
            {
                return NotFound();
            }
            return View(maternalExperiencesThemes);
        }

        // POST: MaternalExperiencesThemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name,IsPrenatal")] MaternalExperiencesThemes maternalExperiencesThemes)
        {
            if (id != maternalExperiencesThemes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maternalExperiencesThemes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaternalExperiencesThemesExists(maternalExperiencesThemes.ID))
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
            return View(maternalExperiencesThemes);
        }

        // GET: MaternalExperiencesThemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiencesThemes = await _context.MaternalExperiencesThemes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (maternalExperiencesThemes == null)
            {
                return NotFound();
            }

            return View(maternalExperiencesThemes);
        }

        // POST: MaternalExperiencesThemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maternalExperiencesThemes = await _context.MaternalExperiencesThemes.FindAsync(id);
            if (maternalExperiencesThemes != null)
            {
                _context.MaternalExperiencesThemes.Remove(maternalExperiencesThemes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaternalExperiencesThemesExists(int id)
        {
            return _context.MaternalExperiencesThemes.Any(e => e.ID == id);
        }
    }
}
