using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]
    public class SedeController : Controller
    {
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: SedeController.cs
         * PROPÓSITO: Administración de sedes institucionales registradas en el sistema EFTIC.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador gestiona el mantenimiento de las sedes disponibles dentro del sistema,
         * permitiendo registrar, visualizar, buscar, actualizar y eliminar las ubicaciones físicas
         * donde se prestan servicios informáticos.
         * 
         * FUNCIONALIDADES:
         * - Listado completo de sedes.
         * - Búsqueda por criterio textual.
         * - Visualización detallada de una sede.
         * - Registro y edición de datos de sede.
         * - Eliminación de sedes con confirmación.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Validación de modelo antes del guardado.
         * - Navegación amigable mediante URLs limpias.
         * - Retroalimentación al usuario mediante `TempData`.
         * - Control de acceso mediante atributo `[Authorize]`.
         * 
         * NOTAS:
         * - Las sedes registradas son utilizadas como datos maestros para los módulos de informes,
         *   inventarios y gestión de soporte.
         * - Se recomienda proteger el módulo ante eliminación de sedes en uso (validación futura).
         * 
         * REQUERIMIENTO FUNCIONAL ASOCIADO:
         * - RF-004 - Gestión de sedes del sistema.
         * 
         * ------------------------------------------------------------
         */


        private Sede objsede = new Sede();

        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objsede.Listar());
                {

                };
            }
            else
            {
                return View(objsede.Buscar(criterio));
            }
        }

        //Ver_Sede
        public ActionResult Ver(int id)
        {
            return View(objsede.Obtener(id));

        }

        //Buscar_Sede
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objsede.Listar() : objsede.Buscar(criterio));

        }

        //Editar_Sede
        public ActionResult AgregarEditar(int id = 0)
        {
            return View(
                id == 0 ? new Sede()
                : objsede.Obtener(id));

        }


        //Guardamos_Sede
        public ActionResult Guardar(Sede objsede)
        {
            if (ModelState.IsValid)
            {
                objsede.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; //Alerta de guardado

                return Redirect("~/Sede/Index");

            }
            else
            {
                return View("~/Views/Sede/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Sede
        public ActionResult Eliminar(int id)
        {
            objsede.SedeID = id;
            objsede.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Sede/Index");
        }


        //RF - 004 - REQURIMIENTO FUNCIONAL - SEDES 
    }
}