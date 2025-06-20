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
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: OtrasController.cs
         * PROPÓSITO: Gestionar actividades adicionales relacionadas al soporte técnico informático.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador permite administrar el catálogo de *otras actividades* realizadas 
         * durante los procesos de soporte técnico que no necesariamente están vinculadas a fallas 
         * directas o incidencias críticas. Las actividades pueden incluir mantenimientos, configuraciones, 
         * capacitaciones u otros apoyos técnicos.
         * 
         * FUNCIONALIDADES:
         * - Listar todas las actividades registradas.
         * - Buscar actividades por criterio de texto.
         * - Ver detalles de una actividad específica.
         * - Agregar o editar actividades (formulario compartido).
         * - Eliminar registros de actividades.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Estructura MVC clara y mantenible.
         * - Validación de modelo antes de guardar datos.
         * - Notificaciones al usuario con `TempData`.
         * - Uso de rutas amigables y limpias para mantener coherencia en la navegación.
         * 
         * NOTAS:
         * - Este catálogo es fundamental para el llenado automático del módulo de informes técnicos.
         * - Puede integrarse un control de uso para verificar qué informes están asociados a una actividad.
         * - Recomendable usar una vista de tipo tabla con opciones de ordenamiento y filtros.
         * 
         * ------------------------------------------------------------
         */

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