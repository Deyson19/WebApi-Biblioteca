
using Microsoft.EntityFrameworkCore;
using WebApi_DataAccess.Entities;

namespace WebApi_DataAccess
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext() { }
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) { }

        //*Configurar string de conexion para el proyecto de pruebas
        private string connectionStringPostgre = "Host=localhost; Database=WebApi-Biblioteca; Username=postgres; Password=deyson.dev";
        private string connectionStringSql = "Server=.;Database=Biblioteca-Api;User Id=Dev;Password=deyson.dev;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(connectionStringPostgre);
            optionsBuilder.UseSqlServer(connectionStringSql);
        }


        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }



    }
}