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
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: FallaController.cs
         * PROPÓSITO: Gestiona las operaciones CRUD para el registro de fallas.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador es responsable de administrar los registros de fallas 
         * dentro del sistema EFTIC, permitiendo registrar, visualizar, buscar, 
         * modificar y eliminar incidencias técnicas o fallos relacionados al inventario.
         * 
         * FUNCIONALIDADES:
         * - Listar todas las fallas registradas.
         * - Buscar fallas según un criterio de texto.
         * - Visualizar el detalle de una falla específica.
         * - Agregar nuevas fallas o editar registros existentes.
         * - Eliminar una falla del sistema.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Seguridad mediante el atributo [Authorize] para proteger el acceso.
         * - Validación del modelo antes de guardar la información.
         * - Uso de TempData para mostrar mensajes de confirmación al usuario.
         * - Separación clara de responsabilidades en métodos individuales.
         * - Reutilización de vistas (AgregarEditar) para reducir duplicación.
         * 
         * NOTAS:
         * - El modelo `Falla` debe tener implementados los métodos: Listar(), Buscar(), Obtener(), Guardar() y Eliminar().
         * - Para futuras mejoras, se recomienda utilizar una capa de servicios o repositorios 
         *   para separar la lógica de negocio del controlador.
         * ------------------------------------------------------------
         */


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