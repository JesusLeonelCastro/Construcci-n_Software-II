using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]
    public class Tipo_EquipoController : Controller
    {
        private Tipo_Equipo objtipo_equipso = new Tipo_Equipo();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objtipo_equipso.Listar());
                {

                };
            }
            else
            {
                return View(objtipo_equipso.Buscar(criterio));
            }
        }

        //Ver_TipoEquipo
        public ActionResult Ver(int id)
        {
            return View(objtipo_equipso.Obtener(id));

        }

        //Buscar_TipoEquipo
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objtipo_equipso.Listar() : objtipo_equipso.Buscar(criterio));

        }

        //Editar_TipoEquipo
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Tipo_Equipo()
                : objtipo_equipso.Obtener(id));

        }


        //Guardamos_TipoEquipo
        public ActionResult Guardar(Tipo_Equipo objtipo_equipso)
        {
            if (ModelState.IsValid)
            {
                objtipo_equipso.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Tipo_Equipo/Index");

            }
            else
            {
                return View("~/Views/Tipo_Equipo/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_TipoEquipo
        public ActionResult Eliminar(int id)
        {
            objtipo_equipso.Tipo_EquipoID = id;
            objtipo_equipso.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Tipo_Equipo/Index");
        }
    }
}