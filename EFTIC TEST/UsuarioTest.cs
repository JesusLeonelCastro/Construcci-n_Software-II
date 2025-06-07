using EFTIC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EFTIC_TEST
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void Listar_Usuarios()
        {
            var dataUsuarios = new List<Usuario>
            {
                new Usuario
                {
                    UsuarioID = 1,
                    Nombre_Usuario = "Usuario 1",
                    Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede 1" },
                    Area = new Area { AreaID = 1, Nombre_Area = "Area 1" }
                },
                new Usuario
                {
                    UsuarioID = 2,
                    Nombre_Usuario = "Usuario 2",
                    Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede 2" },
                    Area = new Area { AreaID = 2, Nombre_Area = "Area 2" }
                }
            }.AsQueryable();

            var mockSetUsuario = new Mock<DbSet<Usuario>>();
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.Provider).Returns(dataUsuarios.Provider);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.Expression).Returns(dataUsuarios.Expression);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.ElementType).Returns(dataUsuarios.ElementType);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.GetEnumerator()).Returns(dataUsuarios.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockSetUsuario.Object);

            var usuarioClass = new Usuario();

        }


        [TestMethod]
        public void Listar_Usuarios_DeberiaRetornarTodosLosUsuariosConSedesYAreas()
        {
            // 1. ARRANGE - Preparar datos de prueba
            var datosPrueba = new List<Usuario>
            {
                new Usuario
                {
                    UsuarioID = 1,
                    DNI = "12345678",
                    Nombre_Usuario = "Juan",
                    Apellido_Usuario = "Perez",
                    Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede Central" },
                    Area = new Area { AreaID = 1, Nombre_Area = "Recursos Humanos" }
                },
                new Usuario
                {
                    UsuarioID = 2,
                    DNI = "87654321",
                    Nombre_Usuario = "Maria",
                    Apellido_Usuario = "Gomez",
                    Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede Norte" },
                    Area = new Area { AreaID = 2, Nombre_Area = "Tecnología" }
                }
            }.AsQueryable();

            // 1.1. Configurar el DbSet mock
            var mockDbSet = new Mock<DbSet<Usuario>>();
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(datosPrueba.Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(datosPrueba.Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(datosPrueba.ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(datosPrueba.GetEnumerator());

            // 1.2. Configurar los Includes para las relaciones
            mockDbSet.Setup(m => m.Include("Sede")).Returns(mockDbSet.Object);
            mockDbSet.Setup(m => m.Include("Area")).Returns(mockDbSet.Object);

            // 1.3. Configurar el contexto mock
            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockDbSet.Object);

            // 2. ACT - Ejecutar el método a probar
            var usuarioService = new Usuario();
            var resultado = usuarioService.Listar(mockContext.Object);

            // 3. ASSERT - Verificar los resultados
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count);

            // Verificar primer usuario
            Assert.AreEqual(1, resultado[0].UsuarioID);
            Assert.AreEqual("Juan", resultado[0].Nombre_Usuario);
            Assert.AreEqual("Sede Central", resultado[0].Sede.Nombre_Sede);
            Assert.AreEqual("Recursos Humanos", resultado[0].Area.Nombre_Area);

            // Verificar segundo usuario
            Assert.AreEqual(2, resultado[1].UsuarioID);
            Assert.AreEqual("Maria", resultado[1].Nombre_Usuario);
            Assert.AreEqual("Sede Norte", resultado[1].Sede.Nombre_Sede);
            Assert.AreEqual("Tecnología", resultado[1].Area.Nombre_Area);
        }

        

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Listar_Usuarios_ConErrorEnBaseDeDatos_DeberiaLanzarExcepcion()
        {
            // 1. ARRANGE
            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Throws(new Exception("Error simulado de base de datos"));

            // 2. ACT
            var usuarioService = new Usuario();
            var resultado = usuarioService.Listar(mockContext.Object);

            // 3. ASSERT - El atributo ExpectedException maneja la verificación
        }


        [TestMethod]
        public void Autenticar_CuandoDNIyPasswordSonCorrectos_DeberiaRetornarTrue()
        {
            // ARRANGE (Preparar datos falsos y configurar Moq)
            var datosFalsos = new List<Usuario>
            {
                new Usuario { DNI = "12345678", Password = "clave123" },
                new Usuario { DNI = "87654321", Password = "clave456" }
            }.AsQueryable();

            // 1. Mock de DbSet<Usuario>
            var mockDbSet = new Mock<DbSet<Usuario>>();
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(datosFalsos.Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(datosFalsos.Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(datosFalsos.ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(datosFalsos.GetEnumerator());

            // 2. Mock del contexto (Model1)
            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockDbSet.Object);

            // 3. Crear instancia de Usuario con credenciales correctas
            var usuario = new Usuario { DNI = "12345678", Password = "clave123" };

            // 4. Inyectar el contexto mockeado usando Reflection (sin modificar la clase)
            var dbField = typeof(Usuario).GetField("db", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbField.SetValue(usuario, mockContext.Object);

            // ACT (Ejecutar el método)
            bool resultado = usuario.Autenticar();

            // ASSERT (Verificar que retorne true)
            Assert.IsTrue(resultado, "Debería retornar true cuando DNI y Password son correctos");
        }

        [TestMethod]
        public void Autenticar_CuandoPasswordEsIncorrecto_DeberiaRetornarFalse()
        {
            // ARRANGE
            var datosFalsos = new List<Usuario>
            {
                new Usuario { DNI = "12345678", Password = "clave123" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Usuario>>();
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(datosFalsos.Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(datosFalsos.Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(datosFalsos.ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(datosFalsos.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockDbSet.Object);

            var usuario = new Usuario { DNI = "12345678", Password = "claveINCORRECTA" };

            // Inyectar contexto usando Reflection
            var dbField = typeof(Usuario).GetField("db", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbField.SetValue(usuario, mockContext.Object);

            // ACT
            bool resultado = usuario.Autenticar();

            // ASSERT
            Assert.IsFalse(resultado, "Debería retornar false cuando el Password es incorrecto");
        }

        [TestMethod]
        public void Autenticar_CuandoDNINoExiste_DeberiaRetornarFalse()
        {
            // ARRANGE
            var datosFalsos = new List<Usuario>
            {
                new Usuario { DNI = "12345678", Password = "clave123" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Usuario>>();
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(datosFalsos.Provider);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(datosFalsos.Expression);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(datosFalsos.ElementType);
            mockDbSet.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(datosFalsos.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockDbSet.Object);

            var usuario = new Usuario { DNI = "00000000", Password = "clave123" }; // DNI inexistente

            // Inyectar contexto usando Reflection
            var dbField = typeof(Usuario).GetField("db", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            dbField.SetValue(usuario, mockContext.Object);

            // ACT
            bool resultado = usuario.Autenticar();

            // ASSERT
            Assert.IsFalse(resultado, "Debería retornar false cuando el DNI no existe");
        }


        [TestMethod]
        public void Agregar_Usuario()
        {

        }
        [TestMethod]

        public void Guardar_USuario()
        {

        }
        [TestMethod]

        public void Guardar_Eliminar()
        {

        }

    }
}
