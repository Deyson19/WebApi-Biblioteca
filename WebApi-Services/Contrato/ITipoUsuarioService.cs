
using WebApi_DataAccess.Entities;
using WebApi_Helpers;

namespace WebApi_Services.Contrato
{
    public interface ITipoUsuarioService : BaseRepositorio<TipoUsuario>
    {
        public Task<Respuesta<TipoUsuario>> GetById(int id);

    }
}
