using EFTIC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFTIC_TEST
{
    [TestClass]
    public class AsignarTest
    {
        private Mock<Model1> mockContext;
        private Mock<DbSet<Asignar>> mockDbSet;
        private List<Asignar> testData;

        [TestInitialize]
        public void Initialize()
        {
            // Configurar datos de prueba
            testData = new List<Asignar>
        {
            new Asignar
            {
                AsignarID = 1,
                AreaID = 1,
                UsuarioID = 1,
                InventarioID = 1,
                Area = new Area { AreaID = 1, Nombre_Area = "Administración" },
                Usuario = new Usuario { UsuarioID = 1, Nombre_Usuario = "Juan Perez" },
                Inventario = new Inventario { InventarioID = 1, Marca = "Laptop HP" }
            },
            new Asignar
            {
                AsignarID = 2,
                AreaID = 2,
                UsuarioID = 2,
                InventarioID = 2,
                Area = new Area { AreaID = 2, Nombre_Area = "Contabilidad" },
                Usuario = new Usuario { UsuarioID = 2, Nombre_Usuario = "Maria Garcia" },
                Inventario = new Inventario { InventarioID = 2, Marca = "Impresora Epson" }
            }
        };

            // Configurar mock DbSet
            mockDbSet = new Mock<DbSet<Asignar>>();
            var queryableData = testData.AsQueryable();

            mockDbSet.As<IQueryable<Asignar>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<Asignar>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<Asignar>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<Asignar>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            // Configurar Include para las relaciones
            mockDbSet.Setup(m => m.Include(It.IsAny<string>())).Returns(mockDbSet.Object);

            // Configurar mock Context
            mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Asignar).Returns(mockDbSet.Object);
        }

        [TestMethod]
        public void Listar_DeberiaRetornarTodasLasAsignacionesConRelaciones()
        {
            // Act
            var asignar = new Asignar();
            var resultado = asignar.Listar(mockContext.Object);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count);

            Assert.AreEqual("Administración", resultado[0].Area.Nombre_Area);
            Assert.AreEqual("Juan Perez", resultado[0].Usuario.Nombre_Usuario);
            Assert.AreEqual("Laptop HP", resultado[0].Inventario.Marca);
        }

        [TestMethod]
        public void Listar_DeberiaRetornarListaVaciaCuandoNoHayRegistros()
        {
            // Reconfigurar para lista vacía
            testData.Clear();
            var emptyQueryable = testData.AsQueryable();
            mockDbSet.As<IQueryable<Asignar>>().Setup(m => m.GetEnumerator()).Returns(() => emptyQueryable.GetEnumerator());

            // Act
            var asignar = new Asignar();
            var resultado = asignar.Listar(mockContext.Object);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Listar_DeberiaLanzarExcepcionCuandoOcurreErrorEnBaseDeDatos()
        {
            // Reconfigurar para lanzar excepción
            mockContext.Setup(c => c.Asignar).Throws(new Exception("Error simulado"));

            // Act
            var asignar = new Asignar();
            var resultado = asignar.Listar(mockContext.Object);
        }

        [TestMethod]
        public void Listar_DeberiaUsarContextoExternoCuandoSeProporciona()
        {
            // Act
            var asignar = new Asignar();
            var resultado = asignar.Listar(mockContext.Object);

            // Assert
            mockContext.Verify(m => m.Asignar, Times.AtLeastOnce());
        }



        [TestMethod]
        public void Agregar_Asignar()
        {

        }
        [TestMethod]

        public void Guardar_Asignar()
        {

        }
        [TestMethod]

        public void Eliminar_Asignar()
        {

        }

    }

}
    