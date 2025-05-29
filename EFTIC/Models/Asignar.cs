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
    }
}