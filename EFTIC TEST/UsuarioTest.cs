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
    public class UsuarioTest
    {
        [TestMethod]
        public void Listar_Usuarios()
        {
            var dataUsuarios = new List<Usuario>
            {
                new Usuario
                {
                    UsuarioID = 1,
                    Nombre_Usuario = "Usuario 1",
                    Sede = new Sede { SedeID = 1, Nombre_Sede = "Sede 1" },
                    Area = new Area { AreaID = 1, Nombre_Area = "Area 1" }
                },
                new Usuario
                {
                    UsuarioID = 2,
                    Nombre_Usuario = "Usuario 2",
                    Sede = new Sede { SedeID = 2, Nombre_Sede = "Sede 2" },
                    Area = new Area { AreaID = 2, Nombre_Area = "Area 2" }
                }
            }.AsQueryable();

            var mockSetUsuario = new Mock<DbSet<Usuario>>();
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.Provider).Returns(dataUsuarios.Provider);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.Expression).Returns(dataUsuarios.Expression);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.ElementType).Returns(dataUsuarios.ElementType);
            mockSetUsuario.As<IQueryable<Usuario>>()
                .Setup(m => m.GetEnumerator()).Returns(dataUsuarios.GetEnumerator());

            var mockContext = new Mock<Model1>();
            mockContext.Setup(c => c.Usuario).Returns(mockSetUsuario.Object);

            var usuarioClass = new Usuario();

        }
    
    }
}
