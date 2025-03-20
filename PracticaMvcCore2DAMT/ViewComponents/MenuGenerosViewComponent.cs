using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2DAMT.Models;
using PracticaMvcCore2DAMT.Repositories;

namespace PracticaMvcCore2DAMT.ViewComponents
{
    public class MenuGenerosViewComponent:ViewComponent
    {
        private RepositoryLibros repo;
        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = await this.repo.GetGenerosAsync();
            return View(generos);

        }
    }
}
