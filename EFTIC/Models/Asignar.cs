namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
