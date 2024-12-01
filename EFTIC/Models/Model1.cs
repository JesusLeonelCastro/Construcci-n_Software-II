using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EFTIC.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Asignar> Asignar { get; set; }
        public virtual DbSet<Equipo_Retirado> Equipo_Retirado { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Falla> Falla { get; set; }
        public virtual DbSet<Informes> Informes { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<O_Actividades> O_Actividades { get; set; }
        public virtual DbSet<Sede> Sede { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tipo_Equipo> Tipo_Equipo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .Property(e => e.Nombre_Area)
                .IsUnicode(false);

            modelBuilder.Entity<Area>()
                .Property(e => e.Descripcion_Area)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Nombre_Equipo)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Serie)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Cod_Patrimonial)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Sub_Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Codigo_Interno)
                .IsUnicode(false);

            modelBuilder.Entity<Equipo_Retirado>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.Nombre_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Estados>()
                .Property(e => e.Descripcion_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.Nombre_Falla)
                .IsUnicode(false);

            modelBuilder.Entity<Falla>()
                .Property(e => e.Descripcion_Falla)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Diagnostico)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Solucion_Primaria)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Nombre_Equipos)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Serie)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Cod_Patrimonial)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Sub_Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Codigo_Interno)
                .IsUnicode(false);

            modelBuilder.Entity<Informes>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Cod_Patrimonial)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Serie)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Codigo_Interno)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.direccion_MAC)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Capacidad_Disco)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Capacidad_Ram)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Marca_Procesador)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Direccion_IP)
                .IsUnicode(false);

            modelBuilder.Entity<Inventario>()
                .Property(e => e.Sistema_operativo)
                .IsUnicode(false);

            modelBuilder.Entity<O_Actividades>()
                .Property(e => e.Nombre_O_Actividad)
                .IsUnicode(false);

            modelBuilder.Entity<O_Actividades>()
                .Property(e => e.Descripcion_O_Actividad)
                .IsUnicode(false);

            modelBuilder.Entity<Sede>()
                .Property(e => e.Nombre_Sede)
                .IsUnicode(false);

            modelBuilder.Entity<Sede>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Tipo_Equipo>()
                .Property(e => e.Nombre_Tipo_Equipo)
                .IsUnicode(false);

            modelBuilder.Entity<Tipo_Equipo>()
                .Property(e => e.Descripcion_Tipo_Equipo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.DNI)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nombre_Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Apellido_Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.TipoUsuario)
                .IsUnicode(false);
        }
    }
}
