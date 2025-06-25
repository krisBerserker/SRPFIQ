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
    public class MeetingsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public MeetingsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Meetings
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.Meetings.Include(m => m.Request).Include(m => m.User);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetings = await _context.Meetings
                .Include(m => m.Request)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meetings == null)
            {
                return NotFound();
            }

            return View(meetings);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdRequest,IdUser,MeetingNumber,EventDate,IdMeetingType,Amount,Note,Action,Delay,CreatedDate,LastModifiedDate")] Meetings meetings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", meetings.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", meetings.IdUser);
            return View(meetings);
        }

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetings = await _context.Meetings.FindAsync(id);
            if (meetings == null)
            {
                return NotFound();
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", meetings.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", meetings.IdUser);
            return View(meetings);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdRequest,IdUser,MeetingNumber,EventDate,IdMeetingType,Amount,Note,Action,Delay,CreatedDate,LastModifiedDate")] Meetings meetings)
        {
            if (id != meetings.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingsExists(meetings.ID))
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
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", meetings.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", meetings.IdUser);
            return View(meetings);
        }

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetings = await _context.Meetings
                .Include(m => m.Request)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meetings == null)
            {
                return NotFound();
            }

            return View(meetings);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetings = await _context.Meetings.FindAsync(id);
            if (meetings != null)
            {
                _context.Meetings.Remove(meetings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingsExists(int id)
        {
            return _context.Meetings.Any(e => e.ID == id);
        }
    }
}
