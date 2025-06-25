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
    public class BirthPlacesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public BirthPlacesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: BirthPlaces
        public async Task<IActionResult> Index()
        {
            return View(await _context.BirthPlaces.ToListAsync());
        }

        // GET: BirthPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var birthPlaces = await _context.BirthPlaces
                .FirstOrDefaultAsync(m => m.ID == id);
            if (birthPlaces == null)
            {
                return NotFound();
            }

            return View(birthPlaces);
        }

        // GET: BirthPlaces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BirthPlaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Active")] BirthPlaces birthPlaces)
        {
            if (ModelState.IsValid)
            {
                _context.Add(birthPlaces);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(birthPlaces);
        }

        // GET: BirthPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var birthPlaces = await _context.BirthPlaces.FindAsync(id);
            if (birthPlaces == null)
            {
                return NotFound();
            }
            return View(birthPlaces);
        }

        // POST: BirthPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Active")] BirthPlaces birthPlaces)
        {
            if (id != birthPlaces.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(birthPlaces);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BirthPlacesExists(birthPlaces.ID))
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
            return View(birthPlaces);
        }

        // GET: BirthPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var birthPlaces = await _context.BirthPlaces
                .FirstOrDefaultAsync(m => m.ID == id);
            if (birthPlaces == null)
            {
                return NotFound();
            }

            return View(birthPlaces);
        }

        // POST: BirthPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var birthPlaces = await _context.BirthPlaces.FindAsync(id);
            if (birthPlaces != null)
            {
                _context.BirthPlaces.Remove(birthPlaces);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BirthPlacesExists(int id)
        {
            return _context.BirthPlaces.Any(e => e.ID == id);
        }
    }
}
