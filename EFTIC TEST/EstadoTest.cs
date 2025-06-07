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
    public class EstadoTest
    {
        [TestMethod]
        public void Lista_estads()
        {
                    var dataEstados = new List<Estados>
            {
                new Estados { EstadoID = 1, Nombre_Estado = "Estado 1" },
                new Estados { EstadoID = 2, Nombre_Estado = "Estado 2" }
            }.AsQueryable(); // Convertir la lista a un IQueryable

            var mockSetEstados = new Mock<DbSet<Estados>>();

            mockSetEstados.As<IQueryable<Estados>>()
                .Setup(m => m.Provider).Returns(dataEstados.Provider);
            mockSetEstados.As<IQueryable<Estados>>()
                .Setup(m => m.Expression).Returns(dataEstados.Expression);
            mockSetEstados.As<IQueryable<Estados>>()
                .Setup(m => m.ElementType).Returns(dataEstados.ElementType);
            mockSetEstados.As<IQueryable<Estados>>()
                .Setup(m => m.GetEnumerator()).Returns(dataEstados.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Estados).Returns(mockSetEstados.Object);

            var estadosClass = new Estados();

            var resultado = estadosClass.Listar(mockContext.Object);

            Assert.IsNotNull(resultado); 
            Assert.AreEqual(2, resultado.Count); 
            Assert.AreEqual("Estado 1", resultado[0].Nombre_Estado); 
            Assert.AreEqual("Estado 2", resultado[1].Nombre_Estado); // Verifica el nombre del segundo estado
        }


        [TestMethod]
        public void Agregar_Estado()
        {

        }
        [TestMethod]

        public void Guardar_Estadoo()
        {

        }
        [TestMethod]

        public void Eliminar_Estado()
        {

        }
    }
}
