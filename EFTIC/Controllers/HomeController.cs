using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFTIC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        /*
         * ------------------------------------------------------------
         * CONTROLADOR: HomeController.cs
         * PROPÓSITO: Controlador principal para la carga del Dashboard del sistema.
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este controlador carga la vista principal del sistema EFTIC, proporcionando 
         * una vista general de los datos clave del sistema como informes, inventario, 
         * fallas, actividades, áreas, estados, sedes, tipos de equipo, etc.
         * 
         * FUNCIONALIDADES:
         * - Mostrar totales de registros de cada entidad (informes, fallas, inventario, etc.).
         * - Agrupar y mostrar el total de informes por área y por sede.
         * - Calcular el total de inventario por área.
         * - Mostrar estadísticas de informes por tipo de equipo.
         * 
         * BUENAS PRÁCTICAS IMPLEMENTADAS:
         * - Uso de ViewBag para enviar múltiples datos a la vista de manera clara.
         * - Agrupación de datos usando LINQ para generar reportes resumidos.
         * - Separación de responsabilidades: este controlador solo visualiza datos, no realiza lógica CRUD.
         * - Código legible, segmentado y reutilizable.
         * 
         * NOTAS:
         * - Para una mejor eficiencia en proyectos a gran escala, se recomienda usar servicios 
         *   o caché en lugar de múltiples llamadas a `.Listar()`.
         * - Se podría optimizar el acceso a datos implementando patrones como Repositorio o Unit of Work.
         * ------------------------------------------------------------
         */


        private Informes objinformes = new Informes();

        private Falla objfalla = new Falla();

        private O_Actividades objo_Actividades = new O_Actividades();

        private Sede objsede = new Sede();

        private Tipo_Equipo objtipo_equipso = new Tipo_Equipo();

        private Estados objestado = new Estados();

        private Equipo_Retirado objequipo = new Equipo_Retirado();

        private Area objarea = new Area();


        private Inventario objinvetario = new Inventario();




        public ActionResult Index()
        {
            //Total de Regsitros
            int totalInformes = objinformes.Listar().Count();
            int totalAreas = objarea.Listar().Count();
            var totalFallas = objfalla.Listar().Count;
            var totalActividades = objo_Actividades.Listar().Count;
            var totalSedes = objsede.Listar().Count;
            var totalTiposEquipos = objtipo_equipso.Listar().Count;
            var totalEstados = objestado.Listar().Count;
            var totalInventario = objinvetario.Listar().Count;


            ViewBag.TotalFallas = totalFallas;
            ViewBag.TotalActividades = totalActividades;
            ViewBag.TotalSedes = totalSedes;
            ViewBag.TotalTiposEquipos = totalTiposEquipos;
            ViewBag.TotalEstados = totalEstados;
            ViewBag.TotalAreas = totalAreas;
            ViewBag.TotalInformes = totalInformes;
            ViewBag.TotalInventario = totalInventario;




            // Obtener el total de informes por área
            var informesPorArea = objinformes.Listar()
                .GroupBy(i => i.AreaID)
                .Select(g => new { AreaId = g.Key, Total = g.Count() })
                .ToList();

            var areas = objarea.Listar();
            var informesPorAreaConNombre = informesPorArea
                .Select(i => new { NombreArea = areas.FirstOrDefault(a => a.AreaID == i.AreaId)?.Nombre_Area, i.Total })
                .ToList();

            ViewBag.InformesPorArea = informesPorAreaConNombre;



            // Obtener el total de informes por sede
            var informesPorSede = objinformes.Listar()
                .GroupBy(i => i.SedeID)
                .Select(g => new { SedeId = g.Key, Total = g.Count() })
                .ToList();

            var sedes = objsede.Listar();
            var informesPorSedeConNombre = informesPorSede
                .Select(i => new { NombreSede = sedes.FirstOrDefault(s => s.SedeID == i.SedeId)?.Nombre_Sede, i.Total })
                .ToList();

            ViewBag.InformesPorSede = informesPorSedeConNombre;


            // Obtener el total de informes por cada tipo de equipo
            var totalTipoEquiposs = objtipo_equipso.Listar()
                .GroupBy(te => te.Nombre_Tipo_Equipo) // Suponiendo que 'Nombre' es el campo que identifica el tipo de equipo
                .Select(g => new {
                    Tipo = g.Key,
                    Total = g.Sum(te => objinformes.Listar().Count(i => i.Tipo_EquipoID == te.Tipo_EquipoID))
                }).ToList();

            ViewBag.TotalTipoEquiposs = totalTipoEquiposs;



            // Obtener el total de inventario por área
            var inventarioPorArea = objinvetario.Listar()
                .GroupBy(i => i.AreaID)
                .Select(g => new { AreaId = g.Key, Total = g.Count() })
                .ToList();

            var areas2 = objarea.Listar();
            var inventarioPorAreaConNombre = inventarioPorArea
                .Select(i => new { NombreArea = areas.FirstOrDefault(a => a.AreaID == i.AreaId)?.Nombre_Area, i.Total })
                .ToList();

            ViewBag.inventarioPorArea = inventarioPorAreaConNombre;


            

            return View();
        }
    }
}