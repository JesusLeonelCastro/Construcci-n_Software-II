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
    public class InformeTest
    {
        [TestMethod]
        public void Listar_Informe()
        {
            // Arrange
            var data = new List<Informes>
            {
                new Informes { InformeID = 1, Area = new Area { AreaID = 1, Nombre_Area = "Area 1" }, Falla = new Falla { FallaID = 1, Descripcion_Falla = "Falla 1" }, Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede 1" }, Tipo_Equipo = new Tipo_Equipo { Tipo_EquipoID = 1, Nombre_Tipo_Equipo = "Equipo 1" } },
                new Informes { InformeID = 2, Area = new Area { AreaID = 2, Nombre_Area = "Area 2" }, Falla = new Falla { FallaID = 2, Descripcion_Falla = "Falla 2" }, Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede 2" }, Tipo_Equipo = new Tipo_Equipo { Tipo_EquipoID = 2, Nombre_Tipo_Equipo = "Equipo 2" } }
            };

            var mockSet = new Mock<DbSet<Informes>>();
            mockSet.As<IQueryable<Informes>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<Informes>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<Informes>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<Informes>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Informes).Returns(mockSet.Object);

            //var service = new InformesService(mockContext.Object);

            //// Act
            //var result = service.Listar();

            //// Assert
            //Assert.NotNull(result);
            //Assert.Equal(2, result.Count); // Verifica que se hayan devuelto 2 informes
            //Assert.Equal("Area 1", result[0].Area.Nombre_Area);
            //Assert.Equal("Falla 1", result[0].Falla.Descripcion);
            //Assert.Equal("Sede 1", result[0].Sede.Nombre_Sede);
            //Assert.Equal("Equipo 1", result[0].Tipo_Equipo.Nombre_Tipo_Equipo);
        }


        [TestMethod]
        public void Agregar_Informe()
        {

        }
        [TestMethod]

        public void Guardar_Informea()
        {

        }
        [TestMethod]

        public void Eliminar_Informe()
        {

        }
        [TestMethod]

        public void Actulizar_Infomee()
        {

        }
    }
}
