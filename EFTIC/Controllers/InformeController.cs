using EFTIC.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Rectangle = iTextSharp.text.Rectangle;
using Font = iTextSharp.text.Font;

namespace EFTIC.Controllers
{
    [Authorize]
    public class InformeController : Controller
    {
        private Informes objinformes = new Informes();
        
        //private Usuario objusuario = new Usuario();


        private Falla objfalla = new Falla();

        private O_Actividades objo_Actividades = new O_Actividades();

        private Sede objsede = new Sede();

        private Tipo_Equipo objtipo_equipso = new Tipo_Equipo();

        private Estados objestado = new Estados();

        private Equipo_Retirado objequipo = new Equipo_Retirado();

        private Area objarea = new Area();



        public ActionResult Index(string criterio)
        {
            var area = objarea.Listar();
            ViewBag.area = area;

            var falla = objfalla.Listar();
            ViewBag.falla = falla;

            var o_Actividades = objo_Actividades.Listar();
            ViewBag.o_Actividades = o_Actividades;

            var sede = objsede.Listar();
            ViewBag.sede = sede;

            var tipo_equipo = objtipo_equipso.Listar();
            ViewBag.tipo_equipo = tipo_equipo;

            var estado = objestado.Listar();
            ViewBag.estado = estado;

            var equipo = objequipo.Listar();
            ViewBag.equipo = equipo;

            //var usuario = objusuario.Listar();
            //ViewBag.usuario = usuario;


            if (criterio == null || criterio == "")
            {
                return View(objinformes.Listar());


            }
            else
            {
                return View(objinformes.Buscar(criterio));
            }


        }

        //Ver_Informe 
        public ActionResult Ver(int id)
        {
            return View(objinformes.Obtener(id));
        }

