using EFTIC.Controllers;
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
    public class AreaTest
    {
        private Mock<Area> _mockArea;
        private Mock<Sede> _mockSede;
        private AreaController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockArea = new Mock<Area>();
            _mockSede = new Mock<Sede>();
        }

        [TestMethod]
        public void Listar_Area()
        {
            var dataAreas = new List<Area>
                {
                    new Area
                    {
                        AreaID = 1,
                        Nombre_Area = "Area 1",
                        Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede 1" }
                    },
                    new Area
                    {
                        AreaID = 2,
                        Nombre_Area = "Area 2",
                        Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede 2" }
                    }
                }.AsQueryable(); // Convertir la lista en un IQueryable

            var mockSetArea = new Mock<DbSet<Area>>();

            mockSetArea.As<IQueryable<Area>>()
                .Setup(m => m.Provider).Returns(dataAreas.Provider);
            mockSetArea.As<IQueryable<Area>>()
                .Setup(m => m.Expression).Returns(dataAreas.Expression);
            mockSetArea.As<IQueryable<Area>>()
                .Setup(m => m.ElementType).Returns(dataAreas.ElementType);
            mockSetArea.As<IQueryable<Area>>()
                .Setup(m => m.GetEnumerator()).Returns(dataAreas.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Area).Returns(mockSetArea.Object);

            var areaService = new Area(); //  usar la clase correcta que contiene el método Listar.

        }

        [TestMethod]
        public void Listar_Area_RetornaTodasLasAreasConSedes()
        {
            // 1. ARRANGE - Preparar datos de prueba y mocks
            var datosPrueba = new List<Area>
            {
                new Area
                {
                    AreaID = 1,
                    Nombre_Area = "Recursos Humanos",
                    Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede Central" }
                },
                new Area
                {
                    AreaID = 2,
                    Nombre_Area = "Tecnología",
                    Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede Norte" }
                }
            }.AsQueryable();

            // 1.1. Configurar el DbSet mock
            var mockDbSet = new Mock<DbSet<Area>>();
            mockDbSet.As<IQueryable<Area>>().Setup(m => m.Provider).Returns(datosPrueba.Provider);
            mockDbSet.As<IQueryable<Area>>().Setup(m => m.Expression).Returns(datosPrueba.Expression);
            mockDbSet.As<IQueryable<Area>>().Setup(m => m.ElementType).Returns(datosPrueba.ElementType);
            mockDbSet.As<IQueryable<Area>>().Setup(m => m.GetEnumerator()).Returns(datosPrueba.GetEnumerator());

            // 1.2. Configurar el Include para que funcione con las relaciones
            mockDbSet.Setup(m => m.Include("Sede")).Returns(mockDbSet.Object);

            // 1.3. Configurar el contexto mock
            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Area).Returns(mockDbSet.Object);

            // 2. ACT - Ejecutar el método a probar
            var areaService = new Area();
            var resultado = areaService.Listar(mockContext.Object);

            // 3. ASSERT - Verificar los resultados
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count);
            Assert.AreEqual("Recursos Humanos", resultado[0].Nombre_Area);
            Assert.AreEqual("Sede Central", resultado[0].Sede.Nombre_Sede);
            Assert.AreEqual("Tecnología", resultado[1].Nombre_Area);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Listar_Area_ConError_LanzaExcepcion()
        {
            // 1. ARRANGE - Configurar mock para lanzar excepción
            var mockDbSet = new Mock<DbSet<Area>>();
            var mockContext = new Mock<Model1>();

            mockContext.Setup(c => c.Area).Throws(new Exception("Error simulado"));

            // 2. ACT - Ejecutar método (debe lanzar excepción)
            var areaService = new Area();
            var resultado = areaService.Listar(mockContext.Object);

            // 3. ASSERT - El atributo ExpectedException maneja la verificación
        }

       
    }
}
