namespace EFTIC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Remoting;

    [Table("Usuario")]
    public partial class Usuario
    {
        /*
         * ------------------------------------------------------------
         * CLASE: Usuario.cs
         * UBICACIÓN: EFTIC.Models
         * AUTOR: Jesús Leonel Castro Gutiérrez
         * FECHA DE DESARROLLO: 01/04/2025 al 20/06/2025
         * 
         * DESCRIPCIÓN GENERAL:
         * Representa la entidad `Usuario`, utilizada para gestionar los datos de los usuarios 
         * del sistema EFTIC, incluyendo su autenticación, sede, área y tipo de usuario.
         * 
         * FUNCIONALIDADES IMPLEMENTADAS:
         * - Propiedades básicas de identificación, autenticación y relación con otras entidades.
         * - Relaciones con `Sede`, `Area`, `Asignar` e `Informes`.
         * - Métodos CRUD para la gestión de usuarios.
         * - Métodos adicionales para autenticación y obtención de datos por login.
         * 
         * MÉTODOS DESTACADOS:
         * - `Listar()`: Lista todos los usuarios con sus respectivas sedes y áreas.
         * - `Buscar(string criterio)`: Filtra usuarios por nombre.
         * - `Obtener(int id)`: Recupera un usuario por su ID, incluyendo sus relaciones.
         * - `Guardar()`: Crea o actualiza un usuario.
         * - `Eliminar()`: Elimina un usuario existente.
         * - `Autenticar()`: Valida las credenciales del usuario mediante su DNI y contraseña.
         * - `ObtenerDatos(string correo)`: Estructura para recuperación de datos por correo.
         * 
         * NOTAS DE IMPLEMENTACIÓN:
         * - Usa Entity Framework 6 (`Model1`) como contexto.
         * - Compatible con autenticación básica mediante DNI y contraseña.
         * - Preparado para futuras extensiones como recuperación de contraseña o tokens.
         * 
         * REQUERIMIENTO RELACIONADO:
         * - RF-001 - Requerimiento Funcional - Gestión de Usuarios
         * ------------------------------------------------------------
         */


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Asignar = new HashSet<Asignar>();
            Informes = new HashSet<Informes>();
        }

        public int UsuarioID { get; set; }

        [StringLength(8)]
        public string DNI { get; set; }

        [StringLength(100)]
        public string Nombre_Usuario { get; set; }

        [StringLength(100)]
        public string Apellido_Usuario { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(100)]
        public string TipoUsuario { get; set; }

        public int? SedeID { get; set; }

        public int? AreaID { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Asignar> Asignar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Informes> Informes { get; set; }

        public virtual Sede Sede { get; set; }


        //Listar_Usuario
        public List<Usuario> Listar(Model1 context = null)
        {
            var ObjUsuario = new List<Usuario>();
            try
            {
                using (var db = context ?? new Model1())
                {
                    ObjUsuario = db.Usuario
                                   .Include(a => a.Sede)  // Incluir relaci�n con Sede
                                   .Include(a => a.Area)  // Incluir relaci�n con Area
                                   .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Listar(): " + ex.Message);
                throw;
            }
            return ObjUsuario;
        }


        //Buscar_Usuario
        public List<Usuario> Buscar(string criterio)
        {
            var ObjUsuario = new List<Usuario>();
            try
            {
                using (var db = new Model1())
                {
                    ObjUsuario = db.Usuario.
                        Where(x => x.Nombre_Usuario.Contains(criterio)).ToList();
                }

            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjUsuario;

        }

        // Obtener_Usuario
        public Usuario Obtener(int id)
        {

            var ObjUsuario = new Usuario();
            try
            {
                using (var db = new Model1())
                {
                    ObjUsuario = db.Usuario
                       .Include("Sede")
                       .Include("Area")
                       .Where(x => x.UsuarioID == id)
                       .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw;

            }
            return ObjUsuario;

        }

        //Agregar_Usuario
        public void Guardar()
        {

            try
            {
                using (var db = new Model1())
                {
                    if (this.UsuarioID > 0)
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

        //Eliminar_Usuario
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


        //Instancis con la BD
        Model1 db = new Model1();

        //Autenticar_Login_Usuario
        public bool Autenticar()
        {

            return db.Usuario
                   .Where(x => x.DNI == this.DNI
                   && x.Password == this.Password)
                   .FirstOrDefault() != null;
        }

        //obtener datos del login
        public Usuario ObtenerDatos(string Correo)
        {
            var usuario = new Usuario();

            try
            {
                using (var db = new Model1())
                {

                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }

        //RF - 001 - REQUERIEMINTO FUNCIONAL - "USUARIOS"
    }
}
