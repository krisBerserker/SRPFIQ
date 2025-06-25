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
    public class MedicalNotesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public MedicalNotesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: MedicalNotes
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.MedicalNotes.Include(m => m.Requests).Include(m => m.Users);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: MedicalNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalNotes = await _context.MedicalNotes
                .Include(m => m.Requests)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicalNotes == null)
            {
                return NotFound();
            }

            return View(medicalNotes);
        }

        // GET: MedicalNotes/Create
        public IActionResult Create()
        {
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: MedicalNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdRequest,IdUser,EventDate,Description,Notes,CreatedDate,LastModifiedDate")] MedicalNotes medicalNotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", medicalNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", medicalNotes.IdUser);
            return View(medicalNotes);
        }

        // GET: MedicalNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalNotes = await _context.MedicalNotes.FindAsync(id);
            if (medicalNotes == null)
            {
                return NotFound();
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", medicalNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", medicalNotes.IdUser);
            return View(medicalNotes);
        }

        // POST: MedicalNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdRequest,IdUser,EventDate,Description,Notes,CreatedDate,LastModifiedDate")] MedicalNotes medicalNotes)
        {
            if (id != medicalNotes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalNotesExists(medicalNotes.ID))
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
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", medicalNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", medicalNotes.IdUser);
            return View(medicalNotes);
        }

        // GET: MedicalNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalNotes = await _context.MedicalNotes
                .Include(m => m.Requests)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicalNotes == null)
            {
                return NotFound();
            }

            return View(medicalNotes);
        }

        // POST: MedicalNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalNotes = await _context.MedicalNotes.FindAsync(id);
            if (medicalNotes != null)
            {
                _context.MedicalNotes.Remove(medicalNotes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalNotesExists(int id)
        {
            return _context.MedicalNotes.Any(e => e.ID == id);
        }
    }
}
