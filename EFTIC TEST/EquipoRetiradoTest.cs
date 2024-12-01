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
    public class EquipoRetiradoTest
    {
        [TestMethod]
        public void Listar_Equipo_Retirado()
        {
            var dataEquipos = new List<Equipo_Retirado>
            {
                new Equipo_Retirado { Equipo_RetiradosID = 1, Nombre_Equipo = "Equipo 1" },
                new Equipo_Retirado { Equipo_RetiradosID = 2, Nombre_Equipo = "Equipo 2" }
            }.AsQueryable(); // Convertir la lista en un IQueryable

            var mockSetEquipo = new Mock<DbSet<Equipo_Retirado>>();

            mockSetEquipo.As<IQueryable<Equipo_Retirado>>()
                .Setup(m => m.Provider).Returns(dataEquipos.Provider);
            mockSetEquipo.As<IQueryable<Equipo_Retirado>>()
                .Setup(m => m.Expression).Returns(dataEquipos.Expression);
            mockSetEquipo.As<IQueryable<Equipo_Retirado>>()
                .Setup(m => m.ElementType).Returns(dataEquipos.ElementType);
            mockSetEquipo.As<IQueryable<Equipo_Retirado>>()
                .Setup(m => m.GetEnumerator()).Returns(dataEquipos.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Equipo_Retirado).Returns(mockSetEquipo.Object);

            var equipoRetiradoClass = new Equipo_Retirado();

            var resultado = equipoRetiradoClass.Listar(mockContext.Object);

            Assert.IsNotNull(resultado); 
            Assert.AreEqual(2, resultado.Count); 
            Assert.AreEqual("Equipo 1", resultado[0].Nombre_Equipo); // Verifica el nombre del primer equipo
            Assert.AreEqual("Equipo 2", resultado[1].Nombre_Equipo); // Verifica el nombre del segundo equipo
        }

    }
}
