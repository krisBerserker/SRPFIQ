using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.DTOs;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Controllers
{
    public class RequestsController : Controller
    {
        private readonly SRPFIQDbContext _context;
   
        
        public RequestsController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(int id)
        {
            var utilisateurConnecter = _context.Users.FirstOrDefault(u => u.ID == id);
            
            if (_context.UserPermissions.Any(up=> up.IdUser == utilisateurConnecter.ID && up.IdUserRole != 5))
            {
                List<UserAssignedRequests> listeDemande = _context.UserAssignedRequests.Where(uar=> uar.IdUser== utilisateurConnecter.ID).ToList();
                List<Requests> userRequest = new List<Requests>();
                foreach (var request in listeDemande)
                {
                  userRequest.Add(_context.Requests.First(r => r.ID == request.IdRequest));
                }
                bool existeEvaluation = false;
                bool existeMaternalExperience = false;
                string maternalExperience = "";
                string evaluation = "";
                Meetings? dernierMeeting = new Meetings();
                List<RequestRelaiDTO> requestRelaiDTOs = new List<RequestRelaiDTO>();
                foreach (var item in userRequest)
                {
                    existeEvaluation = _context.QuestionnaireAnswers.Any(q => q.IdRequest == item.ID);
                    existeMaternalExperience = _context.MaternalExperiences.Any(me => me.IdRequest == item.ID);
                    dernierMeeting = _context.Meetings.Where(m => m.IdRequest == item.ID).OrderByDescending(m=> m.EventDate).FirstOrDefault();
                    switch (existeEvaluation)
                    {
                        case false:
                            evaluation = "À faire";
                            break;
                        case true:
                            if (_context.QuestionnaireAnswers.First(q => q.IdRequest == item.ID).IdStatuts == 2)
                            {
                                evaluation = "Terminée";
                            }
                            else
                            {
                                evaluation = "Débutée";
                            }
                            break;
                    }
                    switch (existeMaternalExperience)
                    {
                        case false:
                            maternalExperience = "À faire";
                            break;
                        case true:
                            maternalExperience = "Terminée";
                            break;
                    }
                    RequestRelaiDTO requestRelaiDTO = new RequestRelaiDTO 
                    {
                        ID = item.ID,
                        NumberFolio = item.FolioNumber,
                        NameMother = item.FullName,
                        DPA = item.EstimatedDeliveryDate,
                        DateDernierSuivi = (dernierMeeting == null ? null : dernierMeeting.EventDate),
                        Evaluation = evaluation,
                        VecuMaternel = maternalExperience,
                    };
                   
                    requestRelaiDTOs.Add(requestRelaiDTO);
                };
                 /*Retourne uniquement les demandes de l'utilisateur connecter*/;
                return View(requestRelaiDTOs.ToList());
            }
            return View("Vous n'êtes pas autorisé à accéder a cette page");
        }

 

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .Include(r => r.UserClosedBy)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["IdUserClosedBy"] = new SelectList(_context.Users, "ID", "FirstName");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FolioNumber,FullName,MedicalCoverage,ReceivedRequestAt,PhoneNumber,Email,Adresse,ZipCode,EstimatedDeliveryDate,NbPregnancy,IsMonoparental,SpokenLanguage,ImmigrationStatus,NbChilds,ChildsAge,IdUserClosedBy,ClosedDate,CreatedDate,LastModifiedDate")] Requests requests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUserClosedBy"] = new SelectList(_context.Users, "ID", "FirstName", requests.IdUserClosedBy);
            return View(requests);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests.FindAsync(id);
            if (requests == null)
            {
                return NotFound();
            }
            ViewData["IdUserClosedBy"] = new SelectList(_context.Users, "ID", "FirstName", requests.IdUserClosedBy);
            return View(requests);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FolioNumber,FullName,MedicalCoverage,ReceivedRequestAt,PhoneNumber,Email,Adresse,ZipCode,EstimatedDeliveryDate,NbPregnancy,IsMonoparental,SpokenLanguage,ImmigrationStatus,NbChilds,ChildsAge,IdUserClosedBy,ClosedDate,CreatedDate,LastModifiedDate")] Requests requests)
        {
            if (id != requests.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestsExists(requests.ID))
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
            ViewData["IdUserClosedBy"] = new SelectList(_context.Users, "ID", "FirstName", requests.IdUserClosedBy);
            return View(requests);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requests = await _context.Requests
                .Include(r => r.UserClosedBy)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (requests == null)
            {
                return NotFound();
            }

            return View(requests);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requests = await _context.Requests.FindAsync(id);
            if (requests != null)
            {
                _context.Requests.Remove(requests);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestsExists(int id)
        {
            return _context.Requests.Any(e => e.ID == id);
        }
    }
}
