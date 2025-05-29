using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    
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

        //Editar_Asignar
        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.Tipo = new Area().Listar();
            ViewBag.Tipo2 = new Usuario().Listar();
            ViewBag.Tipo3 = new Inventario().Listar();
            return View(id == 0 ? new Asignar() : objasignar.Obtener(id));
        }

        //Guardamos_Asignar
        public ActionResult Guardar(Asignar objasignar)
        {
            if (ModelState.IsValid)
            {
                objasignar.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Asignar/Index");

            }
            else
            {
                return View("~/Views/Asignar/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Asignar
        public ActionResult Eliminar(int id)
        {
            objasignar.AsignarID = id;
            objasignar.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Asignar/Index");
        }

    }
}