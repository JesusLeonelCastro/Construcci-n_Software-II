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

        ////Listar_Area 
        //public List<Area> Listar()
        //{
        //    var ObjArea = new List<Area>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjArea = db.Area.ToList();
        //            ObjArea = db.Area.Include(a => a.Sede).ToList();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjArea;
        //}

        //Listar_Area
        public List<Area> Listar(Model1 context = null)
        {
            var ObjArea = new List<Area>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjArea = db.Area
                                   .Include(a => a.Sede)  // Incluir relación con Sede
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
