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
    public class AreaTest
    {
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

    }
}
