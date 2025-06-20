using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: UsuarioController.cs
         * PROPÓSITO: Administración de usuarios del sistema EFTIC.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador gestiona el CRUD de usuarios que utilizan o administran
         * el sistema EFTIC. Incluye la integración con el servicio de RENIEC para 
         * autocompletar datos a partir del DNI ingresado.
         * 
         * FUNCIONALIDADES:
         * - Listado, búsqueda y filtrado de usuarios.
         * - Registro y edición con integración RENIEC (asíncrona).
         * - Eliminación de usuarios.
         * - Visualización detallada de un usuario.
         * - Carga dinámica de áreas según sede (vía AJAX/JSON).
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Validación con `ModelState.IsValid`.
         * - Separación lógica de capas (servicio RENIEC externo).
         * - Control de acceso con `[Authorize]`.
         * - Comunicación cliente-servidor mediante `JsonResult`.
         * - Inyección del cliente HTTP y uso de `async/await`.
         * 
         * NOTAS:
         * - El servicio RENIEC se consulta solo si se proporciona el DNI.
         * - Las áreas se filtran dinámicamente por sede en formularios.
         * - El campo `DNI` es clave para la identificación única.
         * 
         * REQUERIMIENTO FUNCIONAL ASOCIADO:
         * - RF-001 - Administración de Usuarios
         * 
         * ------------------------------------------------------------
         */


        //Instacion de clases
        private Usuario objusuario = new Usuario();
        private readonly Reniec _reniecService;

        //Declaracion de variables
        private Area objarea = new Area();
        private Sede objsede = new Sede();

        

        public UsuarioController()
        {
            var httpClient = new HttpClient();
            _reniecService = new Reniec(httpClient);
        }

        //Index principal
        public ActionResult Index(string criterio)
        {

            var sede = objsede.Listar();
            ViewBag.sede = sede;

            var area = objarea.Listar();
            ViewBag.area = area;

            if (string.IsNullOrEmpty(criterio))
            {
                return View(objusuario.Listar());
            }
            else
            {
                return View(objusuario.Buscar(criterio));
            }
        }

        public ActionResult Ver(int id)
        {
            return View(objusuario.Obtener(id));
        }

        //Buscar Usuario
        public ActionResult Buscar(string criterio)
        {
            return View(string.IsNullOrEmpty(criterio) ? objusuario.Listar() : objusuario.Buscar(criterio));
        }

        //Agregar o editar Usuario
        public async Task<ActionResult> AgregarEditar(int id = 0, string dni = null)
        {
            ViewBag.Tipo = new Sede().Listar();
            ViewBag.Tipo2 = new Area().Listar();

            var model = id == 0 ? new Usuario() : objusuario.Obtener(id);

            if (!string.IsNullOrEmpty(dni))
            {
                var (nombre, apellido) = await _reniecService.ObtenerDatosPorDni(dni);

                if (nombre != null && apellido != null)
                {
                    model.DNI = dni;
                    model.Nombre_Usuario = nombre;
                    model.Apellido_Usuario = apellido;
                }
                //Validacion si no lo encuentra
                else
                {
                    ViewBag.Error = "No se encontraron datos para el DNI proporcionado.";
                }
            }

            return View(model);
        }

        //Guardar Usuario
        public ActionResult Guardar(Usuario objusuario)
        {
            if (ModelState.IsValid)
            {
                objusuario.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; // Alerta de guardado
                return Redirect("~/Usuario/Index");
            }
            else
            {
                return View("~/Views/Usuario/AgregarEditar.cshtml");
            }
        }

        //Eliminar Usuario
        public ActionResult Eliminar(int id)
        {
            objusuario.UsuarioID = id;
            objusuario.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; // Alerta de eliminado
            return Redirect("~/Usuario/Index");
        }



        // Acción para obtener áreas filtradas por sede
        [HttpGet]
        public JsonResult GetAreasBySede(int sedeId)
        {
            // Filtra las áreas por sedeId
            var areas = objarea.Listar().Where(a => a.SedeID == sedeId).Select(a => new
            {
                AreaID = a.AreaID,
                Nombre_Area = a.Nombre_Area
            }).ToList();

            return Json(areas, JsonRequestBehavior.AllowGet);
        }



        //RF - 001 - REQUERIEMINTO FUNCIONAL - "USUARIOS"
    }
}