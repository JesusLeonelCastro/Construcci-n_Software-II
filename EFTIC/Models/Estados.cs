namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;

    public partial class Estados
    {
        /*
         * ------------------------------------------------------------
         * MODELO: Estados.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 15/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Clase que representa los diferentes estados asociados a un informe, 
         * como por ejemplo: "Pendiente", "Revisado", "Aprobado", etc. 
         * Se utiliza para categorizar el estado de los equipos o reportes dentro del sistema.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Listar todos los estados existentes.
         * - Buscar estados por nombre.
         * - Obtener un estado específico por ID.
         * - Guardar o actualizar registros.
         * - Eliminar un estado del sistema.
         * 
         * RELACIONES:
         * - Relación uno a muchos con la entidad `Informes`.
         * 
         * BUENAS PRÁCTICAS UTILIZADAS:
         * - Separación clara de responsabilidades.
         * - Manejo controlado de excepciones.
         * - Inyección opcional de contexto `Model1` para facilitar pruebas y reutilización.
         * 
         * REQUERIMIENTO FUNCIONAL RELACIONADO:
         * - RF-006 - Gestión de Estados de Informes
         * 
         * NOTA:
         * - Este modelo es clave para el flujo de control de reportes técnicos dentro del módulo de inventario.
         * 
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estados()
        {
            Informes = new HashSet<Informes>();
        }

        [Key]
        public int EstadoID { get; set; }

        [StringLength(100)]
        public string Nombre_Estado { get; set; }

        [StringLength(255)]
        public string Descripcion_Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        //Listar_Estado
        //public List<Estados> Listar()
        //{
        //    var ObjEstado = new List<Estados>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjEstado = db.Estados.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjEstado;
        //}

        //Listar_Estado
        public List<Estados> Listar(Model1 context = null)
        {
            var ObjEstado = new List<Estados>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjEstado = db.Estados.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjEstado;
        }



        //Buscar_Estado
        public List<Estados> Buscar(string criterio)
        {
            var ObjEstado = new List<Estados>();
            try
            {
                using (var db = new Model1())
                {
                    ObjEstado = db.Estados.
                        Where(x => x.Nombre_Estado.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjEstado;

        }

        // Obtener_Estado
        public Estados Obtener(int id)
        {

            var ObjEstado = new Estados();
            try
            {
                using (var db = new Model1())
                {
                    ObjEstado = db.Estados.
                        Where(x => x.EstadoID == id).SingleOrDefault();
                }


            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjEstado;

        }

        //Agregar_Estado
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.EstadoID > 0)
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

        //Eliminar_Estado

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
