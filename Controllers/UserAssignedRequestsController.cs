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
    public class UserAssignedRequestsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public UserAssignedRequestsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: UserAssignedRequests
        public async Task<IActionResult> Index(int id)
        {

            var utilisateurConnecter = _context.Users.FirstOrDefault(u => u.ID == id);

            if (_context.UserPermissions.Any(up => up.IdUser == utilisateurConnecter.ID && up.IdUserRole == 5))
            {
                var sRPFIQDbContext = _context.UserAssignedRequests.Include(u => u.Requests).Include(u => u.Users);
                return View(await sRPFIQDbContext.ToListAsync());
            }
            return View();
        }

        // GET: UserAssignedRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAssignedRequests = await _context.UserAssignedRequests
                .Include(u => u.Requests)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userAssignedRequests == null)
            {
                return NotFound();
            }

            return View(userAssignedRequests);
        }

        // GET: UserAssignedRequests/Create
        public IActionResult Create()
        {
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: UserAssignedRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRequest,IdUser")] UserAssignedRequests userAssignedRequests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userAssignedRequests);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Users");
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", userAssignedRequests.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userAssignedRequests.IdUser);
            return View(userAssignedRequests);
        }

        // GET: UserAssignedRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAssignedRequests = await _context.UserAssignedRequests.FindAsync(id);
            if (userAssignedRequests == null)
            {
                return NotFound();
            }
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", userAssignedRequests.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userAssignedRequests.IdUser);
            return View(userAssignedRequests);
        }

        // POST: UserAssignedRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdRequest,IdUser")] UserAssignedRequests userAssignedRequests)
        {
            if (id != userAssignedRequests.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAssignedRequests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAssignedRequestsExists(userAssignedRequests.ID))
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
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", userAssignedRequests.IdRequest);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userAssignedRequests.IdUser);
            return View(userAssignedRequests);
        }

        // GET: UserAssignedRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userAssignedRequests = await _context.UserAssignedRequests
                .Include(u => u.Requests)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userAssignedRequests == null)
            {
                return NotFound();
            }

            return View(userAssignedRequests);
        }

        // POST: UserAssignedRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userAssignedRequests = await _context.UserAssignedRequests.FindAsync(id);
            if (userAssignedRequests != null)
            {
                _context.UserAssignedRequests.Remove(userAssignedRequests);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAssignedRequestsExists(int id)
        {
            return _context.UserAssignedRequests.Any(e => e.ID == id);
        }
    }
}
