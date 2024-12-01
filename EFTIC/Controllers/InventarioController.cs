using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    public class InventarioController : Controller
    {
        private Inventario objinventario = new Inventario();

        private Area objarea = new Area();
        private Sede objsede = new Sede();
        private Tipo_Equipo objtipo_equipo = new Tipo_Equipo();


        public ActionResult Index(string criterio)
        {

            var area = objarea.Listar();
            ViewBag.area = area;

            var sede = objsede.Listar();
            ViewBag.sede = sede;

            var tipoequipo = objtipo_equipo.Listar();
            ViewBag.Tipo_Equipo = tipoequipo;


            if (criterio == null || criterio == "")
            {
                return View(objinventario.Listar());
                {

                };
            }
            else
            {
                return View(objinventario.Buscar(criterio));
            }
        }

        //Ver_Inventario
        public ActionResult Ver(int id)
        {
            return View(objinventario.Obtener(id));

        }

        //Buscar_Inventario
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objinventario.Listar() : objinventario.Buscar(criterio));

        }

        //Editar_Inventario
        public ActionResult AgregarEditar(int id = 0)
        {
            ViewBag.Tipo = new Sede().Listar();
            ViewBag.Tipo2 = new Area().Listar();
            ViewBag.Tipo3 = new Tipo_Equipo().Listar();


            return View(
                id == 0 ? new Inventario()
                : objinventario.Obtener(id));

        }


        //Guardamos_Inventario
        public ActionResult Guardar(Inventario objinventario)
        {
            // Validar si los campos están vacíos y asignar '---'
            if (string.IsNullOrEmpty(objinventario.direccion_MAC)) objinventario.direccion_MAC = "---";
            if (string.IsNullOrEmpty(objinventario.Capacidad_Disco)) objinventario.Capacidad_Disco = "---";
            if (string.IsNullOrEmpty(objinventario.Capacidad_Ram)) objinventario.Capacidad_Ram = "---";
            if (string.IsNullOrEmpty(objinventario.Marca_Procesador)) objinventario.Marca_Procesador = "---";
            if (string.IsNullOrEmpty(objinventario.Direccion_IP)) objinventario.Direccion_IP = "---";
            if (string.IsNullOrEmpty(objinventario.Sistema_operativo)) objinventario.Sistema_operativo = "---";

            // Verificar si el modelo es válido antes de guardar
            if (ModelState.IsValid)
            {
                objinventario.Guardar();
                TempData["AlertarGuardar"] = "Se registro se agrego correctamente"; // Alerta de guardado

                return Redirect("~/Inventario/Index");
            }
            else
            {
                return View("~/Views/Inventario/AgregarEditar.cshtml");
            }

        }


        //Eliminamos_Inventario
        public ActionResult Eliminar(int id)
        {
            objinventario.InventarioID = id;
            objinventario.Eliminar();
            TempData["AlertarEliminar"] = "El registro se Elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Inventario/Index");
        }
    }
}