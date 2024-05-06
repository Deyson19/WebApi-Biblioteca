
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

        public async Task<Respuesta<Prestamo>> Actualizar(ActualizarPrestamoViewModel actualizar)
        {
            if (actualizar.Id == 0)
            {
                return new Respuesta<Prestamo>
                {
                    IsSuccess = false,
                    Message = "El id no es valido",
                    Result = null
                };
            }
            var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.Id == actualizar.Id);
            if (prestamo != null)
            {
                prestamo.Isbn = actualizar.Isbn;
                prestamo.IdentificacionUsuario = actualizar.IdentificacionUsuario;
                prestamo.TipoUsuarioId = actualizar.TipoUsuarioId;

                _dbContext.Prestamos.Update(prestamo);
                await _dbContext.SaveChangesAsync();
                return new Respuesta<Prestamo>
                {
                    IsSuccess = true,
                    Message = "El prestamo ha sido actualizado",
                    Result = prestamo
                };
            }
            return new Respuesta<Prestamo>
            {
                IsSuccess = false,
                Message = "El modelo no es correcto",
                Result = null
            };
        }

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

        public async Task<Respuesta<Prestamo>> Eliminar(int eliminar)
        {
            if (eliminar ==0)
            {
                return new Respuesta<Prestamo>
                {
                    IsSuccess = false,
                    Message = "No es correcto el id",
                    Result = null
                };
            }
            var prestamo = await _dbContext.Prestamos.FirstOrDefaultAsync(x=>x.Id ==eliminar);
            if (prestamo !=null)
            {
                _dbContext.Prestamos.Remove(prestamo);
                await _dbContext.SaveChangesAsync();
                return new Respuesta<Prestamo>
                {
                    IsSuccess = true,
                    Message = "Se ha eliminado el registro",
                    Result = prestamo
                };
            }
            return new Respuesta<Prestamo>
            {
                IsSuccess = false,
                Message = "No hay registro",
                Result = null
            };
        }

        public async Task<Respuesta<bool>> ExisteUnUsuario(string idUsuario)
        {
            var usuario = await _dbContext.Prestamos.FirstOrDefaultAsync(x => x.IdentificacionUsuario.ToLower() == idUsuario.ToLower());
            if (usuario != null)
            {
                return new Respuesta<bool>
                {
                    IsSuccess = true,
                    Message ="Ya existe un usuario con ese documento",
                    Result = true
                };
            }
            return new Respuesta<bool>
            {
                IsSuccess = false,
                Message = "Puedes crear el registro",
                Result = false
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
            var prestamo = await _dbContext.Prestamos.Where(x => x.FechaMaximaDevolucion > DateTime.Now).FirstOrDefaultAsync(x => x.IdentificacionUsuario.ToLower() == idUsuario.ToLower());
            if (prestamo == null)
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
