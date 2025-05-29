using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]
    public class AsignarController : Controller
    {
        private Asignar objasignar = new Asignar();
        private Usuario objusuario = new Usuario();
        private Area objarea = new Area();
        private Inventario objinventario = new Inventario();

        public ActionResult Index(string criterio)
        {
            // Cargar datos para dropdowns/selects
            ViewBag.area = objarea.Listar();
            ViewBag.usuario = objusuario.Listar();
            ViewBag.inventario = objinventario.Listar(); // Cambiado de "sede" a "inventario" para mayor claridad

            // Filtrar por criterio si es necesario
            if (string.IsNullOrEmpty(criterio))
            {
                return View(objasignar.Listar());
            }
            else
            {
                // Ejemplo de filtrado (ajusta según tu lógica)
                var asignacionesFiltradas = objasignar.Listar()
                    .Where(a => a.Area.Nombre_Area.Contains(criterio) ||
                               a.Usuario.Nombre_Usuario.Contains(criterio) ||
                               a.Inventario.Cod_Patrimonial.Contains(criterio))
                    .ToList();

                return View(asignacionesFiltradas);
            }
        }
    }
}