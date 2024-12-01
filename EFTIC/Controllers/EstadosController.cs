using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]

    public class EstadosController : Controller
    {
        private Estados objestado = new Estados();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objestado.Listar());
                {

                };
            }
            else
            {
                return View(objestado.Buscar(criterio));
            }
        }

        //Ver_Estado
        public ActionResult Ver(int id)
        {
            return View(objestado.Obtener(id));

        }

        //Buscar_Estado
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objestado.Listar() : objestado.Buscar(criterio));

        }

        //Editar_Estado
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Estados()
                : objestado.Obtener(id));

        }


        //Guardamos_Estado
        public ActionResult Guardar(Estados objestado)
        {
            if (ModelState.IsValid)
            {
                objestado.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Estados/Index");

            }
            else
            {
                return View("~/Views/Estados/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Estado
        public ActionResult Eliminar(int id)
        {
            objestado.EstadoID = id;
            objestado.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Estados/Index");
        }
    }
}