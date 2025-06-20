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
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: Tipo_EquipoController.cs
         * PROPÓSITO: Gestión de los tipos de equipos informáticos en el sistema EFTIC.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador administra los tipos de equipos tecnológicos disponibles en la plataforma,
         * tales como CPUs, impresoras, laptops, entre otros. Permite operaciones CRUD básicas.
         * 
         * FUNCIONALIDADES:
         * - Listado de tipos de equipo.
         * - Búsqueda textual por nombre.
         * - Visualización de un tipo específico.
         * - Registro y edición de nuevos tipos de equipo.
         * - Eliminación de registros existentes.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Validación del modelo antes del guardado (`ModelState.IsValid`).
         * - Feedback visual al usuario mediante `TempData`.
         * - Control de acceso asegurado con `[Authorize]`.
         * 
         * NOTAS:
         * - Los tipos de equipo registrados se relacionan directamente con los informes técnicos,
         *   inventario y otras entidades clave del sistema.
         * - El campo `Nombre_Tipo_Equipo` debe mantenerse único y representativo.
         * 
         * REQUERIMIENTO FUNCIONAL ASOCIADO:
         * - RF-005 - Administración de catálogo de tipos de equipos.
         * 
         * ------------------------------------------------------------
         */

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