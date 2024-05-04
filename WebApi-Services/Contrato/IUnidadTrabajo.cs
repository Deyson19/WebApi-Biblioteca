namespace WebApi_Services.Contrato
{
    public interface IUnidadTrabajo : IDisposable
    {
        public ITipoUsuarioService TipoUsuario { get; }
        public IPrestamoService Prestamo { get; }
    }
}
