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
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: EstadosController.cs
         * PROPÓSITO: Gestiona las operaciones CRUD de los estados del sistema.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador permite administrar los estados registrados en el sistema 
         * EFTIC, los cuales pueden estar relacionados con equipos, procesos u otros elementos.
         * 
         * FUNCIONALIDADES:
         * - Listar todos los estados existentes.
         * - Buscar estados según un criterio ingresado por el usuario.
         * - Ver el detalle completo de un estado específico.
         * - Agregar nuevos estados o editar los existentes.
         * - Eliminar un registro de estado.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Control de acceso mediante el atributo [Authorize].
         * - Uso de TempData para mostrar notificaciones de guardado y eliminado.
         * - Validación del modelo antes de ejecutar acciones persistentes.
         * - Código limpio, estructurado y fácil de mantener.
         * - Reutilización de vistas para agregar y editar, evitando duplicaciones.
         * 
         * NOTA:
         * - El modelo `Estados` debe implementar los métodos Listar(), Buscar(), Obtener(), 
         *   Guardar() y Eliminar().
         * - En proyectos más complejos se recomienda separar la lógica en capas (servicios, repositorio).
         * ------------------------------------------------------------
         */

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