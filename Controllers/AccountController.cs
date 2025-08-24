using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using WebApplication_SRPFIQ.Utils;
using WebApplication_SRPFIQ.ViewModels;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly SRPFIQDbContext _context;
    
    public AccountController(SRPFIQDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public ActionResult Register()
    {
        var model = new RegisterViewModel
        {
            Roles = GetAvailableRoles()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Trouver l'utilisateur par son email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == model.Email);

        // Vérification du mot de passe
        PasswordUtils utils = new PasswordUtils();
        string hachedPassword = utils.ComputeSha256Hash(model.Password);

        if (user == null || user.PasswordHash.Trim() != hachedPassword.Trim())
        {
            ModelState.AddModelError("", "Email ou mot de passe incorrect.");
            return View(model);
        }

        // Récupérer le rôle de l'utilisateur via la table UsersPermissions
        var role = await _context.UserPermissions
            .Include(up => up.UserRole)
            .Where(up => up.IdUser == user.ID)
            .Select(up => up.UserRole.Name)
            .FirstOrDefaultAsync();

        // Construction des claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
            new Claim(ClaimTypes.Email, user.UserName),
            new Claim(ClaimTypes.Name, user.FirstName)
        };

        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
       

        // Authentification via cookie
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        bool isAuth = HttpContext.User.Identity.IsAuthenticated;
        // Vérifie cette valeur en debug



        if (role == "Maman-Relais" || role == "Doulas")
        {
            //return RedirectToAction("Index", "Requests", new {user.ID});
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            PasswordUtils utils = new PasswordUtils();
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
          await  _context.SaveChangesAsync();

            return RedirectToAction("Login", "Account");
        }
        model.Roles = GetAvailableRoles();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
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

    //Récupérer l'id de l'utilisateur connecté
    //string userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
}
