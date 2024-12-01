namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Falla")]
    public partial class Falla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Falla()
        {
            Informes = new HashSet<Informes>();
        }

        public int FallaID { get; set; }

        [StringLength(100)]
        public string Nombre_Falla { get; set; }

        [StringLength(255)]
        public string Descripcion_Falla { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        //Listar_Falla
        //public List<Falla> Listar()
        //{
        //    var ObjFalla = new List<Falla>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjFalla = db.Falla.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjFalla;
        //}

        //Listar_Falla

        public List<Falla> Listar(Model1 context = null)
        {
            var ObjFalla = new List<Falla>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjFalla = db.Falla.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjFalla;
        }

        //Buscar_Falla
        public List<Falla> Buscar(string criterio)
        {
            var ObjFalla = new List<Falla>();
            try
            {
                using (var db = new Model1())
                {
                    ObjFalla = db.Falla.
                        Where(x => x.Nombre_Falla.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjFalla;

        }

        // Obtener_Falla
        public Falla Obtener(int id)
        {

            var ObjFalla = new Falla();
            try
            {
                using (var db = new Model1())
                {
                    ObjFalla = db.Falla.
                        Where(x => x.FallaID == id).SingleOrDefault();
                }


            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjFalla;

        }

        //Agregar_Falla
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.FallaID > 0)
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

        //Eliminar_Falla
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
