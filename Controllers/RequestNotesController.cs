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
    public class RequestNotesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public RequestNotesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: RequestNotes
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.RequestNotes.Include(r => r.Request).Include(r => r.User);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: RequestNotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestNotes = await _context.RequestNotes
                .Include(r => r.Request)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requestNotes == null)
            {
                return NotFound();
            }

            return View(requestNotes);
        }

        // GET: RequestNotes/Create
        public IActionResult Create()
        {
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: RequestNotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdRequest,IdUser,Note,CreatedDate,LastModifiedDate")] RequestNotes requestNotes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestNotes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", requestNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", requestNotes.IdUser);
            return View(requestNotes);
        }

        // GET: RequestNotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestNotes = await _context.RequestNotes.FindAsync(id);
            if (requestNotes == null)
            {
                return NotFound();
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", requestNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", requestNotes.IdUser);
            return View(requestNotes);
        }

        // POST: RequestNotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdRequest,IdUser,Note,CreatedDate,LastModifiedDate")] RequestNotes requestNotes)
        {
            if (id != requestNotes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestNotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestNotesExists(requestNotes.ID))
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
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", requestNotes.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", requestNotes.IdUser);
            return View(requestNotes);
        }

        // GET: RequestNotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestNotes = await _context.RequestNotes
                .Include(r => r.Request)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requestNotes == null)
            {
                return NotFound();
            }

            return View(requestNotes);
        }

        // POST: RequestNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestNotes = await _context.RequestNotes.FindAsync(id);
            if (requestNotes != null)
            {
                _context.RequestNotes.Remove(requestNotes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestNotesExists(int id)
        {
            return _context.RequestNotes.Any(e => e.ID == id);
        }
    }
}
