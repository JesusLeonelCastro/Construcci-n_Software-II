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
    public class FallasReportadasTest
    {
        [TestMethod]
        public void Lista_FallasReportas()
        {
            var dataFallas = new List<Falla>
            {
                new Falla { FallaID = 1, Descripcion_Falla = "Falla 1" },
                new Falla { FallaID = 2, Descripcion_Falla = "Falla 2" }
            }.AsQueryable(); // Convertir la lista en un IQueryable

            var mockSetFalla = new Mock<DbSet<Falla>>();

            mockSetFalla.As<IQueryable<Falla>>()
                .Setup(m => m.Provider).Returns(dataFallas.Provider);
            mockSetFalla.As<IQueryable<Falla>>()
                .Setup(m => m.Expression).Returns(dataFallas.Expression);
            mockSetFalla.As<IQueryable<Falla>>()
                .Setup(m => m.ElementType).Returns(dataFallas.ElementType);
            mockSetFalla.As<IQueryable<Falla>>()
                .Setup(m => m.GetEnumerator()).Returns(dataFallas.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Falla).Returns(mockSetFalla.Object);

            var fallaClass = new Falla();

            var resultado = fallaClass.Listar(mockContext.Object);

            Assert.IsNotNull(resultado); 
            Assert.AreEqual(2, resultado.Count); 
            Assert.AreEqual("Falla 1", resultado[0].Descripcion_Falla); // Verifica la descripción de la primera falla
            Assert.AreEqual("Falla 2", resultado[1].Descripcion_Falla); // Verifica la descripción de la segunda falla
        }

        [TestMethod]
        public void Agregar_Fallas()
        {

        }
        [TestMethod]

        public void Guardar_Falla()
        {

        }
        [TestMethod]

        public void Eliminar_Falla()
        {

        }
    }
}
