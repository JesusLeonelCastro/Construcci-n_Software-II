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
