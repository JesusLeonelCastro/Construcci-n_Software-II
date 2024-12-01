namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Inventario")]
    public partial class Inventario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inventario()
        {
            Asignar = new HashSet<Asignar>();
        }

        public int InventarioID { get; set; }

        [StringLength(100)]
        public string Cod_Patrimonial { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Serie { get; set; }

        [StringLength(50)]
        public string Modelo { get; set; }

        [StringLength(50)]
        public string Marca { get; set; }

        [StringLength(50)]
        public string Codigo_Interno { get; set; }

        [StringLength(50)]
        public string direccion_MAC { get; set; }

        [StringLength(50)]
        public string Capacidad_Disco { get; set; }

        [StringLength(50)]
        public string Capacidad_Ram { get; set; }

        [StringLength(50)]
        public string Marca_Procesador { get; set; }

        [StringLength(50)]
        public string Direccion_IP { get; set; }

        [StringLength(50)]
        public string Sistema_operativo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ingreso { get; set; }

        public int? Tipo_EquipoID { get; set; }

        public int? SedeID { get; set; }

        public int? AreaID { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignar> Asignar { get; set; }

        public virtual Sede Sede { get; set; }

        public virtual Tipo_Equipo Tipo_Equipo { get; set; }

        //Listar_Inventario      "Poner la relacion de 2 o 3 tablas"
        public List<Inventario> Listar()
        {
            var ObjInventario = new List<Inventario>();
            try
            {
                using (var db = new Model1())
                {
                    // Incluye la relación con la tabla Categorias
                    ObjInventario = db.Inventario.Include(a => a.Area).ToList();
                    ObjInventario = db.Inventario.Include(a => a.Sede).ToList();
                    ObjInventario = db.Inventario.Include(a => a.Tipo_Equipo).ToList();
                    


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjInventario;
        }

        //Buscar_Inventario  
        public List<Inventario> Buscar(string criterio)
        {
            var ObjInventario = new List<Inventario>();
            try
            {
                using (var db = new Model1())
                {
                    // Incluye las relaciones con las tablas relacionadas y filtra por el criterio
                    ObjInventario = db.Inventario
                        .Include(a => a.Area)
                        .Include(a => a.Sede)
                        .Include(a => a.Tipo_Equipo)
                        .Where(a => a.Cod_Patrimonial.Contains(criterio))
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Buscar(): " + ex.Message);
                throw;
            }
            return ObjInventario;
        }

        // Obtener_Inventario     "Cada debes poner la relacion de 2 o 3 tablas"
        public Inventario Obtener(int id)
        {
            var ObjInventario = new Inventario();
            try
            {
                using (var db = new Model1())
                {
                    ObjInventario = db.Inventario.
                        Where(x => x.InventarioID == id).SingleOrDefault();
                }

                using (var db = new Model1())
                {
                    ObjInventario = db.Inventario
                        .Include("Area")
                        .Include("Sede")
                        .Include("Tipo_Equipo")
                        

                        .Where(x => x.InventarioID == id)
                        .SingleOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjInventario;
        }

        //Guardar_Inventario  
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.InventarioID > 0)
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

        //Eliminar_Inventario  
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
