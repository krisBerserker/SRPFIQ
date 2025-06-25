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
    public class MaternalExperiencesController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public MaternalExperiencesController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: MaternalExperiences
        public async Task<IActionResult> Index()
        {
            var sRPFIQDbContext = _context.MaternalExperiences.Include(m => m.BirthPlaces).Include(m => m.MedicalTransferReason).Include(m => m.Requests);
            return View(await sRPFIQDbContext.ToListAsync());
        }

        // GET: MaternalExperiences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiences = await _context.MaternalExperiences
                .Include(m => m.BirthPlaces)
                .Include(m => m.MedicalTransferReason)
                .Include(m => m.Requests)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (maternalExperiences == null)
            {
                return NotFound();
            }

            return View(maternalExperiences);
        }

        // GET: MaternalExperiences/Create
        public IActionResult Create()
        {
            ViewData["IdBirthPlace"] = new SelectList(_context.BirthPlaces, "ID", "Name");
            ViewData["IdMedicalTransferReason"] = new SelectList(_context.MedicalTransferReasons, "ID", "Name");
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber");
            return View();
        }

        // POST: MaternalExperiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdRequest,BirthDate,SizeAtBirthDays,SizeAtBithWeeks,BabyName,BabyGender,IdBirthPlace,BirthPlaceOther,IsNaturalDelivery,HadInductionLabor,HadNaturalReliefs,HadPsychologicalSupport,HadMembranesRupture,HadEpidural,HadOtherAnesthetic,HadEpisiotomy,HadSuctionCupsForceps,HadPlannedCesarean,HadUnPlannedCesarean,HadDeceased,HasBeenTranfered,IdMedicalTransferReason,IsBreastFeedingAtBirth,IsBreastFeedingSixWeeks,BreastFeedingNotes,CreatedDate,LastModifiedDate")] MaternalExperiences maternalExperiences)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maternalExperiences);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBirthPlace"] = new SelectList(_context.BirthPlaces, "ID", "Name", maternalExperiences.IdBirthPlace);
            ViewData["IdMedicalTransferReason"] = new SelectList(_context.MedicalTransferReasons, "ID", "Name", maternalExperiences.IdMedicalTransferReason);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", maternalExperiences.IdRequest);
            return View(maternalExperiences);
        }

        // GET: MaternalExperiences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiences = await _context.MaternalExperiences.FindAsync(id);
            if (maternalExperiences == null)
            {
                return NotFound();
            }
            ViewData["IdBirthPlace"] = new SelectList(_context.BirthPlaces, "ID", "Name", maternalExperiences.IdBirthPlace);
            ViewData["IdMedicalTransferReason"] = new SelectList(_context.MedicalTransferReasons, "ID", "Name", maternalExperiences.IdMedicalTransferReason);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", maternalExperiences.IdRequest);
            return View(maternalExperiences);
        }

        // POST: MaternalExperiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,IdRequest,BirthDate,SizeAtBirthDays,SizeAtBithWeeks,BabyName,BabyGender,IdBirthPlace,BirthPlaceOther,IsNaturalDelivery,HadInductionLabor,HadNaturalReliefs,HadPsychologicalSupport,HadMembranesRupture,HadEpidural,HadOtherAnesthetic,HadEpisiotomy,HadSuctionCupsForceps,HadPlannedCesarean,HadUnPlannedCesarean,HadDeceased,HasBeenTranfered,IdMedicalTransferReason,IsBreastFeedingAtBirth,IsBreastFeedingSixWeeks,BreastFeedingNotes,CreatedDate,LastModifiedDate")] MaternalExperiences maternalExperiences)
        {
            if (id != maternalExperiences.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maternalExperiences);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaternalExperiencesExists(maternalExperiences.ID))
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
            ViewData["IdBirthPlace"] = new SelectList(_context.BirthPlaces, "ID", "Name", maternalExperiences.IdBirthPlace);
            ViewData["IdMedicalTransferReason"] = new SelectList(_context.MedicalTransferReasons, "ID", "Name", maternalExperiences.IdMedicalTransferReason);
            ViewData["IdRequest"] = new SelectList(_context.Requests, "ID", "FolioNumber", maternalExperiences.IdRequest);
            return View(maternalExperiences);
        }

        // GET: MaternalExperiences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maternalExperiences = await _context.MaternalExperiences
                .Include(m => m.BirthPlaces)
                .Include(m => m.MedicalTransferReason)
                .Include(m => m.Requests)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (maternalExperiences == null)
            {
                return NotFound();
            }

            return View(maternalExperiences);
        }

        // POST: MaternalExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maternalExperiences = await _context.MaternalExperiences.FindAsync(id);
            if (maternalExperiences != null)
            {
                _context.MaternalExperiences.Remove(maternalExperiences);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaternalExperiencesExists(int id)
        {
            return _context.MaternalExperiences.Any(e => e.ID == id);
        }
    }
}
