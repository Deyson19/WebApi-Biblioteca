
using WebApi_DataAccess;
using WebApi_Services.Contrato;

namespace WebApi_Services.Implementacion
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly BibliotecaDbContext _dbContext;

        public ITipoUsuarioService TipoUsuario { get; private set; }

        public IPrestamoService Prestamo { get; private set; }

        public UnidadTrabajo(BibliotecaDbContext dbContext)
        {
            _dbContext = dbContext;
            TipoUsuario = new TipoUsuarioService(dbContext);
            Prestamo = new PrestamoService(dbContext);
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
