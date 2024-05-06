using WebApi_DataAccess.Entities;
using WebApi_Helpers;

namespace WebApi_Services.Contrato
{
    public interface IPrestamoService : BaseRepositorio<Prestamo>
    {
        Task<bool> UsuarioTienePrestamo(string idUsuario);
        public Task<Respuesta<Prestamo>> GetById(int id);
        Task<Respuesta<bool>> ExisteUnUsuario(string idUsuario);
        Task<Respuesta<Prestamo>> Actualizar(ActualizarPrestamoViewModel actualizar);
        Task<Respuesta<Prestamo>> Eliminar(int eliminar);
    }
}