        //Buscar_Articulo
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objinformes.Listar() : objinformes.Buscar(criterio));

        }

        //Editar_Informe 
        public ActionResult AgregarEditar(int id = 0)
        {

            // Cargar listas para los dropdowns en la vista
            ViewBag.Tipo = new Area().Listar();
            ViewBag.Tipo4 = new Falla().Listar();
            ViewBag.Tipo2 = new Sede().Listar();
            ViewBag.Tipo3 = new Tipo_Equipo().Listar();
            ViewBag.Tipo6 = new O_Actividades().Listar();
            ViewBag.Tipo7 = new Estados().Listar();
            ViewBag.Tipo5 = new Equipo_Retirado().Listar();
            //ViewBag.Tipo10 = new Usuario().Listar();


            // Crear una instancia del modelo
            Informes model;

            if (id == 0)
            {
                // Si es un nuevo informe, asigna el valor predeterminado al título
                model = new Informes
                {
                    Titulo = "FORMATO DE INFORME TECNICO DE SOPORTE INFORMATICO 2024"

                };
            }
            else
            {
                // Si se está editando un informe existente, carga el informe de la base de datos
                model = new Informes().Obtener(id);
            }

            return View(model);

        }

        //Guardar_Informe 
        public ActionResult Guardar(Informes objinformes)
        {
            if (ModelState.IsValid)
            {
                objinformes.Guardar();
                TempData["AlertarGuardar"] = "El registro se guardó correctamente"; // Alerta de guardado
                return Redirect("~/Informe/Index");
            }
            else
            {
                ViewBag.Tipo = new Area().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new Falla().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new Sede().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new Tipo_Equipo().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new O_Actividades().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new Estados().Listar(); // Asegúrate de que este método devuelva todas las categorías
                ViewBag.Tipo = new Equipo_Retirado().Listar(); // Asegúrate de que este método devuelva todas las categorías

                return View("~/Views/Informe/AgregarEditar.cshtml", objinformes);
            }
        }

        //Eliminamos_Informe 
        public ActionResult Eliminar(int id)
        {
            objinformes.InformeID = id;
            objinformes.Eliminar();
            TempData["AlertarEliminar"] = "El registro se elimino correctamente"; //Alerta de eliminado

            return Redirect("~/Informe/Index");
        }




        //Generar_Informe_Tecnico PDF _ BIEN
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
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("INFORME TÉCNICO DE SOPORTE INFORMÁTICO\n", titleFont)
            {
                Alignment = iTextSharp.text.Element.ALIGN_CENTER
            };
            document.Add(title);

            iTextSharp.text.Paragraph subtitle = new iTextSharp.text.Paragraph($"N° 0{informeID} - 2024\n\n", titleFont)
            {
                Alignment = iTextSharp.text.Element.ALIGN_CENTER
            };
            document.Add(subtitle);


            // Tabla de Datos del Usuario
            PdfPTable userTable = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(userTable, "1.- DATOS DEL USUARIO", "", sectionFont, lightGray);
            AddCellToTable(userTable, "Fecha Solicitud", informe.Fecha_Solicitud?.ToString("dd-MM-yyyy") ?? "N/A", normalFont);
            AddCellToTable(userTable, "Gerencia / Sub Gerencia / Oficina / Unidad", informe.Area.Nombre_Area, normalFont);
            AddCellToTable(userTable, "Sede", informe.Sede.Nombre_Sede, normalFont);
            AddCellToTable(userTable, "Fecha del Informe", informe.Fecha_Informe?.ToString("dd-MM-yyyy") ?? "N/A", normalFont);
            document.Add(userTable);

            // Espacio entre tablas
            document.Add(new iTextSharp.text.Paragraph("\n"));

            // Tabla de Solicitud de Soporte Informático
            PdfPTable soporteTable = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(soporteTable, "2.- SOLICITUD DE SOPORTE INFORMÁTICO", "", sectionFont, lightGray);
            AddCellToTable(soporteTable, "Falla Reportada", informe.Falla.Nombre_Falla, normalFont);
            AddCellToTable(soporteTable, "Equipo Defectuoso", informe.Nombre_Equipos, normalFont);
            document.Add(soporteTable);

            // Espacio entre tablas
            document.Add(new iTextSharp.text.Paragraph("\n"));

            // Tabla de Otras Actividades
            PdfPTable actividadesTable = new PdfPTable(2) { WidthPercentage = 100 };
            AddSubtitleCellToTable(actividadesTable, "3.- OTRAS ACTIVIDADES", "", sectionFont, lightGray);
            AddCellToTable(actividadesTable, "Diagnóstico", informe.Diagnostico, normalFont);
            document.Add(actividadesTable);

            // Espacio entre tablas
            document.Add(new iTextSharp.text.Paragraph("\n"));

            // Tabla de Detalle Técnico del Hardware y Software

            PdfPTable detalleTable2 = new PdfPTable(2) { WidthPercentage = 100 };
            PdfPTable detalleTable = new PdfPTable(4) { WidthPercentage = 100 };
            detalleTable.SetWidths(new float[] { 25f, 25f, 25f, 25f }); // Anchos proporcionales para cada columna
            AddSubtitleCellToTable(detalleTable2, "4.- DETALLE TÉCNICO DEL HARDWARE Y SOFTWARE", "", sectionFont, lightGray);

            AddCellToTable(detalleTable, "Tipo", "HARDWARE", normalFont);
            AddCellToTable(detalleTable, "Sub Tipo", informe.Tipo_Equipo.Nombre_Tipo_Equipo, normalFont);
            AddCellToTable(detalleTable, "Color", informe.Color, normalFont);
            AddCellToTable(detalleTable, "Modelo", informe.Modelo, normalFont);
            AddCellToTable(detalleTable, "Serie", informe.Serie, normalFont);
            AddCellToTable(detalleTable, "Marca", informe.Marca, normalFont);
            AddCellToTable(detalleTable, "Cód. Patrimonial", informe.Cod_Patrimonial, normalFont);
            AddCellToTable(detalleTable, "Código Interno", informe.Codigo_Interno, normalFont);
            document.Add(detalleTable2);
            document.Add(detalleTable);

            // -------------------------------------------

            // Observaciones
            document.Add(new iTextSharp.text.Paragraph("\nOBSERVACIONES", sectionFont));
            iTextSharp.text.Paragraph observaciones = new iTextSharp.text.Paragraph(informe.Observaciones, normalFont);
            document.Add(observaciones);

            // Informe Técnico
            document.Add(new iTextSharp.text.Paragraph("\nDIAGNOSTICO", sectionFont));
            iTextSharp.text.Paragraph informeTecnico = new iTextSharp.text.Paragraph(informe.Diagnostico, normalFont);
            document.Add(informeTecnico);

            // Solución Primaria
            document.Add(new iTextSharp.text.Paragraph("\nSOLUCIÓN PRIMARIA", sectionFont));
            iTextSharp.text.Paragraph solucionPrimaria = new iTextSharp.text.Paragraph(informe.Solucion_Primaria, normalFont);
            document.Add(solucionPrimaria);

            document.Add(new iTextSharp.text.Paragraph("\n", normalFont));
            PdfPTable finalTable = new PdfPTable(3) { WidthPercentage = 100 };

            PdfPCell cell1 = new PdfPCell(new Phrase("USUARIO", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP,
                Padding = 10
            };

            PdfPCell cell2 = new PdfPCell(new Phrase("SOPORTE", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP,
                Padding = 10
            };

            PdfPCell cell3 = new PdfPCell(new Phrase("RESPONSABLE DE EFTIC", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP,
                Padding = 10
            };

            // Agregar celdas a la primera fila
            finalTable.AddCell(cell1);
            finalTable.AddCell(cell2);
            finalTable.AddCell(cell3);

            // Segunda fila
            PdfPCell blankCell1 = new PdfPCell(new Phrase("\n", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP, // Alineación vertical superior
                Padding = 20,
            };

            PdfPCell blankCell2 = new PdfPCell(new Phrase("\n", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP, // Alineación vertical superior
                Padding = 20,
            };

            PdfPCell blankCell3 = new PdfPCell(new Phrase("\n", normalFont))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER,
                VerticalAlignment = PdfPCell.ALIGN_TOP, // Alineación vertical superior
                Padding = 20,
            };

            finalTable.AddCell(blankCell1);
            finalTable.AddCell(blankCell2);
            finalTable.AddCell(blankCell3);

            // Agregar tabla final al documento
            document.Add(finalTable);

            // Cerrar el documento
            document.Close();

            // Devolver el archivo PDF como respuesta
            byte[] pdfBytes = memoryStream.ToArray();
            return File(pdfBytes, "application/pdf", informe.Titulo + " - " + informeID + ".pdf");
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

        private Informes ObtenerInformePorID(int informeID)
        {

            return db.Informes.Find(informeID);
        }
    }
}