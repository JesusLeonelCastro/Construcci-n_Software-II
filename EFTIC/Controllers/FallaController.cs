using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]

    public class FallaController : Controller
    {
        private Falla objfalla = new Falla();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objfalla.Listar());
                {

                };
            }
            else
            {
                return View(objfalla.Buscar(criterio));
            }
        }

        //Ver_Falla
        public ActionResult Ver(int id)
        {
            return View(objfalla.Obtener(id));

        }

        //Buscar_Falla
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objfalla.Listar() : objfalla.Buscar(criterio));

        }

        //Editar_Falla
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Falla()
                : objfalla.Obtener(id));

        }


        //Guardamos_Falla
        public ActionResult Guardar(Falla objfalla)
        {
            if (ModelState.IsValid)
            {
                objfalla.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Falla/Index");

            }
            else
            {
                return View("~/Views/Falla/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Falla
        public ActionResult Eliminar(int id)
        {
            objfalla.FallaID = id;
            objfalla.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Falla/Index");
        }
    }
}