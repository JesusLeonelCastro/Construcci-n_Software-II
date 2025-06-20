namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class O_Actividades
    {
        /*
         * ------------------------------------------------------------
         * ENTIDAD: O_Actividades.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 20/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Este archivo define la clase de entidad `O_Actividades`, la cual representa 
         * las otras actividades técnicas o complementarias vinculadas a los informes 
         * técnicos generados en el sistema EFTIC.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - CRUD completo: Listar, Buscar, Obtener, Guardar y Eliminar actividades.
         * - Soporte para relación 1:N con la entidad `Informes`.
         * - Uso de `DbContext` con la clase `Model1` para acceso a datos mediante Entity Framework 6.
         * 
         * CAMPOS CLAVE:
         * - `O_ActividadesID`: Clave primaria de la entidad.
         * - `Nombre_O_Actividad`: Nombre descriptivo de la actividad técnica.
         * - `Descripcion_O_Actividad`: Detalle adicional de la actividad realizada.
         * 
         * RELACIONES:
         * - Relación de navegación con `Informes` mediante una colección virtual.
         * 
         * NOTAS DE DESARROLLO:
         * - El uso de `context = null` permite inyectar un contexto externo si es necesario.
         * - Se emplea control de excepciones en cada operación para mayor estabilidad.
         * 
         * REFERENCIA:
         * -
         * 
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public O_Actividades()
        {
            Informes = new HashSet<Informes>();
        }

        public int O_ActividadesID { get; set; }

        [StringLength(100)]
        public string Nombre_O_Actividad { get; set; }

        [StringLength(255)]
        public string Descripcion_O_Actividad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }


        //Listar_Otras
        //public List<O_Actividades> Listar()
        //{
        //    var ObjO_Actividades = new List<O_Actividades>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjO_Actividades = db.O_Actividades.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjO_Actividades;
        //}

        //Listar_Otras

        public List<O_Actividades> Listar(Model1 context = null)
        {
            var ObjO_Actividades = new List<O_Actividades>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjO_Actividades = db.O_Actividades.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjO_Actividades;
        }

        //Buscar_Otras
        public List<O_Actividades> Buscar(string criterio)
        {
            var ObjO_Actividades = new List<O_Actividades>();
            try
            {
                using (var db = new Model1())
                {
                    ObjO_Actividades = db.O_Actividades.
                        Where(x => x.Nombre_O_Actividad.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjO_Actividades;

        }

        // Obtener_Otras
        public O_Actividades Obtener(int id)
        {

            var ObjO_Actividades = new O_Actividades();
            try
            {
                using (var db = new Model1())
                {
                    ObjO_Actividades = db.O_Actividades.
                        Where(x => x.O_ActividadesID == id).SingleOrDefault();
                }


            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjO_Actividades;

        }

        //Agregar_Otras
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.O_ActividadesID > 0)
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

        //Eliminar_Otras
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
