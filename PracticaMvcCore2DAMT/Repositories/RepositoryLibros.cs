using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2DAMT.Data;
using PracticaMvcCore2DAMT.Models;

namespace PracticaMvcCore2DAMT.Repositories
{
    public class RepositoryLibros
    {
        private LibrosContext context;
        public RepositoryLibros(LibrosContext context)
        {
            this.context = context;
        }

        public async Task FinalizarCompraAsync
            (List<int> idsLibros, int idUsuario)
        {
            DateOnly fecha = DateOnly.FromDateTime(DateTime.Now);
            int idFactura = await this.GetLastIdFacturaAsync();
            foreach (int idLibro in idsLibros)
            {
                int idPedido = await this.GetLastIdPedidoAsync();
                Pedido pedido = new Pedido();
                pedido.IdPedido = idPedido;
                pedido.IdFactura = idFactura;
                pedido.Fecha = fecha;
                pedido.IdLibro = idLibro;
                pedido.IdUsuario = idUsuario;
                pedido.Cantidad = 1;
                await this.context.Pedidos.AddAsync(pedido);
                await this.context.SaveChangesAsync();
            }
        }
        public async Task<int> GetLastIdFacturaAsync()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(x => x.IdFactura) + 1;
            }
        }
        public async Task<int> GetLastIdPedidoAsync()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(x => x.IdPedido)+1;
            }
        }
        public async Task<Usuario> LogInUsuarioAsync
            (string email, string password)
        {
            Usuario usuario =
                await this.context.Usuarios
                .Where(x => x.Email == email
                && x.Pass == password).FirstOrDefaultAsync();
            return usuario;
        }
        public async Task<List<Libro>> GetLibrosSessionAsync
            (List<int> idsLibros)
        {
            return await this.context.Libros
                .Where(x => idsLibros.Contains(x.IdLibro))
                .ToListAsync();
        }
        public async Task<Libro> FindLibro
            (int idLibro)
        {
            return await this.context.Libros
                .Where(x => x.IdLibro == idLibro)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Libro>> GetLibrosGeneroAsync
            (int idGenero)
        {
            return await this.context.Libros
                .Where(x => x.IdGenero == idGenero)
                .ToListAsync();
        }
        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await this.context.Generos.ToListAsync();
        }
        public async Task<List<Libro>> GetLibrosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }
    }
}
