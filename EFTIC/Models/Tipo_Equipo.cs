namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Tipo_Equipo
    {
        /*
         * ------------------------------------------------------------
         * CLASE: Tipo_Equipo.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 20/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Representa la entidad `Tipo_Equipo`, utilizada para clasificar los equipos registrados 
         * en los informes técnicos y el inventario del sistema EFTIC.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Propiedades de la entidad Tipo_Equipo.
         * - Relaciones uno-a-muchos con `Informes` e `Inventario`.
         * - Métodos CRUD (Listar, Buscar, Obtener, Guardar, Eliminar).
         * 
         * MÉTODOS DESTACADOS:
         * - `Listar()`: Retorna todos los tipos de equipos registrados.
         * - `Buscar(string criterio)`: Busca tipos de equipos por nombre.
         * - `Obtener(int id)`: Obtiene un tipo de equipo específico por su ID.
         * - `Guardar()`: Inserta o actualiza un tipo de equipo.
         * - `Eliminar()`: Elimina un tipo de equipo existente.
         * 
         * NOTAS DE IMPLEMENTACIÓN:
         * - Integra Entity Framework 6 con `DbContext` (`Model1`).
         * - Las propiedades de navegación están inicializadas en el constructor.
         * - Decorado con anotaciones de datos (`[StringLength]`).
         * 
         * REQUERIMIENTO RELACIONADO:
         * - RF-003 - Requerimiento Funcional - Registro de Tipos de Equipos
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Equipo()
        {
            Informes = new HashSet<Informes>();
            Inventario = new HashSet<Inventario>();
        }

        public int Tipo_EquipoID { get; set; }

        [StringLength(100)]
        public string Nombre_Tipo_Equipo { get; set; }

        [StringLength(255)]
        public string Descripcion_Tipo_Equipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventario> Inventario { get; set; }

        //Listar_Tioo_Equipo
        //public List<Tipo_Equipo> Listar()
        //{
        //    var ObjTipoEquipo = new List<Tipo_Equipo>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjTipoEquipo = db.Tipo_Equipo.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjTipoEquipo;
        //}

        public List<Tipo_Equipo> Listar(Model1 context = null)
        {
            var ObjTipoEquipo = new List<Tipo_Equipo>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjTipoEquipo = db.Tipo_Equipo.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjTipoEquipo;
        }


        //Buscar_Tioo_Equipo
        public List<Tipo_Equipo> Buscar(string criterio)
        {
            var ObjTipoEquipo = new List<Tipo_Equipo>();
            try
            {
                using (var db = new Model1())
                {
                    ObjTipoEquipo = db.Tipo_Equipo.
                        Where(x => x.Nombre_Tipo_Equipo.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjTipoEquipo;

        }

        // Obtener_Tioo_Equipo
        public Tipo_Equipo Obtener(int id)
        {

            var ObjTipoEquipo = new Tipo_Equipo();
            try
            {
                using (var db = new Model1())
                {
                    ObjTipoEquipo = db.Tipo_Equipo.
                        Where(x => x.Tipo_EquipoID == id).SingleOrDefault();
                }


            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjTipoEquipo;

        }

        //Agregar_Tioo_Equipo
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.Tipo_EquipoID > 0)
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

        //Eliminar_Tioo_Equipo

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
