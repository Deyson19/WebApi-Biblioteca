using Microsoft.AspNetCore.Mvc;
using WebApi_DataAccess.Entities;
using WebApi_Helpers;
using WebApi_Services.Contrato;

namespace WebApi_Biblioteca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController(IUnidadTrabajo unidadTrabajo) : ControllerBase
    {
        private readonly IUnidadTrabajo _unidadTrabajo = unidadTrabajo;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var listado = await _unidadTrabajo.Prestamo.GetAll();
            return Ok(listado);
        }
        [HttpGet("{idprestamo}")]
        public async Task<IActionResult> Get(int idprestamo)
        {
            if (idprestamo == 0)
            {
                return BadRequest();
            }
            var prestamo = await _unidadTrabajo.Prestamo.GetById(idprestamo);
            if (prestamo.IsSuccess)
            {
                var prestamoPorId = new ObtenerPrestamoPorId
                {
                    Id = prestamo.Result.Id,
                    Isbn = prestamo.Result.Isbn,
                    IdentificacionUsuario = prestamo.Result.IdentificacionUsuario,
                    TipoUsuario = prestamo.Result.TipoUsuarioId,
                    FechaMaximaDevolucion = prestamo.Result.FechaMaximaDevolucion.ToString("d"),
                };
                return Ok(prestamoPorId);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearPrestamoViewModel prestamo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var existeTipoUsuario = await ObtenerTipoUsuario(prestamo.TipoUsuarioId);
            if (existeTipoUsuario == null)
            {
                return BadRequest(new { mensaje = "Tipo de usuario no permitido en la biblioteca" });
            }
            if (existeTipoUsuario.Id == 3)
            {
                var existePrestamo = await UsuarioTienePrestamo(prestamo.IdentificacionUsuario);
                if (existePrestamo)
                {
                    return BadRequest(
                        new
                        {
                            mensaje = $"El usuario con identificación {prestamo.IdentificacionUsuario} ya tiene un libro prestado por lo cual no se le puede realizar otro préstamo"
                        }
                    );
                }
                var nuevoPrestamoInvitado = CrearPrestamoModel(prestamo);
                if (nuevoPrestamoInvitado != null)
                {
                    var agregarPrestamo = await _unidadTrabajo.Prestamo.Create(nuevoPrestamoInvitado);
                    if (agregarPrestamo.IsSuccess)
                    {
                        return Ok(new
                        {
                            id = nuevoPrestamoInvitado.Id,
                            fechaDevolucion = nuevoPrestamoInvitado.FechaMaximaDevolucion.ToString("d")
                        });
                    }
                    return BadRequest(agregarPrestamo.Message);
                }
                return BadRequest(new { mensaje = "No se pudo crear el préstamo" });
            }

            var nuevoPrestamo = CrearPrestamoModel(prestamo);
            if (nuevoPrestamo == null)
            {
                return BadRequest();
            }
            var resultado = await _unidadTrabajo.Prestamo.Create(nuevoPrestamo);
            if (resultado.IsSuccess)
            {
                return Ok(new
                {
                    id = nuevoPrestamo.IdentificacionUsuario,
                    fechaDevolucion = nuevoPrestamo.FechaMaximaDevolucion.ToString("d")
                });
            }
            return BadRequest(resultado.Message);
        }
        [HttpDelete("{idPrestamo}")]
        public async Task<IActionResult> Delete(int idPrestamo)
        {
            if (idPrestamo ==0)
            {
                return BadRequest(new { mensaje = "El id no es correcto" });
            }
            var eliminar = await _unidadTrabajo.Prestamo.Eliminar(idPrestamo);
            if (eliminar.IsSuccess)
            {
                return StatusCode(StatusCodes.Status200OK, eliminar.IsSuccess);
            }
            return BadRequest(new { mensaje = eliminar.Message });
        }
        [HttpPut("{idPrestamo}")]
        public async Task<IActionResult> Update(int idPrestamo,[FromBody] ActualizarPrestamoViewModel model)
        {
            if (idPrestamo ==0 || idPrestamo != model.Id)
            {
                return BadRequest("Id no es correcto");
            }
            if (ModelState.IsValid)
            {
                var prestamo = await _unidadTrabajo.Prestamo.Actualizar(model);
                if (prestamo.IsSuccess)
                {
                    return Ok(prestamo.Result);
                }
                return NotFound(new {mensaje= prestamo.Message });
            }
            return BadRequest(new { mensaje = "No es correcto el modelo" });
        }



        private Prestamo CrearPrestamoModel(CrearPrestamoViewModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Prestamo
            {
                IdentificacionUsuario = model.IdentificacionUsuario,
                Isbn = RandomIsbn.IsbnNuevo(model.Isbn),
                FechaPrestamo = DateTime.Now,
                TipoUsuarioId = model.TipoUsuarioId,
                FechaMaximaDevolucion = CantidadMaximaPrestamo.FechaMaximaPrestamo(model.TipoUsuarioId)
            };
        }

        private async Task<TipoUsuario> ObtenerTipoUsuario(int id)
        {
            var resultado = await _unidadTrabajo.TipoUsuario.GetById(id);
            return resultado.Result;
        }

        private async Task<bool> UsuarioTienePrestamo(string idUsuario) => await _unidadTrabajo.Prestamo.UsuarioTienePrestamo(idUsuario);

    }
}