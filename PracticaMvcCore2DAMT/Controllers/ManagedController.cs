using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2DAMT.Filters;
using PracticaMvcCore2DAMT.Models;
using PracticaMvcCore2DAMT.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2DAMT.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;
        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login
            (string email, string password)
        {
            Usuario usuario = await this.repo.LogInUsuarioAsync(email, password);
            // VERIFICAMOS QUE EXISTE ESTE USUARIO
            if (usuario != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                Claim claimId =
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(claimId);

                Claim claimName =
                    new Claim(ClaimTypes.Name, usuario.Nombre);
                identity.AddClaim(claimName);

                Claim claimApellido =
                    new Claim(("Apellidos"), usuario.Apellidos);
                identity.AddClaim(claimApellido);

                Claim claimEmail =
                    new Claim(("Email"), usuario.Email);
                identity.AddClaim(claimEmail);

                Claim claimFoto =
                    new Claim(("Fotoperfil"), usuario.Foto.ToString());
                identity.AddClaim(claimFoto);

                ClaimsPrincipal usuarioPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    usuarioPrincipal);
                return RedirectToAction("Perfil","Managed");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> Perfil()
        {
            return View();
        }
    }
}
