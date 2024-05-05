
using WebApi_DataAccess.Entities;
using WebApi_DataAccess;
using WebApi_Helpers;
using WebApi_Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Services.Implementacion
{
    public class PrestamoService(BibliotecaDbContext context) : IPrestamoService
    {
        private readonly BibliotecaDbContext _dbContext = context;

        public async Task<Respuesta<Prestamo>> Create(Prestamo entity)
        {
            if (entity == null)
            {
                return new Respuesta<Prestamo>
                {
                    IsSuccess = false,
                    Message = "No es un modelo correcto",
                    Result = entity
                };
            }
            await _dbContext.Prestamos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return new Respuesta<Prestamo>
            {
                IsSuccess = true,
                Message = "Registro creado",
                Result = entity
            };
        }

        public async Task<Respuesta<IEnumerable<Prestamo>>> GetAll()
        {
            var listado = await _dbContext.Prestamos.ToListAsync();
            return new Respuesta<IEnumerable<Prestamo>>
            {
                IsSuccess = listado.Any(),
                Result = listado,
                Message = listado.Any() ? "Listado de prestamos" : "No hay resultados"
            };
        }

        public async Task<Respuesta<Prestamo>> GetById(int id)
        {
            if (id == 0)
            {
                return new Respuesta<Prestamo>
                {
                    IsSuccess = false,
                    Message = "El valor del id no es correcto",
                    Result = null
                };
            }
            var prestamo = await GetPrestamo(id);
            return new Respuesta<Prestamo>
            {
                IsSuccess = prestamo != null,
                Message = prestamo != null ? "Se ha encontrado un prestamo" : "No hay resultados",
                Result = prestamo
            };
        }


        public async Task<bool> UsuarioTienePrestamo(string idUsuario)
        {
            var prestamo = await _dbContext.Prestamos.Where(x => x.FechaMaximaDevolucion < DateTime.Now).FirstOrDefaultAsync(x => x.IdentificacionUsuario.ToLower() == idUsuario.ToLower());
            if (prestamo != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<Prestamo> GetPrestamo(int id)
        {
            var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == id);
            return prestamo;
        }
    }
}
