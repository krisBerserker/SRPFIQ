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
    public class UserPermissionsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public UserPermissionsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: UserPermissions
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.UserPermissions.Include(u => u.UserRole).Include(u => u.Users);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: UserPermissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermissions = await _context.UserPermissions
                .Include(u => u.UserRole)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userPermissions == null)
            {
                return NotFound();
            }

            return View(userPermissions);
        }

        // GET: UserPermissions/Create
        public IActionResult Create()
        {
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "ID", "ID");
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: UserPermissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdUserRole,IdUser")] UserPermissions userPermissions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userPermissions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "ID", "ID", userPermissions.IdUserRole);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userPermissions.IdUser);
            return View(userPermissions);
        }

        // GET: UserPermissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermissions = await _context.UserPermissions.FindAsync(id);
            if (userPermissions == null)
            {
                return NotFound();
            }
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "ID", "ID", userPermissions.IdUserRole);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userPermissions.IdUser);
            return View(userPermissions);
        }

        // POST: UserPermissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdUserRole,IdUser")] UserPermissions userPermissions)
        {
            if (id != userPermissions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPermissions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPermissionsExists(userPermissions.ID))
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
            ViewData["IdUserRole"] = new SelectList(_context.UserRoles, "ID", "ID", userPermissions.IdUserRole);
            ViewData["IdUser"] = new SelectList(_context.Users, "ID", "FirstName", userPermissions.IdUser);
            return View(userPermissions);
        }

        // GET: UserPermissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPermissions = await _context.UserPermissions
                .Include(u => u.UserRole)
                .Include(u => u.Users)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userPermissions == null)
            {
                return NotFound();
            }

            return View(userPermissions);
        }

        // POST: UserPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userPermissions = await _context.UserPermissions.FindAsync(id);
            if (userPermissions != null)
            {
                _context.UserPermissions.Remove(userPermissions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPermissionsExists(int id)
        {
            return _context.UserPermissions.Any(e => e.ID == id);
        }
    }
}
