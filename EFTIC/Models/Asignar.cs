namespace EFTIC.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity; // ¡Este es el using que faltaba!
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Asignar")]
    public partial class Asignar
    {
        /*
         * ------------------------------------------------------------
         * MODELO: Asignar.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Representa la entidad "Asignar" en el sistema EFTIC, utilizada
         * para registrar y gestionar la asignación de equipos informáticos 
         * (inventario) a usuarios dentro de un área determinada.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Listar todas las asignaciones con relaciones a Usuario, Área e Inventario.
         * - Obtener una asignación específica por su ID.
         * - Registrar una nueva asignación o actualizar una existente.
         * - Eliminar una asignación del sistema.
         * 
         * RELACIONES:
         * - FK con Usuario, Área e Inventario (muchas asignaciones pueden compartir relaciones).
         * - Carga las relaciones con `Include` para optimizar las vistas.
         * 
         * BUENAS PRÁCTICAS UTILIZADAS:
         * - Manejo de errores con try-catch
         * - Reutilización de contexto con opción de inyección externa
         * - Patrón Entity Framework para la persistencia de datos
         * - Separación clara de responsabilidades por método
         * 
         * REQUERIMIENTO FUNCIONAL RELACIONADO:
         * - RF-006 - Gestión de Asignaciones de Equipos
         * 
         * NOTA:
         * - Requiere el using de `System.Data.Entity` para poder ejecutar `Include()`
         * 
         * ------------------------------------------------------------
         */


        public int AsignarID { get; set; }

        public int? AreaID { get; set; }

        public int? UsuarioID { get; set; }

        public int? InventarioID { get; set; }


        public virtual Area Area { get; set; }

        public virtual Inventario Inventario { get; set; }

        public virtual Usuario Usuario { get; set; }

        //Listar Asignar
        public List<Asignar> Listar(Model1 context = null)
        {
            var ObjAsignar = new List<Asignar>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjAsignar = db.Asignar
                        .Include(a => a.Area)
                        .Include(a => a.Usuario)
                        .Include(a => a.Inventario)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjAsignar;
        }

        // Obtener_Asignar
        public Asignar Obtener(int id)
        {

            var ObjAsignar = new Asignar();
            try
            {
                using (var db = new Model1())
                {
                    ObjAsignar = db.Asignar
                               .Include("Area")
                               .Include("Usuario")
                               .Include("Inventario")

                               .Where(x => x.AsignarID == id)
                               .SingleOrDefault();
                }



            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjAsignar;

        }

        //Agregar_Asignar
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.AsignarID > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;

            }
        }

        //Eliminar_Asignar
        public void Eliminar()
        {

            try
            {
                using (var db = new Model1())
                {
                    db.Entry(this).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}