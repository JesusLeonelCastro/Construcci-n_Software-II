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
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: Equipo_RetiradoController.cs
         * PROPÓSITO: Gestiona las operaciones CRUD de equipos retirados.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador forma parte del sistema EFTIC y permite realizar 
         * operaciones de registro, visualización, edición, búsqueda y eliminación 
         * de los equipos que han sido retirados del inventario principal.
         * 
         * FUNCIONALIDADES:
         * - Listar todos los equipos retirados.
         * - Buscar equipos retirados según un criterio.
         * - Ver detalle de un equipo retirado específico.
         * - Agregar o editar información de equipos retirados.
         * - Eliminar un registro de equipo retirado.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Uso del patrón MVC para separar lógica, vista y modelo.
         * - Validación del modelo antes de guardar datos en la base.
         * - Utilización de TempData para mostrar notificaciones al usuario.
         * - Código limpio y estructurado, fácil de mantener.
         * - Evita duplicación mediante reutilización de vistas y lógica.
         * 
         * NOTA:
         * - Se recomienda en proyectos más grandes aplicar principios SOLID y 
         *   separar lógica de negocio en servicios o capas adicionales.
         * ------------------------------------------------------------
         */


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