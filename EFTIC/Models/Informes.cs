namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Informes
    {
        /*
         * ------------------------------------------------------------
         * MODELO: Informes.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 20/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Representa los informes técnicos generados por el personal del área de soporte,
         * que documentan diagnósticos, soluciones aplicadas y características de los equipos
         * intervenidos. Este modelo es uno de los ejes centrales del sistema de gestión técnica.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Listar informes con relaciones a múltiples entidades (Área, Sede, Falla, etc.).
         * - Buscar informes por título.
         * - Obtener detalles completos de un informe por ID, incluyendo datos relacionados.
         * - Guardar (crear/editar) informes técnicos.
         * - Eliminar informes.
         * 
         * RELACIONES:
         * - FK con Area, Sede, Usuario, Tipo_Equipo, Estados, Falla, O_Actividades, Equipo_Retirado.
         * 
         * OBSERVACIONES:
         * - El método `Listar()` contiene múltiples llamadas redundantes a `Include()`; 
         *   podría optimizarse agrupando todas las relaciones en una sola llamada LINQ.
         * 
         * REQUERIMIENTO FUNCIONAL RELACIONADO:
         * - RF-009 - Registro y gestión de informes técnicos.
         * 
         * BUENAS PRÁCTICAS:
         * - Separación clara de responsabilidades.
         * - Uso de `Include()` para carga explícita de entidades relacionadas.
         * - Control de errores con manejo de excepciones.
         * 
         * ------------------------------------------------------------
         */


        [Key]
        public int InformeID { get; set; }

        [StringLength(255)]
        public string Titulo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha_Solicitud { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Fecha_Informe { get; set; }

        [StringLength(100)]
        public string Diagnostico { get; set; }

        [Column(TypeName = "text")]
        public string Solucion_Primaria { get; set; }

        [StringLength(100)]
        public string Nombre_Equipos { get; set; }

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

        [StringLength(100)]
        public string Observaciones { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ingreso { get; set; }

        public int? EstadoID { get; set; }

        public int? UsuarioID { get; set; }

        public int? Tipo_EquipoID { get; set; }

        public int? AreaID { get; set; }

        public int? SedeID { get; set; }

        public int? FallaID { get; set; }

        public int? O_ActividadesID { get; set; }

        public int? Equipo_RetiradosID { get; set; }

        public virtual Area Area { get; set; }

        public virtual Equipo_Retirado Equipo_Retirado { get; set; }

        public virtual Estados Estados { get; set; }

        public virtual Falla Falla { get; set; }

        public virtual O_Actividades O_Actividades { get; set; }

        public virtual Sede Sede { get; set; }

        public virtual Tipo_Equipo Tipo_Equipo { get; set; }

        public virtual Usuario Usuario { get; set; }

        //Listar_Informes      "Poner la relacion de 2 o 3 tablas"
        public List<Informes> Listar()
        {
            var ObjInformes = new List<Informes>();
            try
            {
                using (var db = new Model1())
                {
                    // Incluye la relación con la tabla Categorias
                    ObjInformes = db.Informes.Include(a => a.Area).ToList();
                    ObjInformes = db.Informes.Include(a => a.Falla).ToList();
                    ObjInformes = db.Informes.Include(a => a.Sede).ToList();
                    ObjInformes = db.Informes.Include(a => a.Tipo_Equipo).ToList();
                    ObjInformes = db.Informes.Include(a => a.Area).ToList();
                    ObjInformes = db.Informes.Include(a => a.O_Actividades).ToList();
                    ObjInformes = db.Informes.Include(a => a.Estados).ToList();
                    ObjInformes = db.Informes.Include(a => a.Equipo_Retirado).ToList();
                    ObjInformes = db.Informes.Include(a => a.Usuario).ToList();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjInformes;
        }

        //Buscar_Informes
        public List<Informes> Buscar(string criterio)
        {
            var ObjInformes = new List<Informes>();
            try
            {
                using (var db = new Model1())
                {
                    // Incluye las relaciones con las tablas relacionadas y filtra por el criterio
                    ObjInformes = db.Informes
                        .Include(a => a.Area)
                        .Include(a => a.Falla)
                        .Include(a => a.Sede)
                        .Include(a => a.Tipo_Equipo)
                        .Include(a => a.O_Actividades)
                        .Include(a => a.Estados)
                        .Include(a => a.Equipo_Retirado)
                        .Include(a => a.Usuario)
                        .Where(a => a.Titulo.Contains(criterio))
                        .ToList();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Buscar(): " + ex.Message);
                throw;
            }
            return ObjInformes;
        }

        // Obtener_Informes    "Cada debes poner la relacion de 2 o 3 tablas"
        public Informes Obtener(int id)
        {
            var ObjInformes = new Informes();
            try
            {
                using (var db = new Model1())
                {
                    ObjInformes = db.Informes.
                        Where(x => x.InformeID == id).SingleOrDefault();
                }

                using (var db = new Model1())
                {
                    ObjInformes = db.Informes
                        .Include("Area")
                        .Include("Falla")
                        .Include("Sede")
                        .Include("Tipo_Equipo")
                        .Include("O_Actividades")
                        .Include("Estados")
                        .Include("Equipo_Retirado")
                        .Include("Usuario")

                        .Where(x => x.InformeID == id)
                        .SingleOrDefault();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjInformes;
        }

        //Guardar_Informes
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.InformeID > 0)
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

        //Eliminar_Informes
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

        //RF-009 - Requerimiento funcional - Nuevo Informe 

    }
}
