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