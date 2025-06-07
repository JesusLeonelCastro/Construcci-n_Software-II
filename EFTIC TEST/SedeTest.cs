using EFTIC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;


namespace EFTIC_TEST
{
    [TestClass]
    public class SedeTest
    {
        [TestMethod]
        public void Listar_Sedes()
        {
            var data = new List<Sede>
            {
                new Sede { SedeID = 1, Nombre_Sede = "Sede 1", Descripcion = "Descripción de Sede 1" },
                new Sede { SedeID = 2, Nombre_Sede = "Sede 2", Descripcion = "Descripción de Sede 2" },
                new Sede { SedeID = 3, Nombre_Sede = "Sede 3", Descripcion = "Descripción de Sede 3" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Sede>>();
            mockSet.As<IQueryable<Sede>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Sede>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Sede>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Sede>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Sede).Returns(mockSet.Object);

            var claseSede = new Sede();
            var resultado = claseSede.Listar(mockContext.Object);

            Assert.IsNotNull(resultado); 
            Assert.AreEqual(3, resultado.Count); 
            Assert.AreEqual("Sede 1", resultado[0].Nombre_Sede); // Verificar el nombre de la primera sede
            Assert.AreEqual("Sede 2", resultado[1].Nombre_Sede); // Verificar el nombre de la segunda sede
            Assert.AreEqual("Sede 3", resultado[2].Nombre_Sede); // Verificar el nombre de la tercera sede



        }

        [TestMethod]

        public void Buscar_Deberia_Devolver_Sedes_Correspondientes_Al_Criterio()
        {
            var dataSedes = new List<Sede>
            {
                new Sede { SedeID = 1, Nombre_Sede = "Sede Central" },
                new Sede { SedeID = 2, Nombre_Sede = "Sede Norte" },
                new Sede { SedeID = 3, Nombre_Sede = "Sede Sur" }
            }.AsQueryable(); 

            var mockSetSede = new Mock<DbSet<Sede>>();

            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.Provider).Returns(dataSedes.Provider);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.Expression).Returns(dataSedes.Expression);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.ElementType).Returns(dataSedes.ElementType);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.GetEnumerator()).Returns(dataSedes.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Sede).Returns(mockSetSede.Object);

            var sedeClass = new Sede(); // Instancia de la clase que contiene el método Buscar

           
        }

        [TestMethod]
        public void Buscar_No_Deberia_Devolver_Sedes_Cuando_No_Coincide_Criterio()
        {
            var dataSedes = new List<Sede>
            {
                new Sede { SedeID = 1, Nombre_Sede = "Sede Central" },
                new Sede { SedeID = 2, Nombre_Sede = "Sede Norte" },
                new Sede { SedeID = 3, Nombre_Sede = "Sede Sur" }
            }.AsQueryable(); // Convertir la lista en un IQueryable

            var mockSetSede = new Mock<DbSet<Sede>>();

            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.Provider).Returns(dataSedes.Provider);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.Expression).Returns(dataSedes.Expression);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.ElementType).Returns(dataSedes.ElementType);
            mockSetSede.As<IQueryable<Sede>>()
                .Setup(m => m.GetEnumerator()).Returns(dataSedes.GetEnumerator());

            // Mock del contexto para 'Model1'
            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Sede).Returns(mockSetSede.Object);

            var sedeClass = new Sede(); // Instancia de la clase que contiene el método Buscar

            
        }

        [TestMethod]
        public void Guardar_Sede()
        {
            var nuevaSede = new Sede
            {
                SedeID = 0,  // SedeID es 0, indicando que es una nueva sede
                Nombre_Sede = "Sede Test"
            };

            var mockSetSede = new Mock<DbSet<Sede>>();

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Sede).Returns(mockSetSede.Object);

            mockContext.Setup(m => m.SaveChanges()).Returns(1);

           

            
        }

        [TestMethod]
        public void Agregar_Ssede()
        {

        }
        [TestMethod]

        public void Guardar_Sedee()
        {

        }
        [TestMethod]

        public void Eliminar_Sede()
        {

        }
    }
}
