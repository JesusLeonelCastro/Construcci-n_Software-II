namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Sede")]
    public partial class Sede
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sede()
        {

            Area = new HashSet<Area>();
            Informes = new HashSet<Informes>();
            Inventario = new HashSet<Inventario>();
            Usuario = new HashSet<Usuario>();
        }



        public int SedeID { get; set; }

        [StringLength(100)]
        public string Nombre_Sede { get; set; }

        [StringLength(255)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Area> Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventario> Inventario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        

        //Listar_Sede
        public List<Sede> Listar(Model1 context = null)
        {
            var ObjSede = new List<Sede>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjSede = db.Sede.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjSede;
        }


        //Buscar_Sede 
        public List<Sede> Buscar(string criterio)
        {
            var ObjSede = new List<Sede>();
            try
            {
                using (var db = new Model1())
                {
                    ObjSede = db.Sede.
                        Where(x => x.Nombre_Sede.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjSede;

        }

        // Obtener_Sede
        public Sede Obtener(int id)
        {

            var ObjSede = new Sede();
            try
            {
                using (var db = new Model1())
                {
                    ObjSede = db.Sede.
                        Where(x => x.SedeID == id).SingleOrDefault();
                }


            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjSede;

        }

        //Agregar_Sede
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.SedeID > 0)
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

        //Eliminar_Sede 
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
        //RF - 004 - REQURIMIENTO FUNCIONAL - SEDES 

    }
}
