using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_DataAccess.Entities
{
    public class TipoUsuario
    {
        [Key]
        public int Id { get; set; }
        public required string Nombre { get; set; }
    }
}