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
    public class OtrosTest
    {
        [TestMethod]
        public void Listar_Otros()
        {
            var dataActividades = new List<O_Actividades>
            {
                new O_Actividades { O_ActividadesID = 1, Descripcion_O_Actividad = "Actividad 1" },
                new O_Actividades { O_ActividadesID = 2, Descripcion_O_Actividad = "Actividad 2" }
            }.AsQueryable(); // Convertir la lista en un IQueryable

            var mockSetActividades = new Mock<DbSet<O_Actividades>>();

            mockSetActividades.As<IQueryable<O_Actividades>>()
                .Setup(m => m.Provider).Returns(dataActividades.Provider);
            mockSetActividades.As<IQueryable<O_Actividades>>()
                .Setup(m => m.Expression).Returns(dataActividades.Expression);
            mockSetActividades.As<IQueryable<O_Actividades>>()
                .Setup(m => m.ElementType).Returns(dataActividades.ElementType);
            mockSetActividades.As<IQueryable<O_Actividades>>()
                .Setup(m => m.GetEnumerator()).Returns(dataActividades.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.O_Actividades).Returns(mockSetActividades.Object);

            var oActividadesClass = new O_Actividades(); // Instancia de la clase que contiene el método Listar

            var resultado = oActividadesClass.Listar(mockContext.Object); // Pasar el contexto simulado

            
        }
        [TestMethod]
        public void Agregar_Otros()
        {

        }
        [TestMethod]

        public void Guardar_Otros()
        {

        }
        [TestMethod]

        public void Eliminar_Otros()
        {

        }
    }
}
