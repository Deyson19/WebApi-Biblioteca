using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_DataAccess.Entities
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Isbn { get; set; }
        [ForeignKey("UsuarioId")]
        [MaxLength(10)]
        public string IdentificacionUsuario { get; set; }
        public required DateTime FechaPrestamo { get; set; }
        public required DateTime FechaMaximaDevolucion { get; set; }
    }
    public class CrearPrestamoViewModel
    {
        [MaxLength(6, ErrorMessage = "El campo {0} no puede superar los {1} caracteres")]
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public int TipoUsuarioId { get; set; }
    }
    public class ObtenerPrestamoViewModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string IdentificacionUsuario { get; set; }
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}