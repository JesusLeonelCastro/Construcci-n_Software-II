using EFTIC.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
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


        private Model1 db = new Model1();

        public ActionResult GenerarPDF(int informeID)
        {
            // Obtener los datos del informe según el ID
            var informe = ObtenerInformePorID(informeID);

            if (informe == null)
            {
                return HttpNotFound();
            }

            // Crear documento PDF
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 25, 25);
            MemoryStream memoryStream = new MemoryStream();
            iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Cargar las imágenes
            iTextSharp.text.Image leftImage = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Assets/Images/logomodificado.png")); // Reemplaza con la ruta a tu imagen
            iTextSharp.text.Image rightImage = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Assets/Images/logotipo_png.png")); // Reemplaza con la ruta a tu imagen

            // Ajustar tamaño de las imágenes si es necesario
            leftImage.ScaleToFit(100, 50); // Ajustar tamaño según sea necesario
            rightImage.ScaleToFit(100, 50); // Ajustar tamaño según sea necesario

            // Crear tabla para el encabezado con dos columnas
            PdfPTable headerTable = new PdfPTable(2) { WidthPercentage = 100 };
            headerTable.SetWidths(new float[] { 1f, 1f });

            // Agregar imágenes a las celdas
            PdfPCell leftCell = new PdfPCell(leftImage)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
            };

            PdfPCell rightCell = new PdfPCell(rightImage)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_RIGHT,
                VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
            };

            headerTable.AddCell(leftCell);
            headerTable.AddCell(rightCell);

            // Agregar la tabla del encabezado al documento
            document.Add(headerTable);

            // Fuentes y estilos
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            Font subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            BaseColor lightGray = new BaseColor(230, 230, 230); // Color de fondo para los subtítulos

            // Título y subtítulo
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("INFORME DE INVENTARIO EQUIPO INFORMÁTICO\n", titleFont)
            {
                Alignment = iTextSharp.text.Element.ALIGN_CENTER
            };
            document.Add(title);

            iTextSharp.text.Paragraph subtitle = new iTextSharp.text.Paragraph($"N° 0{informeID} - 2025\n\n", titleFont)
            {
                Alignment = iTextSharp.text.Element.ALIGN_CENTER
            };
            document.Add(subtitle);


            // Tabla de Datos del Usuario
            PdfPTable userTable = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(userTable, "1.- DATOS DEL EQUIPO NUEVO", "", sectionFont, lightGray);

            AddCellToTable(userTable, "Fecha ingreso", informe.Ingreso?.ToString("dd-MM-yyyy") ?? "N/A", normalFont);
            AddCellToTable(userTable, "Area", informe.Area.Nombre_Area, normalFont);
            AddCellToTable(userTable, "Sede", informe.Sede.Nombre_Sede, normalFont);
            AddCellToTable(userTable, "Tipo equipo", informe.Tipo_Equipo.Nombre_Tipo_Equipo, normalFont);
            AddCellToTable(userTable, "Codigo patrimonial", informe.Cod_Patrimonial, normalFont);
            
            document.Add(userTable);
            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));


            // Tabla de Solicitud de Soporte Informático
            PdfPTable soporteTable2 = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(soporteTable2, "2.- DETALLES", "", sectionFont, lightGray);
            AddCellToTable(soporteTable2, "Color equipo", informe.Color, normalFont);
            AddCellToTable(soporteTable2, "Numero.Serie", informe.Serie, normalFont);
            AddCellToTable(soporteTable2, "Modelo equipo", informe.Modelo, normalFont);
            AddCellToTable(soporteTable2, "Marca equipo", informe.Marca, normalFont);
            AddCellToTable(soporteTable2, "Codigo Interno", informe.Codigo_Interno, normalFont);
            document.Add(soporteTable2);

            // Espacio entre tablas
            document.Add(new iTextSharp.text.Paragraph("\n"));

            iTextSharp.text.Paragraph subtitlee = new iTextSharp.text.Paragraph($"ESPECIFICO DE / CPU / IMPRESORA", titleFont)
            {
                Alignment = iTextSharp.text.Element.ALIGN_CENTER
            };
            document.Add(subtitlee);

            document.Add(new iTextSharp.text.Paragraph("\n"));
            document.Add(new iTextSharp.text.Paragraph("\n"));


            // Tabla de Solicitud de Soporte Informático
            PdfPTable soporteTable = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(soporteTable, "3.- ESPECIFICOS", "", sectionFont, lightGray);
            AddCellToTable(soporteTable, "Tipo equipo", informe.Tipo_Equipo.Nombre_Tipo_Equipo, normalFont);
            AddCellToTable(soporteTable, "Direccion MAC", informe.direccion_MAC, normalFont);
            AddCellToTable(soporteTable, "Capacidad disco", informe.Capacidad_Disco, normalFont);
            AddCellToTable(soporteTable, "Capacidad ram", informe.Capacidad_Ram, normalFont);
            AddCellToTable(soporteTable, "Marca procesador", informe.Marca_Procesador, normalFont);
            AddCellToTable(soporteTable, "Direccion IP", informe.Direccion_IP, normalFont);
            AddCellToTable(soporteTable, "Sistema operativo", informe.Sistema_operativo, normalFont);
            document.Add(soporteTable);

            document.Add(new iTextSharp.text.Paragraph("\n", normalFont));
            

            document.Close();

            byte[] pdfBytes = memoryStream.ToArray();
            return File(pdfBytes, "application/pdf", "FORMATO INFORME INVENTARIO - " + informeID + ".pdf");
        }

        private void AddCellToTable(PdfPTable table, string label, string value, Font font)
        {
            PdfPCell cellLabel = new PdfPCell(new Phrase(label, font))
            {
                Border = Rectangle.BOX, // Ver los bordes
                Padding = 5
            };
            PdfPCell cellValue = new PdfPCell(new Phrase(value, font))
            {
                Border = Rectangle.BOX, // Ver los bordes
                Padding = 5
            };
            table.AddCell(cellLabel);
            table.AddCell(cellValue);
        }

        private void AddSubtitleCellToTable(PdfPTable table, string subtitle, string value, Font font, BaseColor backgroundColor)
        {
            PdfPCell subtitleCell = new PdfPCell(new Phrase(subtitle, font))
            {
                Border = Rectangle.BOX,
                BackgroundColor = backgroundColor,
                Colspan = 2,
                Padding = 5
            };
            table.AddCell(subtitleCell);
        }

        private Inventario ObtenerInformePorID(int inventarioID)
        {

            return db.Inventario.Find(inventarioID);
        }
    }
}