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
    public class MedicalTransferReasonsController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public MedicalTransferReasonsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: MedicalTransferReasons
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicalTransferReasons.ToListAsync());
        }

        // GET: MedicalTransferReasons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalTransferReason = await _context.MedicalTransferReasons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicalTransferReason == null)
            {
                return NotFound();
            }

            return View(medicalTransferReason);
        }

        // GET: MedicalTransferReasons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalTransferReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name")] MedicalTransferReason medicalTransferReason)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicalTransferReason);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicalTransferReason);
        }

        // GET: MedicalTransferReasons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalTransferReason = await _context.MedicalTransferReasons.FindAsync(id);
            if (medicalTransferReason == null)
            {
                return NotFound();
            }
            return View(medicalTransferReason);
        }

        // POST: MedicalTransferReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name")] MedicalTransferReason medicalTransferReason)
        {
            if (id != medicalTransferReason.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicalTransferReason);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalTransferReasonExists(medicalTransferReason.ID))
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
            return View(medicalTransferReason);
        }

        // GET: MedicalTransferReasons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalTransferReason = await _context.MedicalTransferReasons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medicalTransferReason == null)
            {
                return NotFound();
            }

            return View(medicalTransferReason);
        }

        // POST: MedicalTransferReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalTransferReason = await _context.MedicalTransferReasons.FindAsync(id);
            if (medicalTransferReason != null)
            {
                _context.MedicalTransferReasons.Remove(medicalTransferReason);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicalTransferReasonExists(int id)
        {
            return _context.MedicalTransferReasons.Any(e => e.ID == id);
        }
    }
}
