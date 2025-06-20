namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Equipo_Retirado
    {
        /*
         * ------------------------------------------------------------
         * MODELO: Equipo_Retirado.cs
         * UBICACI�N: EFTIC.Models
         * AUTOR: Jes�s Leonel Castro Guti�rrez
         * FECHA DE DESARROLLO: 01/04/2025 al 15/06/2025
         * 
         * DESCRIPCI�N GENERAL:
         * Representa la entidad "Equipo_Retirado" dentro del sistema EFTIC, 
         * utilizada para registrar informaci�n sobre equipos inform�ticos 
         * que han sido dados de baja o retirados del inventario activo.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Listar todos los equipos retirados.
         * - Buscar por nombre de equipo.
         * - Obtener un equipo retirado por su ID.
         * - Registrar nuevos equipos retirados o actualizar existentes.
         * - Eliminar un registro de equipo retirado del sistema.
         * 
         * RELACIONES:
         * - Relaci�n con la entidad `Informes`, permitiendo su trazabilidad documental.
         * 
         * BUENAS PR�CTICAS UTILIZADAS:
         * - Uso de `try-catch` para manejo de excepciones controladas.
         * - M�todos desacoplados para responsabilidad �nica.
         * - Flexibilidad con inyecci�n opcional de contexto (`Model1`).
         * 
         * REQUERIMIENTO FUNCIONAL RELACIONADO:
         * - RF-007 - Gesti�n de Equipos Retirados
         * 
         * NOTA:
         * - Validaciones adicionales podr�an incorporarse a nivel de vista o controlador.
         * 
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipo_Retirado()
        {
            Informes = new HashSet<Informes>();
        }

        [Key]
        public int Equipo_RetiradosID { get; set; }

        [StringLength(100)]
        public string Nombre_Equipo { get; set; }

        [StringLength(100)]
        public string Tipo { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Serie { get; set; }

        [StringLength(100)]
        public string Cod_Patrimonial { get; set; }

        [StringLength(100)]
        public string Sub_Tipo { get; set; }

        [StringLength(50)]
        public string Modelo { get; set; }

        [StringLength(50)]
        public string Marca { get; set; }

        [StringLength(50)]
        public string Codigo_Interno { get; set; }

        [StringLength(50)]
        public string Observaciones { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ingreso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }


        //Listar_Estado
        //public List<Equipo_Retirado> Listar()
        //{
        //    var ObjEquipo = new List<Equipo_Retirado>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            ObjEquipo = db.Equipo_Retirado.ToList();


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error en Listar(): " + ex.Message);
        //        throw;
        //    }
        //    return ObjEquipo;
        //}

        //Listar_Equipo_Retirado
        public List<Equipo_Retirado> Listar(Model1 context = null)
        {
            var ObjEquipo = new List<Equipo_Retirado>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjEquipo = db.Equipo_Retirado.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjEquipo;
        }

        //Buscar_Equipo
        public List<Equipo_Retirado> Buscar(string criterio)
        {
            var ObjEquipo = new List<Equipo_Retirado>();
            try
            {
                using (var db = new Model1())
                {
                    ObjEquipo = db.Equipo_Retirado.

                        Where(x => x.Nombre_Equipo.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjEquipo;

        }

        // Obtener_Equipo
        public Equipo_Retirado Obtener(int id)
        {

            var ObjEquipo = new Equipo_Retirado();
            try
            {
                using (var db = new Model1())
                {
                    ObjEquipo = db.Equipo_Retirado
                                         .Where(x => x.Equipo_RetiradosID == id)
                                         .SingleOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjEquipo;

        }

        //Agregar_Equipo
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.Equipo_RetiradosID > 0)
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

        //Eliminar_Equipo

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
