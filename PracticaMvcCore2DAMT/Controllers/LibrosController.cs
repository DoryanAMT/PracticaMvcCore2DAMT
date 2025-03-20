using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PracticaMvcCore2DAMT.Extensions;
using PracticaMvcCore2DAMT.Filters;
using PracticaMvcCore2DAMT.Models;
using PracticaMvcCore2DAMT.Repositories;
using System.Security.Claims;

namespace PracticaMvcCore2DAMT.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Carrito
            (int? idLibroEliminar)
        {
            if (HttpContext.Session.GetObject<List<int>>("IDSLIBROS").IsNullOrEmpty())
            {
                ViewData["MENSAJE"] = "No hay nada en el carrito";
                return View();
            }
            else
            {
                List<int> idsLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
                if (idLibroEliminar != null)
                {
                    idsLibros.Remove(idLibroEliminar.Value);
                    if (idsLibros.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSLIBROS");
                        ViewData["MENSAJE"] = "No hay nada en el carrito";
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSLIBROS", idsLibros);
                    }
                }
                List<Libro> librosCarrito = await this.repo.GetLibrosSessionAsync(idsLibros);
                return View(librosCarrito);
            }
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> FinalizarCompra
            ()
        {
            int idUser = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<int> idsLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
            await this.repo.FinalizarCompraAsync(idsLibros, idUser);
            HttpContext.Session.Remove("IDSLIBROS");
            return RedirectToAction("Perfil", "Managed");

        }
        public async Task<IActionResult> Detalle
            (int idLibro, int? agregarCarrito)
        {
            //  AGREGAR AL CARRITO
            if (agregarCarrito != null)
            {
                List<int> idsLibros;
                if (HttpContext.Session.GetObject<List<int>>("IDSLIBROS") == null)
                {
                    idsLibros = new List<int>();
                }
                else
                {
                    idsLibros = HttpContext.Session.GetObject<List<int>>("IDSLIBROS");
                }
                idsLibros.Add(agregarCarrito.Value);
                HttpContext.Session.SetObject("IDSLIBROS", idsLibros);
            }

            // ENCONTRAR LIBRO
            Libro libro = await this.repo.FindLibro(idLibro);
            return View(libro);
        }
        public async Task<IActionResult> LibrosGenero
            (int idGenero)
        {
            List<Libro> librosGenero = await this.repo.GetLibrosGeneroAsync(idGenero);
            return View(librosGenero);
        }
        public async Task<IActionResult> Index()
        {
            List<Libro> libros = await this.repo.GetLibrosAsync(); ;
            return View(libros);
        }
    }
}
