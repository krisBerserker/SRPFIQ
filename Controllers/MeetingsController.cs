using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using WebApplication_SRPFIQ.ViewModels;

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

            var model = new SuiviViewModel
            {
                Numero = meetings.MeetingNumber,
                Date = meetings.EventDate,
                Duree = meetings.Amount,
                TypeRencontre = (meetings.IdMeetingType == 1 ? "Téléphonique" : meetings.IdMeetingType == 2 ? "Texto" : "Présentiel"),
                Notes = meetings.Note,
                Actions = meetings.Action,
                Delais = meetings.Delay,
                Mode = "Affichage"
            };

            return View(model);
        }

        // GET: Meetings/Create/5
        public IActionResult Create(int id)
        {
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            var model = new SuiviViewModel
            {
                IDRequest = id,
                Date = DateTime.Now,
                Mode = "Ajout"
            };
            return View("Details", model);
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuiviViewModel suiviViewModel)
        {
            int numberLastMeeting = _context.Meetings.Where(m =>
            m.IdRequest == suiviViewModel.IDRequest).OrderByDescending(m=> m.EventDate).Last().MeetingNumber;
            Meetings meetings = new Meetings
            {
                MeetingNumber = numberLastMeeting+1,
                EventDate = suiviViewModel.Date,
                Amount = suiviViewModel.Duree,
                Delay = suiviViewModel.Delais,
                IdMeetingType = (suiviViewModel.TypeRencontre == "Téléphonique"? 1 : suiviViewModel.TypeRencontre == "Texto" ? 2 : 3),
                Action = suiviViewModel.Actions,
                IdRequest = suiviViewModel.IDRequest,
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now,
                Note = suiviViewModel.Notes,
                IdUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                
            };
            if (ModelState.IsValid)
            {
                _context.Add(meetings);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Requests", new { id = suiviViewModel.IDRequest });
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
