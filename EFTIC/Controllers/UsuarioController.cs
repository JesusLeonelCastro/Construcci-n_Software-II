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
        private Usuario objusuario = new Usuario();
        private readonly Reniec _reniecService;

        private Area objarea = new Area();
        private Sede objsede = new Sede();

        

        public UsuarioController()
        {
            var httpClient = new HttpClient();
            _reniecService = new Reniec(httpClient);
        }

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

        public ActionResult Buscar(string criterio)
        {
            return View(string.IsNullOrEmpty(criterio) ? objusuario.Listar() : objusuario.Buscar(criterio));
        }

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
                else
                {
                    ViewBag.Error = "No se encontraron datos para el DNI proporcionado.";
                }
            }

            return View(model);
        }

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
    }
}