using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using WebApplication_SRPFIQ.ViewModels;
using WebApplication_SRPFIQ.Utils;

namespace WebApplication_SRPFIQ.Controllers
{
    public class UsersController : Controller
    {
        private readonly SRPFIQDbContext _context;

        public UsersController(SRPFIQDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var model = new RegisterViewModel
            {
                Roles = GetAvailableRoles()
            };
            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               var utils = new PasswordUtils();
                Users users = new Users
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    PasswordHash = utils.ComputeSha256Hash(model.Password),
                    Active = true,
                };
                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                // 2. Récupérer le rôle sélectionné (par nom ou ID)
                var role = _context.UserRoles.FirstOrDefault(r => r.ID == model.SelectedRole);
                if (role == null)
                {
                    ModelState.AddModelError("SelectedRole", "Rôle invalide.");
                    model.Roles = GetAvailableRoles();
                    return View(model);
                }

                // 3. Ajouter l’entrée dans UserPermissions
                var permission = new UserPermissions
                {
                    IdUser = users.ID,
                    IdUserRole = role.ID,
                };

                _context.UserPermissions.Add(permission);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Users");
            }
            model.Roles = GetAvailableRoles();
            return View(model);
        }

        [HttpPost]

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,UserName,PasswordHash,LastLoginDate,MustChangePassword,Active")] Users users)
        {
            if (id != users.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.ID))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }


        private List<SelectListItem> GetAvailableRoles()
        {

            return _context.UserRoles
                .Select(r => new SelectListItem
                {
                    Value = r.ID.ToString(),
                    Text = r.Name
                })
                .ToList();

        }
    }
}
