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
    public class TipoEquipoTest
    {
        [TestMethod]
        public void Listar_TipoEquipo()
        {
            var dataTipoEquipo = new List<Tipo_Equipo>
            {
                new Tipo_Equipo { Tipo_EquipoID = 1, Nombre_Tipo_Equipo = "Tipo 1" },
                new Tipo_Equipo { Tipo_EquipoID = 2, Nombre_Tipo_Equipo = "Tipo 2" }
            }.AsQueryable();

            var mockSetTipoEquipo = new Mock<DbSet<Tipo_Equipo>>();
            mockSetTipoEquipo.As<IQueryable<Tipo_Equipo>>()
                .Setup(m => m.Provider).Returns(dataTipoEquipo.Provider);
            mockSetTipoEquipo.As<IQueryable<Tipo_Equipo>>()
                .Setup(m => m.Expression).Returns(dataTipoEquipo.Expression);
            mockSetTipoEquipo.As<IQueryable<Tipo_Equipo>>()
                .Setup(m => m.ElementType).Returns(dataTipoEquipo.ElementType);
            mockSetTipoEquipo.As<IQueryable<Tipo_Equipo>>()
                .Setup(m => m.GetEnumerator()).Returns(dataTipoEquipo.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Tipo_Equipo).Returns(mockSetTipoEquipo.Object);

            var tipoEquipoClass = new Tipo_Equipo();

            var resultado = tipoEquipoClass.Listar(mockContext.Object);

            Assert.IsNotNull(resultado); // Verifica que no sea null
            Assert.AreEqual(2, resultado.Count); // Verifica que haya 2 tipos de equipo
            Assert.AreEqual("Tipo 1", resultado[0].Nombre_Tipo_Equipo); // Verifica el nombre del primer tipo de equipo
            Assert.AreEqual("Tipo 2", resultado[1].Nombre_Tipo_Equipo); // Verifica el nombre del segundo tipo de equipo

        }
    }
}
