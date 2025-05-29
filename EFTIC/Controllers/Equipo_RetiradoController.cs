using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    public class Equipo_RetiradoController : Controller
    {

        private Equipo_Retirado objequipo = new Equipo_Retirado();

        public ActionResult Index(string criterio)
        {


            if (criterio == null || criterio == "")
            {
                return View(objequipo.Listar());
                {

                };
            }
            else
            {
                return View(objequipo.Buscar(criterio));
            }
        }

        //Ver_Equipo_retirado
        public ActionResult Ver(int id)
        {
            return View(objequipo.Obtener(id));

        }

        //Buscar_Equipo_retirado
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objequipo.Listar() : objequipo.Buscar(criterio));

        }

        //Editar_Equipo_retirado
        public ActionResult AgregarEditar(int id = 0)
        {


            return View(
                id == 0 ? new Equipo_Retirado()
                : objequipo.Obtener(id));

        }


        //Guardamos_Equipo_retirado
        public ActionResult Guardar(Equipo_Retirado objequipo)
        {
            if (ModelState.IsValid)
            {
                objequipo.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Equipo_Retirado/Index");

            }
            else
            {


                return View("~/Views/Equipo_Retirado/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Equipo
        public ActionResult Eliminar(int id)
        {
            objequipo.Equipo_RetiradosID = id;
            objequipo.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Equipo_Retirado/Index");
        }

        //RF - 011 - REQUERIEMTO FUNCIONAL - EQUIPOS RETIRADOS
    }
}