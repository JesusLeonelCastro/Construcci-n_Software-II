namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Area")]
    public partial class Area
    {
        /*
         * ------------------------------------------------------------
         * MODELO: Area.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/09/2024 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Representa la entidad "Área" dentro del sistema de gestión 
         * de inventario y usuarios EFTIC. Cada área pertenece a una sede 
         * y puede estar asociada a múltiples objetos como inventario, usuarios, 
         * informes y asignaciones.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Listar todas las áreas incluyendo su relación con la sede.
         * - Buscar áreas por nombre.
         * - Obtener una área específica por ID.
         * - Registrar o actualizar datos de un área.
         * - Eliminar un área del sistema.
         * 
         * RELACIONES:
         * - FK con la entidad "Sede"
         * - 1:N con Inventario, Usuario, Informes y Asignar
         * 
         * BUENAS PRÁCTICAS UTILIZADAS:
         * - Manejo controlado de errores con try-catch
         * - Inclusión explícita de relaciones (Include / Include("Sede"))
         * - Uso de Entity Framework para el control de estados (Added/Modified/Deleted)
         * - Patrón de entidad parcial (partial class) para permitir extensiones
         * 
         * REQUERIMIENTO FUNCIONAL RELACIONADO:
         * - RF-002 - Gestión de Áreas en el sistema
         * 
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Area()
        {
            Asignar = new HashSet<Asignar>();
            Informes = new HashSet<Informes>();
            Inventario = new HashSet<Inventario>();
            Usuario = new HashSet<Usuario>();
        }

        public int AreaID { get; set; }

        [StringLength(100)]
        public string Nombre_Area { get; set; }

        [StringLength(255)]
        public string Descripcion_Area { get; set; }

        public int? SedeID { get; set; }

        public virtual Sede Sede { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignar> Asignar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventario> Inventario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

       
        //Listar_Area
        public List<Area> Listar(Model1 context = null)
        {
            var ObjArea = new List<Area>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjArea = db.Area
                                   .Include(a => a.Sede)  // Incluir relaci�n con Sede
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjArea;
        }

        //Buscar_Area 
        public List<Area> Buscar(string criterio)
        {
            var ObjArea = new List<Area>();
            try
            {
                using (var db = new Model1())
                {
                    ObjArea = db.Area.
                        Where(x => x.Nombre_Area.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjArea;

        }

        // Obtener_Area
        public Area Obtener(int id)
        {

            var ObjArea = new Area();
            try
            {
                using (var db = new Model1())
                {
                    ObjArea = db.Area
                               .Include("Sede")
                               .Where(x => x.AreaID == id)
                               .SingleOrDefault();
                }



            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjArea;

        }

        //Agregar_Area 
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.AreaID > 0)
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

        //Eliminar_Area 
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
