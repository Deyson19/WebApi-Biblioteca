
using WebApi_Helpers;

namespace WebApi_Services
{
    public interface BaseRepositorio<T>
    {
        public Task<Respuesta<T>> Create(T entity);
        public Task<Respuesta<IEnumerable<T>>> GetAll();
    }
}
