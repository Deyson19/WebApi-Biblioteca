using System.ComponentModel.DataAnnotations;

namespace WebApi_DataAccess.Entities
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Isbn { get; set; }
        [MaxLength(10)]
        public string IdentificacionUsuario { get; set; }
        public required DateTime FechaPrestamo { get; set; }
        public required DateTime FechaMaximaDevolucion { get; set; }
        public int TipoUsuarioId { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
    public class CrearPrestamoViewModel
    {
        [MaxLength(6, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string Isbn { get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string IdentificacionUsuario { get; set; }
        //[MaxLength(1, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public int TipoUsuarioId { get; set; }
    }
    public class ActualizarPrestamoViewModel
    {
        public int Id { get; set; }

        [MaxLength(6, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string Isbn { get; set; }
        [MaxLength(10, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string IdentificacionUsuario { get; set; }
        //[MaxLength(1, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public int TipoUsuarioId { get; set; }
    }
    
    public class ObtenerPrestamoPorId
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuario { get; set; }
        public string FechaMaximaDevolucion { get; set; }
    }
}