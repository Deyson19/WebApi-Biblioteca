using Microsoft.EntityFrameworkCore;
using Moq;
using WebApi_DataAccess.Entities;
using WebApi_DataAccess;
using WebApi_Services.Contrato;
using WebApi_Services.Implementacion;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace WebApi_TestServices
{
    [TestClass]
    public class PrestamoTest
    {
        //NOTA: Para ejecutar las pruebas, es necesario contar con registros en la base de datos
        private IUnidadTrabajo _unidadTrabajo;
        private BibliotecaDbContext _dbContext;
        [TestInitialize]
        public void Setup()
        {

            var options = new DbContextOptions<BibliotecaDbContext>();
            _dbContext = new BibliotecaDbContext(options);
            _unidadTrabajo = new UnidadTrabajo(_dbContext);
        }
        [TestMethod]
        public async Task GetAll_IsSuccess()
        {
            //Act 
            var response = await _unidadTrabajo.Prestamo.GetAll();
            Assert.IsTrue(response.IsSuccess);
        }
        [TestMethod]
        public async Task GetAll_IsNotNull()
        {
            //Act
            var response = await _unidadTrabajo.Prestamo.GetAll();
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GetById_ExistLoad()
        {
            // Arrange
            int id = 3;

            // Act
            var response = await _unidadTrabajo.Prestamo.GetById(id);

            // Assert
            Assert.IsTrue(response.IsSuccess);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GetById_NonExistLoad()
        {
            // Arrange
            int id = 100;

            // Act
            var response = await _unidadTrabajo.Prestamo.GetById(id);

            // Assert
            Assert.IsFalse(response.IsSuccess);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task Eliminar_IdInvalido_RetornaError()
        {
            // Arrange
            var mockContext = new Mock<BibliotecaDbContext>();
            var service = new PrestamoService(mockContext.Object);

            // Act
            var result = await service.Eliminar(0);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No es correcto el id", result.Message);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public async Task Eliminar_PrestamoNoEncontrado_RetornaError()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.Eliminar(100);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No hay registro", result.Message);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public async Task Eliminar_PrestamoEncontrado_RetornaPrestamoEliminado()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.Eliminar(2);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Se ha eliminado el registro", result.Message);
        }

        [TestMethod]
        public async Task ExisteUnUsuario_UsuarioEncontrado_RetornaTrue()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.ExisteUnUsuario("9285350348");

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Result);
        }

        [TestMethod]
        public async Task ExisteUnUsuario_UsuarioNoEncontrado_RetornaFalse()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.ExisteUnUsuario("123456789");

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.IsFalse(result.Result);
        }

        [TestMethod]
        public async Task UsuarioTienePrestamo_PrestamoEncontrado_RetornaTrue()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.UsuarioTienePrestamo("9285350348");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UsuarioTienePrestamo_PrestamoNoEncontrado_RetornaFalse()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.UsuarioTienePrestamo("123456789");

            // Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public async Task Create_PrestamoNull_RetornaError()
        {
            // Arrange
            var mockContext = new Mock<BibliotecaDbContext>();
            var service = new PrestamoService(mockContext.Object);

            // Act
            var result = await service.Create(null);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("No es un modelo correcto", result.Message);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public async Task Create_PrestamoValido_RetornaPrestamoCreado()
        {
            // Arrange
            var prestamo = new Prestamo
            {
                Isbn = "123456789",
                IdentificacionUsuario = "123456789",
                TipoUsuarioId = 1,
                FechaPrestamo = DateTime.Now,
                FechaMaximaDevolucion = DateTime.Now.AddDays(10)
            };
            // Act
            var result = await _unidadTrabajo.Prestamo.Create(prestamo);
            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Registro creado", result.Message);
            Assert.AreEqual(prestamo, result.Result);
        }

        [TestMethod]
        public async Task Actualizar_IdInvalido_RetornaError()
        {
            // Arrange
            var mockContext = new Mock<BibliotecaDbContext>();
            var service = new PrestamoService(mockContext.Object);

            // Act
            var result = await service.Actualizar(new ActualizarPrestamoViewModel { Id = 0 });

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("El id no es valido", result.Message);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public async Task Actualizar_PrestamoNoEncontrado_RetornaError()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.Actualizar(new ActualizarPrestamoViewModel { Id = 1 });

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("El modelo no es correcto", result.Message);
            Assert.IsNull(result.Result);
        }

        [TestMethod]
        public async Task Actualizar_PrestamoEncontrado_RetornaPrestamoActualizado()
        {
            // Act
            var result = await _unidadTrabajo.Prestamo.Actualizar(new ActualizarPrestamoViewModel { Id = 5, Isbn = "UEYG572619", IdentificacionUsuario = "9417199404", TipoUsuarioId = 2 });

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Se el prestamo ha sido actualizado", result.Message);
            Assert.AreEqual("UEYG572619", result.Result.Isbn);
            Assert.AreEqual("9417199404", result.Result.IdentificacionUsuario);
            Assert.AreEqual(2, result.Result.TipoUsuarioId);
        }

    }
}