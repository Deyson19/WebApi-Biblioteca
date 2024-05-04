
using WebApi_DataAccess.Entities;
using WebApi_DataAccess;
using WebApi_Helpers;
using WebApi_Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace WebApi_Services.Implementacion
{
    public class TipoUsuarioService(BibliotecaDbContext context) : ITipoUsuarioService
    {
        private readonly BibliotecaDbContext _dbContext = context;

        public async Task<Respuesta<TipoUsuario>> Create(TipoUsuario entity)
        {
            if (entity == null)
            {
                return new Respuesta<TipoUsuario>
                {
                    IsSuccess = false,
                    Message = "El modelo no es correcto",
                    Result = entity
                };
            }
            else
            {
                await _dbContext.TipoUsuarios.AddAsync(entity);
                await _dbContext.SaveChangesAsync();

                return new Respuesta<TipoUsuario>
                {
                    IsSuccess = true,
                    Message = "Registro creado",
                    Result = entity
                };
            }
        }

        public async Task<Respuesta<IEnumerable<TipoUsuario>>> GetAll()
        {
            var listado = await _dbContext.TipoUsuarios.ToListAsync();
            return new Respuesta<IEnumerable<TipoUsuario>>
            {
                IsSuccess = listado.Count != 0,
                Result = listado,
                Message = listado.Count != 0 ? "Listado de tipos de usuario" : "No hay registros"
            };
        }

        public async Task<Respuesta<TipoUsuario>> GetById(int id)
        {
            var tipoUsuario = await _dbContext.TipoUsuarios.FirstOrDefaultAsync(x => x.Id == id);
            return new Respuesta<TipoUsuario>
            {
                IsSuccess = tipoUsuario != null,
                Message = tipoUsuario != null ? "Tipo de usuario encontrado" : "No hay coincidencias",
                Result = tipoUsuario
            };
        }
    }
}
