using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2DAMT.Models;

namespace PracticaMvcCore2DAMT.Data
{
    public class LibrosContext:DbContext
    {
        public LibrosContext(DbContextOptions<LibrosContext> options)
            :base(options){ }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
