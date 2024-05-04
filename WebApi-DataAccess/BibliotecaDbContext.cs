
using Microsoft.EntityFrameworkCore;
using WebApi_DataAccess.Entities;

namespace WebApi_DataAccess
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext() { }
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }

    }
}