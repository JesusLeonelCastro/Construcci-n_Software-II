using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]

    public class OtrasController : Controller
    {
        private O_Actividades objo_Actividades = new O_Actividades();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objo_Actividades.Listar());
                {

                };
            }
            else
            {
                return View(objo_Actividades.Buscar(criterio));
            }
        }

        //Ver_Otras
        public ActionResult Ver(int id)
        {
            return View(objo_Actividades.Obtener(id));

        }

        //Buscar_Otras
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objo_Actividades.Listar() : objo_Actividades.Buscar(criterio));

        }

        //Editar_Otras
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new O_Actividades()
                : objo_Actividades.Obtener(id));

        }


        //Guardamos_Otras
        public ActionResult Guardar(O_Actividades objo_Actividades)
        {
            if (ModelState.IsValid)
            {
                objo_Actividades.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Otras/Index");

            }
            else
            {
                return View("~/Views/Otras/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Otras
        public ActionResult Eliminar(int id)
        {
            objo_Actividades.O_ActividadesID = id;
            objo_Actividades.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Otras/Index");
        }
    }
}