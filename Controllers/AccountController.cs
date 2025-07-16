using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication_SRPFIQ.Data;
using WebApplication_SRPFIQ.Models;
using WebApplication_SRPFIQ.Utils;

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

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Trouver l'utilisateur par son email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == model.Email);

        // Vérification du mot de passe
        Utils utils = new Utils();
        string hachedPassword = utils.ComputeSha256Hash(model.Password);

        if (user == null /*|| user.PasswordHash.Trim() != hachedPassword.Trim()*/)
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
            new Claim(ClaimTypes.Name, user.UserName)
        };

        if (!string.IsNullOrEmpty(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        //Récupérer l'id de l'utilisateur connecté
        //string userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        // Authentification via cookie
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        if (role == "Maman-Relais")
        {
            return RedirectToAction("Index", "Requests", new {user.ID});
        }
        else
        {
            return RedirectToAction("Index", "Requests");
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}
